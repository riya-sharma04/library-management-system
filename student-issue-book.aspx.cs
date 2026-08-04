using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

public partial class student_issue_book : System.Web.UI.Page
{
    MySqlConnection CONN = new MySqlConnection();
    private string CS =
    ConfigurationManager.ConnectionStrings["ABC"].ConnectionString;
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
            CONN = new MySqlConnection(CS);
            
            MaxStudID();
            
        }
    }
    public void MaxStudID()
    {
        using (MySqlConnection con = new MySqlConnection(CS))
        {
            con.Open();

            MySqlCommand CMDMAX = new MySqlCommand(
                "SELECT IFNULL(MAX(issue_no),0)+1 FROM issue_book_details",
                con);

            object Result = CMDMAX.ExecuteScalar();

            TextBox1.Text = Result.ToString();
        }
    }
    //public void MaxStudID()
    //{
    //    if (CONN.State == ConnectionState.Closed)
    //        CONN.Open();

    //    MySqlCommand CMDMAX = new MySqlCommand();

    //    CMDMAX.Connection = CONN;

    //    CMDMAX.CommandText =
    //    "SELECT IFNULL(MAX(issue_no),0)+1 FROM issue_book_details";

    //    object Result = CMDMAX.ExecuteScalar();

    //    TextBox1.Text = Result.ToString();

    //    CONN.Close();
    //}

    protected void TextBox2_TextChanged(object sender, EventArgs e)
    {
        string cs = ConfigurationManager.ConnectionStrings["ABC"].ConnectionString;

        using (MySqlConnection con = new MySqlConnection(cs))
        {
            con.Open();

            MySqlCommand cmd = new MySqlCommand(
            @"SELECT author,
                 title,
                 publisher,
                 subject,
                 price
          FROM books_details
          WHERE acc_no=@acc", con);

            cmd.Parameters.AddWithValue("@acc", TextBox2.Text.Trim());

            MySqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                TextBox3.Text = dr["author"].ToString();
                TextBox4.Text = dr["title"].ToString();
                TextBox5.Text = dr["publisher"].ToString();
                TextBox6.Text = dr["subject"].ToString();
                TextBox7.Text = dr["price"].ToString();
            }
            else
            {
                Response.Write("<script>alert('Book not found.');</script>");

                TextBox3.Text = "";
                TextBox4.Text = "";
                TextBox5.Text = "";
                TextBox6.Text = "";
                TextBox7.Text = "";

                TextBox2.Focus();
            }
        }
    }
    protected void TextBox10_TextChanged(object sender, EventArgs e)
    {
        string cs = ConfigurationManager.ConnectionStrings["ABC"].ConnectionString;

        using (MySqlConnection con = new MySqlConnection(cs))
        {
            con.Open();

            MySqlCommand cmd = new MySqlCommand(
            @"SELECT
            branch_class,
            uni_reg_no,
            student_name,
            sem_year,
            email_id,
            contact_no
          FROM students_details
          WHERE clg_roll_no=@roll", con);

            cmd.Parameters.AddWithValue("@roll", TextBox10.Text.Trim());

            MySqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                TextBox11.Text = dr["branch_class"].ToString();
                TextBox12.Text = dr["uni_reg_no"].ToString();
                TextBox13.Text = dr["student_name"].ToString();
                TextBox14.Text = dr["sem_year"].ToString();
                TextBox15.Text = dr["email_id"].ToString();
                TextBox16.Text = dr["contact_no"].ToString();
            }
            else
            {
                Response.Write("<script>alert('Student not found.');</script>");

                TextBox11.Text = "";
                TextBox12.Text = "";
                TextBox13.Text = "";
                TextBox14.Text = "";
                TextBox15.Text = "";
                TextBox16.Text = "";

                TextBox10.Focus();
            }
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
//--------------- BASIC VALIDATION ---------------//


if (TextBox1.Text.Trim() == "")
        {
            Response.Write("<script>alert('Please Enter ISSUE NO!');</script>");
            TextBox1.Focus();
            return;
        }

        if (TextBox2.Text.Trim() == "")
        {
            Response.Write("<script>alert('Please Enter ACCOUNT NO!');</script>");
            TextBox2.Focus();
            return;
        }

        if (TextBox8.Text.Trim() == "")
        {
            Response.Write("<script>alert('Please Enter FOR DAYS!');</script>");
            TextBox8.Focus();
            return;
        }
        int forDays;

        if (!int.TryParse(TextBox8.Text.Trim(), out forDays) || forDays <= 0)
        {
            Response.Write("<script>alert('Please enter valid number of days!');</script>");
            TextBox8.Focus();
            return;
        }

        if (TextBox9.Text.Trim() == "")
        {
            Response.Write("<script>alert('Please FILL ISSUE DATE!');</script>");
            TextBox9.Focus();
            return;
        }

        DateTime issueDate;

        if (!DateTime.TryParse(TextBox9.Text.Trim(), out issueDate))
        {
            Response.Write("<script>alert('Please enter a valid Issue Date!');</script>");
            TextBox9.Focus();
            return;
        }

        DateTime dueDate = issueDate.AddDays(forDays);

        if (TextBox10.Text.Trim() == "")
        {
            Response.Write("<script>alert('Please Enter CLG ROLL NO!');</script>");
            TextBox10.Focus();
            return;
        }


        //--------------- CHECK BOOK EXISTS ---------------//

        using (MySqlConnection con = new MySqlConnection(CS))
        {
            con.Open();

            MySqlCommand cmdBook = new MySqlCommand(
                "SELECT COUNT(*) FROM books_details WHERE acc_no=@acc", con);

            cmdBook.Parameters.AddWithValue("@acc", TextBox2.Text.Trim());

            int bookCount = Convert.ToInt32(cmdBook.ExecuteScalar());

            if (bookCount == 0)
            {
                Response.Write("<script>alert('Book not found. Please enter a valid Account No.');</script>");
                TextBox2.Focus();
                return;
            }


            //--------------- CHECK BOOK ALREADY ISSUED ---------------//

            MySqlCommand cmdIssued = new MySqlCommand(
                @"SELECT COUNT(*) 
          FROM issue_book_details 
          WHERE acc_no=@acc 
          AND issued_or_returned='ISSUED'", con);

            cmdIssued.Parameters.AddWithValue("@acc", TextBox2.Text.Trim());

            int issuedCount = Convert.ToInt32(cmdIssued.ExecuteScalar());

            if (issuedCount > 0)
            {
                Response.Write("<script>alert('This book is currently not available. It has already been issued.');</script>");
                TextBox2.Focus();
                return;
            }


            //--------------- CHECK STUDENT EXISTS ---------------//

            MySqlCommand cmdStudent = new MySqlCommand(
                "SELECT COUNT(*) FROM students_details WHERE clg_roll_no=@roll", con);

            cmdStudent.Parameters.AddWithValue("@roll", TextBox10.Text.Trim());

            int studentCount = Convert.ToInt32(cmdStudent.ExecuteScalar());

            if (studentCount == 0)
            {
                Response.Write("<script>alert('Student not found. Please enter a valid College Roll No.');</script>");
                TextBox10.Focus();
                return;
            }


            //--------------- CHECK ISSUE NO ---------------//

            MySqlCommand cmdIssue = new MySqlCommand(
                "SELECT COUNT(*) FROM issue_book_details WHERE issue_no=@issueNo", con);

            cmdIssue.Parameters.AddWithValue("@issueNo", TextBox1.Text.Trim());

            int issueCount = Convert.ToInt32(cmdIssue.ExecuteScalar());

            if (issueCount > 0)
            {
                Response.Write("<script>alert('This Issue No already exists.');</script>");
                return;
            }


            //--------------- INSERT RECORD ---------------//

            MySqlCommand cmdInsert = new MySqlCommand(
                @"INSERT INTO issue_book_details
        (
            issue_no,
            acc_no,
            author,
            title,
            publisher,
            subject,
            price,
            for_days,
            issue_date,
            due_date,
            `clgRoll_or_teacherID`,
            `branch_or_dept`,
            uni_reg_no,
            `student_or_teacher_name`,
            sem_year,
            email_id,
            contact_no,
            member_type,
            `issued_or_returned`,
            fine
        )
        VALUES
        (
            @issueNo,
            @acc,
            @author,
            @title,
            @publisher,
            @subject,
            @price,
            @forDays,
            @issueDate,
            @dueDate,
            @roll,
            @branch,
            @uniReg,
            @studentName,
            @semYear,
            @email,
            @contact,
            'STUDENT',
            'ISSUED',
            '0.00'
        )", con);


            cmdInsert.Parameters.AddWithValue("@issueNo", TextBox1.Text.Trim());
            cmdInsert.Parameters.AddWithValue("@acc", TextBox2.Text.Trim());
            cmdInsert.Parameters.AddWithValue("@author", TextBox3.Text.Trim());
            cmdInsert.Parameters.AddWithValue("@title", TextBox4.Text.Trim());
            cmdInsert.Parameters.AddWithValue("@publisher", TextBox5.Text.Trim());
            cmdInsert.Parameters.AddWithValue("@subject", TextBox6.Text.Trim());
            cmdInsert.Parameters.AddWithValue("@price", TextBox7.Text.Trim());
            cmdInsert.Parameters.AddWithValue("@forDays", forDays);
            cmdInsert.Parameters.AddWithValue("@issueDate", issueDate);
            cmdInsert.Parameters.AddWithValue("@dueDate", dueDate);

            cmdInsert.Parameters.AddWithValue("@roll", TextBox10.Text.Trim());
            cmdInsert.Parameters.AddWithValue("@branch", TextBox11.Text.Trim());
            cmdInsert.Parameters.AddWithValue("@uniReg", TextBox12.Text.Trim());
            cmdInsert.Parameters.AddWithValue("@studentName", TextBox13.Text.Trim());
            cmdInsert.Parameters.AddWithValue("@semYear", TextBox14.Text.Trim());
            cmdInsert.Parameters.AddWithValue("@email", TextBox15.Text.Trim());
            cmdInsert.Parameters.AddWithValue("@contact", TextBox16.Text.Trim());

            cmdInsert.ExecuteNonQuery();
        }


        //--------------- SUCCESS ---------------//

        Response.Write("<script>alert('Book issued successfully.');</script>");

        ClearData();

        MaxStudID();

        TextBox2.Focus();


}

    
    private void ClearData()
    {
        TextBox2.Text = "";
        TextBox3.Text = "";
        TextBox4.Text = "";
        TextBox5.Text = "";
        TextBox6.Text = "";
        TextBox7.Text = "";
        TextBox8.Text = "";
        TextBox9.Text = "";
        TextBox10.Text = "";
        TextBox11.Text = "";
        TextBox12.Text = "";
        TextBox13.Text = "";
        TextBox14.Text = "";
        TextBox15.Text = "";
        TextBox16.Text = "";
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        ClearData();

        

        TextBox2.Focus();
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