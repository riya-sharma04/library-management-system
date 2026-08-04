using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

public partial class teacher_issue_book : System.Web.UI.Page
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
            string AA = ConfigurationManager.ConnectionStrings["ABC"].ConnectionString;
            CONN = new MySqlConnection(AA);
            MaxStudID();
        }
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();

        Response.Redirect("home-page.aspx");
    }
    public void MaxStudID()
    {
        using (MySqlConnection con = new MySqlConnection(
            ConfigurationManager.ConnectionStrings["ABC"].ConnectionString))
        {
            con.Open();

            MySqlCommand cmd = new MySqlCommand(
                "SELECT IFNULL(MAX(issue_no),0)+1 FROM issue_book_details", con);

            object result = cmd.ExecuteScalar();

            TextBox1.Text = result.ToString();
        }
    }
   
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
    protected void TextBox9_TextChanged(object sender, EventArgs e)
    {
        string cs = ConfigurationManager.ConnectionStrings["ABC"].ConnectionString;

        using (MySqlConnection con = new MySqlConnection(cs))
        {
            con.Open();

            MySqlCommand cmd = new MySqlCommand(
            @"SELECT
            teacher_name,
            department,
            email_id,
            contact_no
          FROM teachers_details
          WHERE teacher_id=@teacherID", con);

            cmd.Parameters.AddWithValue("@teacherID", TextBox9.Text.Trim());

            MySqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                TextBox10.Text = dr["teacher_name"].ToString();
                TextBox11.Text = dr["department"].ToString();
                TextBox12.Text = dr["email_id"].ToString();
                TextBox13.Text = dr["contact_no"].ToString();
            }
            else
            {
                Response.Write("<script>alert('Teacher not found.');</script>");

                TextBox10.Text = "";
                TextBox11.Text = "";
                TextBox12.Text = "";
                TextBox13.Text = "";

                TextBox9.Focus();
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
            Response.Write("<script>alert('Please FILL ISSUE DATE!');</script>");
            TextBox8.Focus();
            return;
        }

        if (TextBox9.Text.Trim() == "")
        {
            Response.Write("<script>alert('Please ENTER TEACHER ID!');</script>");
            TextBox9.Focus();
            return;
        }

        if (TextBox14.Text.Trim() == "")
        {
            Response.Write("<script>alert('Please ENTER FOR DAYS!');</script>");
            TextBox14.Focus();
            return;
        }

        int forDays;

        if (!int.TryParse(TextBox14.Text.Trim(), out forDays) || forDays <= 0)
        {
            Response.Write("<script>alert('For Days must be a valid number greater than 0.');</script>");
            TextBox14.Focus();
            return;
        }

        DateTime issueDate;

        if (!DateTime.TryParse(TextBox8.Text.Trim(), out issueDate))
        {
            Response.Write("<script>alert('Please enter a valid Issue Date.');</script>");
            TextBox8.Focus();
            return;
        }

        //--------------- CALCULATE DUE DATE ---------------//

        DateTime dueDate = issueDate.AddDays(forDays);


        using (MySqlConnection con = new MySqlConnection(
            ConfigurationManager.ConnectionStrings["ABC"].ConnectionString))
        {
            con.Open();


            //--------------- CHECK BOOK EXISTS ---------------//

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


            //--------------- CHECK TEACHER EXISTS ---------------//

            MySqlCommand cmdTeacher = new MySqlCommand(
                "SELECT COUNT(*) FROM teachers_details WHERE teacher_id=@teacherID", con);

            cmdTeacher.Parameters.AddWithValue("@teacherID", TextBox9.Text.Trim());

            int teacherCount = Convert.ToInt32(cmdTeacher.ExecuteScalar());

            if (teacherCount == 0)
            {
                Response.Write("<script>alert('Teacher not found. Please enter a valid Teacher ID.');</script>");
                TextBox9.Focus();
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


            //--------------- INSERT TEACHER ISSUE RECORD ---------------//

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
            `student_or_teacher_name`,
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
            @teacherID,
            @department,
            @teacherName,
            @email,
            @contact,
            'TEACHER',
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

            cmdInsert.Parameters.AddWithValue("@teacherID", TextBox9.Text.Trim());
            cmdInsert.Parameters.AddWithValue("@department", TextBox11.Text.Trim());
            cmdInsert.Parameters.AddWithValue("@teacherName", TextBox10.Text.Trim());
            cmdInsert.Parameters.AddWithValue("@email", TextBox12.Text.Trim());
            cmdInsert.Parameters.AddWithValue("@contact", TextBox13.Text.Trim());

            cmdInsert.ExecuteNonQuery();
        }


        //--------------- SUCCESS ---------------//

        Response.Write("<script>alert('Book issued successfully to teacher.');</script>");

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
}