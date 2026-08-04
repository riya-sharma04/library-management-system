using System;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class book_status : System.Web.UI.Page
{
    private string ConnectionString
    {
        get
        {
            return ConfigurationManager
                .ConnectionStrings["ABC"]
                .ConnectionString;
        }
    }


    // =========================================================
    // PAGE LOAD
    // =========================================================

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("login-page.aspx");
            return;
        }

        //lblUserName.Text = Session["UserName"].ToString();

        lblUserName.Text = "👤 Welcome,";
        lblUserName1.Text = Session["UserName"].ToString();
        lblUserName2.Text = "Library Management Dashboard";

        if (!IsPostBack)
        {
            // Default status
            if (DropDownListStatus.Items.Count > 0)
            {
                DropDownListStatus.SelectedValue = "ALL";
            }

            LoadBookStatus();
        }
    }


    // =========================================================
    // COMMON QUERY
    // =========================================================

    private string GetBookStatusQuery()
    {
        return @"
            SELECT
                b.acc_no,
                b.title,
                b.author,
                b.subject,

                CASE
                    WHEN i.acc_no IS NULL THEN 'NEVER ISSUED'
                    WHEN i.issued_or_returned = 'ISSUED' THEN 'ISSUED'
                    WHEN i.issued_or_returned = 'RETURNED' THEN 'AVAILABLE'
                    ELSE 'AVAILABLE'
                END AS status,

                CASE
                    WHEN i.acc_no IS NULL THEN '-'
                    ELSE IFNULL(i.student_or_teacher_name, '-')
                END AS issued_to,

                CASE
                    WHEN i.acc_no IS NULL THEN '-'
                    ELSE IFNULL(i.member_type, '-')
                END AS user_type,

                i.issue_date,
                i.return_date

            FROM books_details b

            LEFT JOIN
            (
                SELECT i1.*
                FROM issue_book_details i1

                INNER JOIN
                (
                    SELECT
                        acc_no,
                        MAX(issue_no) AS latest_issue_no
                    FROM issue_book_details
                    GROUP BY acc_no
                ) i2

                ON i1.acc_no = i2.acc_no
                AND i1.issue_no = i2.latest_issue_no

            ) i

            ON b.acc_no = i.acc_no
        ";
    }


    // =========================================================
    // LOAD ALL BOOK STATUS
    // =========================================================

    private void LoadBookStatus()
    {
        using (MySqlConnection CONN =
            new MySqlConnection(ConnectionString))
        {
            try
            {
                CONN.Open();

                string query = GetBookStatusQuery() +
                                " ORDER BY b.acc_no;";

                using (MySqlCommand CMD =
                    new MySqlCommand(query, CONN))
                {
                    using (MySqlDataAdapter DA =
                        new MySqlDataAdapter(CMD))
                    {
                        DataTable DT = new DataTable();

                        DA.Fill(DT);

                        GridView1.DataSource = DT;
                        GridView1.DataBind();

                        CalculateSummary(DT);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowAlert(
                    "Error loading book status: " +
                    ex.Message
                );
            }
        }
    }


    // =========================================================
    // SUMMARY
    // =========================================================

    private void CalculateSummary(DataTable DT)
    {
        int totalBooks = DT.Rows.Count;

        int availableBooks = 0;
        int issuedBooks = 0;
        int neverIssuedBooks = 0;

        foreach (DataRow row in DT.Rows)
        {
            string status =
                row["status"].ToString().Trim();

            if (status == "AVAILABLE")
            {
                availableBooks++;
            }
            else if (status == "ISSUED")
            {
                issuedBooks++;
            }
            else if (status == "NEVER ISSUED")
            {
                neverIssuedBooks++;
            }
        }

        lblTotalBooks.Text =
            totalBooks.ToString();

        lblAvailableBooks.Text =
            availableBooks.ToString();

        lblIssuedBooks.Text =
            issuedBooks.ToString();

        lblNeverIssued.Text =
            neverIssuedBooks.ToString();
    }


    // =========================================================
    // SEARCH TEXTBOX
    // =========================================================

    protected void TextBoxSearch_TextChanged(
        object sender,
        EventArgs e)
    {
        FilterBookStatus();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {

        FilterBookStatus();
        // Search using keyword
    }

    // =========================================================
    // STATUS DROPDOWN
    // =========================================================

    protected void DropDownListStatus_SelectedIndexChanged(
        object sender,
        EventArgs e)
    {
        FilterBookStatus();
    }


    // =========================================================
    // FILTER BOOK STATUS
    // =========================================================

    private void FilterBookStatus()
    {
        using (MySqlConnection CONN =
            new MySqlConnection(ConnectionString))
        {
            try
            {
                CONN.Open();

                string query = GetBookStatusQuery() + @"

                    WHERE
                    (
                        @Search = ''
                        OR CAST(b.acc_no AS CHAR) LIKE @SearchLike
                        OR b.title LIKE @SearchLike
                        OR b.author LIKE @SearchLike
                        OR b.subject LIKE @SearchLike
                    )

                    AND
                    (
                        @Status = 'ALL'

                        OR
                        (
                            @Status = 'AVAILABLE'
                            AND i.acc_no IS NOT NULL
                            AND i.issued_or_returned = 'RETURNED'
                        )

                        OR
                        (
                            @Status = 'ISSUED'
                            AND i.acc_no IS NOT NULL
                            AND i.issued_or_returned = 'ISSUED'
                        )

                        OR
                        (
                            @Status = 'NEVER'
                            AND i.acc_no IS NULL
                        )
                    )

                    ORDER BY b.acc_no;
                ";

                using (MySqlCommand CMD =
                    new MySqlCommand(query, CONN))
                {
                    string search =
                        TextBoxSearch.Text.Trim();

                    string status = "ALL";

                    if (DropDownListStatus.SelectedValue != null &&
                        DropDownListStatus.SelectedValue != "")
                    {
                        status =
                            DropDownListStatus.SelectedValue;
                    }

                    CMD.Parameters.Add(
                        "@Search",
                        MySqlDbType.VarChar
                    ).Value = search;

                    CMD.Parameters.Add(
                        "@SearchLike",
                        MySqlDbType.VarChar
                    ).Value = "%" + search + "%";

                    CMD.Parameters.Add(
                        "@Status",
                        MySqlDbType.VarChar
                    ).Value = status;

                    using (MySqlDataAdapter DA =
                        new MySqlDataAdapter(CMD))
                    {
                        DataTable DT =
                            new DataTable();

                        DA.Fill(DT);

                        GridView1.DataSource = DT;
                        GridView1.DataBind();

                        // Summary always shows complete library count
                        LoadSummary();
                    }
                }
            }
            catch (Exception ex)
            {
                ShowAlert(
                    "Search Error: " +
                    ex.Message
                );
            }
        }
    }


    // =========================================================
    // LOAD FULL SUMMARY
    // =========================================================

    private void LoadSummary()
    {
        using (MySqlConnection CONN =
            new MySqlConnection(ConnectionString))
        {
            try
            {
                CONN.Open();

                string query = GetBookStatusQuery() + @"

                    SELECT
                        COUNT(*) AS TotalBooks,

                        SUM(
                            CASE
                                WHEN status = 'AVAILABLE'
                                THEN 1
                                ELSE 0
                            END
                        ) AS AvailableBooks,

                        SUM(
                            CASE
                                WHEN status = 'ISSUED'
                                THEN 1
                                ELSE 0
                            END
                        ) AS IssuedBooks,

                        SUM(
                            CASE
                                WHEN status = 'NEVER ISSUED'
                                THEN 1
                                ELSE 0
                            END
                        ) AS NeverIssued

                    FROM
                    (
                        SELECT
                            b.acc_no,

                            CASE
                                WHEN i.acc_no IS NULL
                                    THEN 'NEVER ISSUED'

                                WHEN i.issued_or_returned = 'ISSUED'
                                    THEN 'ISSUED'

                                WHEN i.issued_or_returned = 'RETURNED'
                                    THEN 'AVAILABLE'

                                ELSE 'AVAILABLE'
                            END AS status

                        FROM books_details b

                        LEFT JOIN
                        (
                            SELECT i1.*
                            FROM issue_book_details i1

                            INNER JOIN
                            (
                                SELECT
                                    acc_no,
                                    MAX(issue_no)
                                    AS latest_issue_no
                                FROM issue_book_details
                                GROUP BY acc_no
                            ) i2

                            ON i1.acc_no = i2.acc_no
                            AND i1.issue_no =
                                i2.latest_issue_no

                        ) i

                        ON b.acc_no = i.acc_no
                    ) AS BookStatus;
                ";

                /*
                 * IMPORTANT:
                 * Above query starts with GetBookStatusQuery(),
                 * so it is not suitable for the summary.
                 * Therefore we use a separate query below.
                 */

                query = @"
                    SELECT

                        COUNT(*) AS TotalBooks,

                        SUM(
                            CASE
                                WHEN i.acc_no IS NOT NULL
                                AND i.issued_or_returned = 'RETURNED'
                                THEN 1
                                ELSE 0
                            END
                        ) AS AvailableBooks,

                        SUM(
                            CASE
                                WHEN i.acc_no IS NOT NULL
                                AND i.issued_or_returned = 'ISSUED'
                                THEN 1
                                ELSE 0
                            END
                        ) AS IssuedBooks,

                        SUM(
                            CASE
                                WHEN i.acc_no IS NULL
                                THEN 1
                                ELSE 0
                            END
                        ) AS NeverIssued

                    FROM books_details b

                    LEFT JOIN
                    (
                        SELECT i1.*
                        FROM issue_book_details i1

                        INNER JOIN
                        (
                            SELECT
                                acc_no,
                                MAX(issue_no)
                                AS latest_issue_no
                            FROM issue_book_details
                            GROUP BY acc_no
                        ) i2

                        ON i1.acc_no = i2.acc_no
                        AND i1.issue_no =
                            i2.latest_issue_no

                    ) i

                    ON b.acc_no = i.acc_no;
                ";

                using (MySqlCommand CMD =
                    new MySqlCommand(query, CONN))
                {
                    using (MySqlDataReader DR =
                        CMD.ExecuteReader())
                    {
                        if (DR.Read())
                        {
                            lblTotalBooks.Text =
                                GetSafeValue(
                                    DR["TotalBooks"]
                                );

                            lblAvailableBooks.Text =
                                GetSafeValue(
                                    DR["AvailableBooks"]
                                );

                            lblIssuedBooks.Text =
                                GetSafeValue(
                                    DR["IssuedBooks"]
                                );

                            lblNeverIssued.Text =
                                GetSafeValue(
                                    DR["NeverIssued"]
                                );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowAlert(
                    "Summary Error: " +
                    ex.Message
                );
            }
        }
    }


    // =========================================================
    // GRIDVIEW ROW DATA BOUND
    // =========================================================

    protected void GridView1_RowDataBound(
        object sender,
        GridViewRowEventArgs e)
    {
        if (e.Row.RowType ==
            DataControlRowType.DataRow)
        {
            object statusObject =
                DataBinder.Eval(
                    e.Row.DataItem,
                    "status"
                );

            if (statusObject == null)
                return;

            string status =
                statusObject.ToString().Trim();

            /*
             * Columns:
             *
             * 0 = Acc No
             * 1 = Book Title
             * 2 = Author
             * 3 = Subject
             * 4 = Status
             * 5 = Issued To
             * 6 = User Type
             * 7 = Issue Date
             * 8 = Return Date
             */

            if (e.Row.Cells.Count > 4)
            {
                e.Row.Cells[4].CssClass = "";

                if (status == "AVAILABLE")
                {
                    e.Row.Cells[4].CssClass =
                        "status-available";
                }
                else if (status == "ISSUED")
                {
                    e.Row.Cells[4].CssClass =
                        "status-issued";
                }
                else if (status == "NEVER ISSUED")
                {
                    e.Row.Cells[4].CssClass =
                        "status-never";
                }
            }
        }
    }


    // =========================================================
    // GRIDVIEW DELETE COMMAND
    // =========================================================

    protected void GridView1_RowCommand(
        object sender,
        GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteBook")
        {
            string accNo =
                Convert.ToString(
                    e.CommandArgument
                );

            if (string.IsNullOrWhiteSpace(accNo))
            {
                ShowAlert(
                    "Invalid Acc No."
                );

                return;
            }

            DeleteBook(accNo);
        }
    }


    // =========================================================
    // DELETE BOOK
    // =========================================================

    private void DeleteBook(string accNo)
    {
        using (MySqlConnection CONN =
            new MySqlConnection(ConnectionString))
        {
            try
            {
                CONN.Open();

                // ---------------------------------------------
                // CHECK ISSUE / RETURN HISTORY
                // ---------------------------------------------

                string checkQuery = @"
                    SELECT COUNT(*)
                    FROM issue_book_details
                    WHERE acc_no = @acc_no;
                ";

                using (MySqlCommand CheckCMD =
                    new MySqlCommand(
                        checkQuery,
                        CONN))
                {
                    CheckCMD.Parameters.Add(
                        "@acc_no",
                        MySqlDbType.VarChar
                    ).Value = accNo;

                    int count =
                        Convert.ToInt32(
                            CheckCMD.ExecuteScalar()
                        );

                    if (count > 0)
                    {
                        ShowAlert(
                            "This book has issue/return history and cannot be deleted."
                        );

                        return;
                    }
                }


                // ---------------------------------------------
                // CHECK BOOK EXISTS
                // ---------------------------------------------

                string existsQuery = @"
                    SELECT COUNT(*)
                    FROM books_details
                    WHERE acc_no = @acc_no;
                ";

                using (MySqlCommand ExistsCMD =
                    new MySqlCommand(
                        existsQuery,
                        CONN))
                {
                    ExistsCMD.Parameters.Add(
                        "@acc_no",
                        MySqlDbType.VarChar
                    ).Value = accNo;

                    int exists =
                        Convert.ToInt32(
                            ExistsCMD.ExecuteScalar()
                        );

                    if (exists == 0)
                    {
                        ShowAlert(
                            "Book not found."
                        );

                        return;
                    }
                }


                // ---------------------------------------------
                // DELETE BOOK
                // ---------------------------------------------

                string deleteQuery = @"
                    DELETE FROM books_details
                    WHERE acc_no = @acc_no;
                ";

                using (MySqlCommand DeleteCMD =
                    new MySqlCommand(
                        deleteQuery,
                        CONN))
                {
                    DeleteCMD.Parameters.Add(
                        "@acc_no",
                        MySqlDbType.VarChar
                    ).Value = accNo;

                    int result =
                        DeleteCMD.ExecuteNonQuery();

                    if (result > 0)
                    {
                        ShowAlertAndRedirect(
                            "Book deleted successfully.",
                            "book-status.aspx"
                        );
                    }
                    else
                    {
                        ShowAlert(
                            "Book could not be deleted."
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                ShowAlert(
                    "Delete Error: " +
                    ex.Message
                );
            }
        }
    }


    // =========================================================
    // LOGOUT
    // =========================================================

    protected void btnLogout_Click(
        object sender,
        EventArgs e)
    {
        Session.Clear();
        Session.Abandon();

        Response.Redirect(
            "login-page.aspx"
        );
    }


    // =========================================================
    // HELPER - ALERT
    // =========================================================

    private void ShowAlert(string message)
    {
        string safeMessage =
            message
                .Replace("\\", "\\\\")
                .Replace("'", "\\'")
                .Replace("\r", "")
                .Replace("\n", " ");

        ClientScript.RegisterStartupScript(
            this.GetType(),
            "alert",
            "alert('" +
            safeMessage +
            "');",
            true
        );
    }


    // =========================================================
    // HELPER - ALERT + REDIRECT
    // =========================================================

    private void ShowAlertAndRedirect(
        string message,
        string url)
    {
        string safeMessage =
            message
                .Replace("\\", "\\\\")
                .Replace("'", "\\'")
                .Replace("\r", "")
                .Replace("\n", " ");

        string safeUrl =
            url.Replace("'", "");

        ClientScript.RegisterStartupScript(
            this.GetType(),
            "success",
            "alert('" +
            safeMessage +
            "');" +
            "window.location='" +
            safeUrl +
            "';",
            true
        );
    }


    //// =========================================================
    //// HELPER - NULL SAFE VALUE
    //// =========================================================

    private string GetSafeValue(object value)
    {
        if (value == null ||
            value == DBNull.Value)
        {
            return "0";
        }

        return value.ToString();
    }
}