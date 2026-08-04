using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

public partial class return_book : System.Web.UI.Page
{
    MySqlConnection CONN = new MySqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] != null)
        {
            lblUserName.Text = "👤 Welcome,";
            lblUserName1.Text = Session["UserName"].ToString();
            lblUserName2.Text = "Library Management Dashboard";
        }
        else
        {
            // If session expired or user not logged in, redirect to login page
            Response.Redirect("~/login-page.aspx");
        }
        if (!IsPostBack)
        {
            TextBox6.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }
    protected void TextBox12_TextChanged(object sender, EventArgs e)
    {
        SearchIssueBook();
    }
    //public void SearchIssueBook()
    //{
    //    if (TextBox12.Text.Trim() == "")
    //    {
    //        Response.Write("<script>alert('Please Enter Issue No')</script>");
    //        TextBox12.Focus();
    //        return;
    //    }

    //    string AA = ConfigurationManager.ConnectionStrings["ABC"].ConnectionString;
    //    CONN = new MySqlConnection(AA);
    //    MySqlCommand CMD1 = new MySqlCommand();
    //    CMD1.Connection = CONN;
    //    CMD1.CommandText = @"SELECT *
    //                 FROM emp.issue_book_details
    //                 WHERE issue_no=@issue_no";

    //    CMD1.Parameters.AddWithValue("@issue_no", TextBox12.Text.Trim());

    //    MySqlDataAdapter DA = new MySqlDataAdapter(CMD1);

    //    DataSet DS = new DataSet();

    //    DA.Fill(DS);

    //    // Issue No exist nahi karta
    //    if (DS.Tables[0].Rows.Count == 0)
    //    {
    //        Response.Write("<script>alert('Issue No Not Found!')</script>");
    //        TextBox12.Focus();
    //        return;
    //    }

    //    // Book already returned
    //    if (DS.Tables[0].Rows[0]["issued_or_returned"].ToString().Trim().ToUpper() == "RETURNED")
    //    {
    //        Response.Write("<script>alert('This book has already been returned!')</script>");

    //        TextBox1.Text = "";
    //        TextBox2.Text = "";
    //        TextBox3.Text = "";
    //        TextBox4.Text = "";
    //        TextBox5.Text = "";
    //        TextBox6.Text = DateTime.Now.ToString("yyyy-MM-dd");
    //        TextBox7.Text = "";
    //        TextBox8.Text = "";
    //        TextBox9.Text = "";
    //        TextBox10.Text = "";
    //        TextBox11.Text = "";
    //        TextBox13.Text = "";

    //        TextBox12.Focus();
    //        return;
    //    }
    //    //--------------- RECORD LOAD ---------------//

    //    DataRow row = DS.Tables[0].Rows[0];

    //    TextBox1.Text = row["acc_no"].ToString();
    //    TextBox2.Text = row["title"].ToString();
    //    TextBox3.Text = row["subject"].ToString();

    //    TextBox4.Text = row["for_days"].ToString();

    //    TextBox7.Text = row["member_type"].ToString();

    //    TextBox8.Text = row["clgRoll_or_teacherID"].ToString();

    //    TextBox9.Text = row["branch_or_dept"].ToString();

    //    TextBox10.Text = row["student_or_teacher_name"].ToString();

    //    TextBox6.Text = DateTime.Now.ToString("yyyy-MM-dd");


    //    //--------------- ISSUE DATE ---------------//

    //    if (!Convert.IsDBNull(row["issue_date"]))
    //    {
    //        DateTime issueDate = Convert.ToDateTime(row["issue_date"]);
    //        TextBox5.Text = issueDate.ToString("yyyy-MM-dd");
    //    }
    //    else
    //    {
    //        TextBox5.Text = "";
    //    }


    //    //--------------- CALCULATE DUE DATE ---------------//

    //    if (!Convert.IsDBNull(row["issue_date"]) &&
    //        !Convert.IsDBNull(row["for_days"]))
    //    {
    //        DateTime issueDate = Convert.ToDateTime(row["issue_date"]);

    //        int allowedDays = Convert.ToInt32(row["for_days"]);

    //        DateTime dueDate = issueDate.AddDays(allowedDays);

    //        TextBox13.Text = dueDate.ToString("yyyy-MM-dd");
    //    }
    //    else
    //    {
    //        TextBox13.Text = "";
    //    }
       
    //    //--------------- CALCULATE DUE DATE ---------------//

    //    string member_type = TextBox7.Text.Trim().ToLower();
    //        if (member_type == "student")
    //        {
    //            if (!string.IsNullOrEmpty(TextBox5.Text) && !string.IsNullOrEmpty(TextBox4.Text))
    //            {
    //                DateTime issueDate = DateTime.Parse(TextBox5.Text);
    //                DateTime returnDate = DateTime.Now.Date; // current date

    //                int allowedDays = Convert.ToInt32(TextBox4.Text);
    //                int actualDays = (returnDate - issueDate).Days;
    //                if (actualDays > allowedDays)
    //                {
    //                    int extraDays = actualDays - allowedDays;
    //                    int fineAmount = extraDays * 5;
    //                    TextBox11.Text = fineAmount.ToString("0.00");
    //                }
    //                else
    //                {
    //                    TextBox11.Text = "0.00";
    //                }
    //            }
    //        }
    //        else
    //        {
    //            // Member is not a student (e.g., teacher), so no fine
    //            TextBox11.Text = "0.00";
    //        }

    //    }

public void SearchIssueBook()
    {
        if (TextBox12.Text.Trim() == "")
        {
            Response.Write("<script>alert('Please Enter Issue No or Account No')</script>");
            TextBox12.Focus();
            return;
        }

        string AA = ConfigurationManager.ConnectionStrings["ABC"].ConnectionString;

        CONN = new MySqlConnection(AA);

        MySqlCommand CMD1 = new MySqlCommand();
        CMD1.Connection = CONN;

        CMD1.CommandText = @"
        SELECT *
        FROM emp.issue_book_details
        WHERE (issue_no = @search OR acc_no = @search)
        AND issued_or_returned = 'ISSUED'
        LIMIT 1";

        CMD1.Parameters.AddWithValue("@search", TextBox12.Text.Trim());

        MySqlDataAdapter DA = new MySqlDataAdapter(CMD1);

        DataSet DS = new DataSet();

        DA.Fill(DS);


        //--------------- NO ISSUED BOOK FOUND ---------------//

        if (DS.Tables[0].Rows.Count == 0)
        {
            Response.Write("<script>alert('No issued book found for return!')</script>");

            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox5.Text = "";
            TextBox6.Text = DateTime.Now.ToString("yyyy-MM-dd");
            TextBox7.Text = "";
            TextBox8.Text = "";
            TextBox9.Text = "";
            TextBox10.Text = "";
            TextBox11.Text = "";
            TextBox13.Text = "";

            TextBox12.Focus();

            return;
        }


        //--------------- RECORD LOAD ---------------//

        DataRow row = DS.Tables[0].Rows[0];

        TextBox1.Text = row["acc_no"].ToString();
        TextBox2.Text = row["title"].ToString();
        TextBox3.Text = row["subject"].ToString();

        TextBox4.Text = row["for_days"].ToString();

        TextBox7.Text = row["member_type"].ToString();

        TextBox8.Text = row["clgRoll_or_teacherID"].ToString();

        TextBox9.Text = row["branch_or_dept"].ToString();

        TextBox10.Text = row["student_or_teacher_name"].ToString();

        TextBox6.Text = DateTime.Now.ToString("yyyy-MM-dd");


        //--------------- ISSUE DATE ---------------//

        if (!Convert.IsDBNull(row["issue_date"]))
        {
            DateTime issueDate = Convert.ToDateTime(row["issue_date"]);
            TextBox5.Text = issueDate.ToString("yyyy-MM-dd");
        }
        else
        {
            TextBox5.Text = "";
        }


        //--------------- CALCULATE DUE DATE ---------------//

        if (!Convert.IsDBNull(row["issue_date"]) &&
            !Convert.IsDBNull(row["for_days"]))
        {
            DateTime issueDate = Convert.ToDateTime(row["issue_date"]);

            int allowedDays = Convert.ToInt32(row["for_days"]);

            DateTime dueDate = issueDate.AddDays(allowedDays);

            TextBox13.Text = dueDate.ToString("yyyy-MM-dd");
        }
        else
        {
            TextBox13.Text = "";
        }


        //--------------- CALCULATE FINE ---------------//

        string member_type = TextBox7.Text.Trim().ToLower();

        if (member_type == "student")
        {
            if (!string.IsNullOrEmpty(TextBox5.Text) &&
                !string.IsNullOrEmpty(TextBox4.Text))
            {
                DateTime issueDate = DateTime.Parse(TextBox5.Text);

                DateTime returnDate = DateTime.Now.Date;

                int allowedDays = Convert.ToInt32(TextBox4.Text);

                int actualDays = (returnDate - issueDate).Days;

                if (actualDays > allowedDays)
                {
                    int extraDays = actualDays - allowedDays;

                    int fineAmount = extraDays * 5;

                    TextBox11.Text = fineAmount.ToString("0.00");
                }
                else
                {
                    TextBox11.Text = "0.00";
                }
            }
        }
        else
        {
            // Teacher ke liye fine nahi
            TextBox11.Text = "0.00";
        }
    }


protected void Button2_Click(object sender, EventArgs e)
    {
        if (TextBox12.Text.Trim() == "")
        {
            Response.Write("<script>alert('Please Enter Issue No or Account No')</script>");
            TextBox12.Focus();
            return;
        }

        string AA = ConfigurationManager.ConnectionStrings["ABC"].ConnectionString;

        CONN = new MySqlConnection(AA);
        CONN.Open();

        MySqlCommand CMD1 = new MySqlCommand();
        CMD1.Connection = CONN;

        CMD1.CommandText = @"
        UPDATE emp.issue_book_details
        SET
            return_date = @return_date,
            for_days = @for_days,
            fine = @fine,
            issued_or_returned = 'RETURNED'
        WHERE (issue_no = @search OR acc_no = @search)
        AND issued_or_returned = 'ISSUED'
        LIMIT 1";

        CMD1.Parameters.AddWithValue("@return_date", DateTime.Now.ToString("yyyy-MM-dd"));
        CMD1.Parameters.AddWithValue("@for_days", TextBox4.Text);
        CMD1.Parameters.AddWithValue("@fine", TextBox11.Text);
        CMD1.Parameters.AddWithValue("@search", TextBox12.Text.Trim());

        int result = CMD1.ExecuteNonQuery();

        CONN.Close();


        //--------------- RETURN SUCCESS ---------------//

        if (result > 0)
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox5.Text = "";

            TextBox6.Text = DateTime.Now.ToString("yyyy-MM-dd");

            TextBox7.Text = "";
            TextBox8.Text = "";
            TextBox9.Text = "";
            TextBox10.Text = "";
            TextBox11.Text = "";
            TextBox13.Text = "";
            TextBox12.Text = "";

            Response.Write("<script>alert('Book Returned Successfully')</script>");

            TextBox12.Focus();
        }
        else
        {
            Response.Write("<script>alert('No issued book found for return!')</script>");

            TextBox12.Focus();
        }
    }



    //protected void Button2_Click(object sender, EventArgs e)
    //{
    //    string AA = ConfigurationManager.ConnectionStrings["ABC"].ConnectionString;
    //    string AB = Convert.ToDateTime(TextBox5.Text).ToString("yyyy-MM-dd");
    //    TextBox6.Text = DateTime.Now.ToString("yyyy-MM-dd");
    //    CONN = new MySqlConnection(AA);
    //    CONN.Open();
    //    MySqlCommand CMD1 = new MySqlCommand();
    //    CMD1.Connection = CONN;

    //    CMD1.CommandText = @"UPDATE emp.issue_book_details
    //                        SET
    //                        return_date=@return_date,
    //                        for_days=@for_days,
    //                        fine=@fine,
    //                        issued_or_returned='RETURNED'
    //                        WHERE issue_no=@issue_no
    //                        AND issued_or_returned='ISSUED'";

    //    CMD1.Parameters.AddWithValue("@return_date", TextBox6.Text);
    //    CMD1.Parameters.AddWithValue("@for_days", TextBox4.Text);
    //    CMD1.Parameters.AddWithValue("@fine", TextBox11.Text);
    //    CMD1.Parameters.AddWithValue("@issue_no", TextBox12.Text);
        

    //    CMD1.ExecuteNonQuery();
    //    CONN.Close();
    //        TextBox1.Text = "";
    //        TextBox2.Text = "";
    //        TextBox3.Text = "";
    //        TextBox4.Text = "";
    //        TextBox5.Text = "";
    //        TextBox6.Text = DateTime.Now.ToString("yyyy-MM-dd");
    //        TextBox7.Text = "";
    //        TextBox8.Text = "";
    //        TextBox9.Text = "";
    //        TextBox10.Text = "";
    //        TextBox11.Text = "";
    //        TextBox13.Text = "";
    //    Response.Write("<script>alert('Book Returned Successfully')</script>");
    //}


    protected void Button1_Click(object sender, EventArgs e)
    {
        TextBox1.Text = "";
        TextBox2.Text = "";
        TextBox3.Text = "";
        TextBox4.Text = "";
        TextBox5.Text = "";
        TextBox6.Text = DateTime.Now.ToString("yyyy-MM-dd");
        TextBox7.Text = "";
        TextBox8.Text = "";
        TextBox9.Text = "";
        TextBox10.Text = "";
        TextBox11.Text = "";
        TextBox13.Text = "";
        TextBox12.Text = "";
        TextBox12.Focus();
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("index.aspx");
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();

        Response.Redirect("home-page.aspx");
    }
}