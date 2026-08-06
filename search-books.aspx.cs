using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

public partial class search_books : System.Web.UI.Page
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
            if (Request.QueryString["search"] != null)
            {
                TextBox1.Text = Request.QueryString["search"];
            }

            FilterRow();
            dropdown5Value();
            
        }
    }
    public void fullTable()
    {
        string AA = ConfigurationManager.ConnectionStrings["ABC"].ConnectionString;
        CONN = new MySqlConnection(AA);
        CONN.Open();
        MySqlCommand CMD1 = new MySqlCommand();
        CMD1.Connection = CONN;

        CMD1.CommandText = @"SELECT * FROM emp.books_details ORDER BY acc_no";
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
            CMD2.CommandText = @"SELECT * FROM emp.books_details 
                                WHERE acc_no = @filter
                                   OR author = @filter
                                   OR title = @filter
                                   OR publisher = @filter 
                                   OR subject = @filter 
                                   OR location_rack = @filter 
                                   OR remarks = @filter 
                                   OR call_no = @filter 
                                   OR edition = @filter  
                                ORDER BY acc_no";
            CMD2.Parameters.AddWithValue("@filter", filterText);
        }
        else
        {
            CMD2.CommandText = "SELECT * FROM emp.books_details ORDER BY acc_no";
        }
        MySqlDataAdapter DA = new MySqlDataAdapter(CMD2);
        DataTable DT = new DataTable();
        DA.Fill(DT);
        GridView1.DataSource = DT;
        GridView1.DataBind();

        // Agar search kiya aur record nahi mila
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
    

    public void dropdown5Value()
    {
        string AA = ConfigurationManager.ConnectionStrings["ABC"].ConnectionString;
        CONN = new MySqlConnection(AA);
        MySqlCommand CMD1 = new MySqlCommand();
        CMD1.Connection = CONN;
        CMD1.CommandText = "SELECT DISTINCT subject FROM emp.books_details ORDER BY subject";

        MySqlDataAdapter DA = new MySqlDataAdapter(CMD1);


        DataSet DS = new DataSet();

        DA.Fill(DS);
        DropDownList5.Items.Clear();
        DropDownList5.Items.Add("All");
        for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
        {
            DropDownList5.Items.Add(DS.Tables[0].Rows[i][0].ToString());
        }
    }
    
    protected void DropDownList5_SelectedIndexChanged(object sender, EventArgs e)
    {

        string AA = ConfigurationManager.ConnectionStrings["ABC"].ConnectionString;
        CONN = new MySqlConnection(AA);
        CONN.Open();
        string dropdown5 = DropDownList5.SelectedValue;
        MySqlCommand CMD1 = new MySqlCommand();
        CMD1.Connection = CONN;
        if (dropdown5 == "All")
        {
            CMD1.CommandText = @"SELECT * FROM emp.books_details ORDER BY acc_no";
        }
        else
        {
            CMD1.CommandText = @"SELECT * FROM emp.books_details WHERE subject = @value ORDER BY acc_no";
            CMD1.Parameters.AddWithValue("@value", dropdown5);
        }
        
        MySqlDataAdapter DA = new MySqlDataAdapter(CMD1);
        DataTable DT = new DataTable();
        DA.Fill(DT);
        GridView1.DataSource = DT;
        GridView1.DataBind();

        CONN.Close();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteBook")
        {
            string accNo = e.CommandArgument.ToString();

            string AA = ConfigurationManager.ConnectionStrings["ABC"].ConnectionString;

            using (MySqlConnection CONN = new MySqlConnection(AA))
            {
                CONN.Open();

                MySqlCommand CMD = new MySqlCommand(
                    "DELETE FROM emp.books_details WHERE acc_no=@accno", CONN);

                CMD.Parameters.AddWithValue("@accno", accNo);

                int rows = CMD.ExecuteNonQuery();

                if (rows > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg",
                        "alert('Book record deleted successfully.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg",
                        "alert('Book record not found.');", true);
                }
            }

            // Refresh Subject dropdown
            dropdown5Value();

            // Reset filters
            DropDownList5.SelectedIndex = 0;
            TextBox1.Text = "";

            // Reload full table
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
                    "return confirm('Are you sure you want to delete this Book record?');";
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
