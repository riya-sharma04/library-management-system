using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;

public partial class loginpage2 : System.Web.UI.Page
{
    MySqlConnection CONN = new MySqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadStatistics();
        }
    }
    public void LoadStatistics()
    {
        string AA = ConfigurationManager.ConnectionStrings["ABC"].ConnectionString;

        CONN = new MySqlConnection(AA);

        CONN.Open();

        MySqlCommand CMD = new MySqlCommand();

        CMD.Connection = CONN;

        // Books

        CMD.CommandText = "SELECT COUNT(*) FROM books_details";

        lblBooks.Text = CMD.ExecuteScalar().ToString();

        // Students

        CMD.CommandText = "SELECT COUNT(*) FROM students_details";

        lblStudents.Text = CMD.ExecuteScalar().ToString();

        // Teachers

        CMD.CommandText = "SELECT COUNT(*) FROM teachers_details";

        lblTeachers.Text = CMD.ExecuteScalar().ToString();

        // Issued Books

        CMD.CommandText = "SELECT COUNT(*) FROM issue_book_details WHERE issued_or_returned='ISSUED'";

        lblIssuedBooks.Text = CMD.ExecuteScalar().ToString();

        CONN.Close();
    }
    protected void btnGetStarted_Click(object sender, EventArgs e)
    {
        Response.Redirect("login-page.aspx");
    }
}