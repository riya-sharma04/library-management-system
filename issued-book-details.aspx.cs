using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

public partial class issued_book_details : System.Web.UI.Page
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
            FilterRow();
        }
    }
    public void FilterRow()
    {
        string AA = ConfigurationManager.ConnectionStrings["ABC"].ConnectionString;

        using (MySqlConnection CONN = new MySqlConnection(AA))
        {
            CONN.Open();

            string memberType = DropDownList1.SelectedValue;
            string search = TextBox1.Text.Trim();

            MySqlCommand CMD = new MySqlCommand();
            CMD.Connection = CONN;

            CMD.CommandText = @"SELECT * FROM emp.issue_book_details
                            WHERE issued_or_returned='issued'";

            if (memberType != "Select")
            {
                CMD.CommandText += " AND member_type=@memberType";
                CMD.Parameters.AddWithValue("@memberType", memberType);
            }

            if (!string.IsNullOrEmpty(search))
            {
                CMD.CommandText += @" AND
            (
                issue_no=@search OR
                acc_no=@search OR
                title=@search OR
                author=@search OR
                publisher=@search OR
                subject=@search OR
                student_or_teacher_name=@search OR
                clgRoll_or_teacherID=@search OR
                uni_reg_no=@search OR
                branch_or_dept=@search
            )";

                CMD.Parameters.AddWithValue("@search", search);
            }

            CMD.CommandText += " ORDER BY issue_no";

            MySqlDataAdapter DA = new MySqlDataAdapter(CMD);
            DataTable DT = new DataTable();
            DA.Fill(DT);

            // Search me record nahi mila
            if (!string.IsNullOrEmpty(search) && DT.Rows.Count == 0)
            {
                TextBox1.Text = "";

                CMD.Parameters.Clear();
                CMD.CommandText = @"SELECT * FROM emp.issue_book_details
                                WHERE issued_or_returned='issued'";

                if (memberType != "Select")
                {
                    CMD.CommandText += " AND member_type=@memberType";
                    CMD.Parameters.AddWithValue("@memberType", memberType);
                }

                CMD.CommandText += " ORDER BY issue_no";

                DA = new MySqlDataAdapter(CMD);
                DT = new DataTable();
                DA.Fill(DT);

                ScriptManager.RegisterStartupScript(this, GetType(), "msg",
                    "alert('No record found.');", true);
            }

            GridView1.DataSource = DT;
            GridView1.DataBind();
        }
    }

    
    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
        FilterRow();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FilterRow();
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        FilterRow();
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();

        Response.Redirect("home-page.aspx");
    }
}