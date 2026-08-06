using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;


public partial class fee_collection : System.Web.UI.Page
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
            fullTable();
        }
    }
    public void fullTable()
    {
        string AA = ConfigurationManager.ConnectionStrings["ABC"].ConnectionString;
        CONN = new MySqlConnection(AA);
        CONN.Open();
        MySqlCommand CMD1 = new MySqlCommand();
        CMD1.Connection = CONN;

        CMD1.CommandText = @"SELECT * FROM emp.issue_book_details WHERE member_type = 'student' ORDER BY issue_no";
        MySqlDataAdapter DA = new MySqlDataAdapter(CMD1);
        DataTable DT = new DataTable();
        DA.Fill(DT);
        GridView1.DataSource = DT;
        GridView1.DataBind();

        CONN.Close();
    }
    public void FilterRow()
    {
        string AA = ConfigurationManager.ConnectionStrings["ABC"].ConnectionString;
        CONN = new MySqlConnection(AA);
        CONN.Open();
        string filterText = TextBox1.Text.Trim();
        MySqlCommand CMD2 = new MySqlCommand();
        CMD2.Connection = CONN;
        if (!string.IsNullOrEmpty(filterText))
        {
            CMD2.CommandText = @"SELECT * FROM emp.issue_book_details
                    WHERE
                    (
                        acc_no = @filter
                        OR clgRoll_or_teacherID = @filter
                        OR issue_no = @filter
                        OR title = @filter
                        OR student_or_teacher_name = @filter
                    )
                    AND member_type = 'student'
                    ORDER BY issue_no";
            CMD2.Parameters.AddWithValue("@filter", filterText);
        }
        else
        {
             fullTable();
             CONN.Close();
             return;
        }
        MySqlDataAdapter DA = new MySqlDataAdapter(CMD2);
        DataTable DT = new DataTable();
        DA.Fill(DT);
        GridView1.DataSource = DT;
        GridView1.DataBind();
        if (!string.IsNullOrEmpty(filterText) && DT.Rows.Count == 0)
        {
            // TextBox clear
            TextBox1.Text = "";

            // Full table load
            fullTable();

            // Alert show
            ScriptManager.RegisterStartupScript(this, GetType(), "msg",
                "alert('No record found.');", true);

            CONN.Close();
            return;
        }
        CONN.Close();
    }
    protected void TextBox1_TextChanged1(object sender, EventArgs e)
    {
        FilterRow();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FilterRow();

        // Search using keyword
    }
    public void IssueStatus()
    {
        string AA = ConfigurationManager.ConnectionStrings["ABC"].ConnectionString;
        CONN = new MySqlConnection(AA);
        CONN.Open();
        string issueStatus = DropDownList2.SelectedValue;
        MySqlCommand CMD1 = new MySqlCommand();
        CMD1.Connection = CONN;
        if (DropDownList2.SelectedIndex == 0)
        {
            CMD1.CommandText = "SELECT * FROM emp.issue_book_details WHERE member_type = 'student' ORDER BY issue_no";
        }
        else    
        {
            CMD1.CommandText = @"SELECT * FROM emp.issue_book_details WHERE issued_or_returned = @status AND member_type = 'student' ORDER BY issue_no";
            CMD1.Parameters.AddWithValue("@status", issueStatus);
        }
        MySqlDataAdapter DA = new MySqlDataAdapter(CMD1);
        DataTable DT = new DataTable();
        DA.Fill(DT);
        GridView1.DataSource = DT;
        GridView1.DataBind();

        CONN.Close();
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        IssueStatus();
        
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteRecord")
        {
            string issueNo = e.CommandArgument.ToString();

            string AA = ConfigurationManager.ConnectionStrings["ABC"].ConnectionString;

            using (MySqlConnection CONN = new MySqlConnection(AA))
            {
                CONN.Open();

                MySqlCommand CMD = new MySqlCommand(
                    "DELETE FROM emp.issue_book_details WHERE issue_no=@issueNo", CONN);

                CMD.Parameters.AddWithValue("@issueNo", issueNo);

                int rows = CMD.ExecuteNonQuery();

                if (rows > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg",
                        "alert('Record deleted successfully.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg",
                        "alert('Record not found.');", true);
                }
            }
            DropDownList2.SelectedIndex = 0;
            TextBox1.Text = "";

            // Reload full grid
            fullTable();
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");

            if (btnDelete != null)
            {
                btnDelete.OnClientClick =
                    "return confirm('Are you sure you want to delete this record?');";
            }
        }
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();

        Response.Redirect("home-page.aspx");
    }
}
