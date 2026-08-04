using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;


public partial class search_students : System.Web.UI.Page
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
            FilterRow(); 
            dropdown4Value();
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

        CMD1.CommandText = @"SELECT * FROM emp.students_details ORDER BY clg_roll_no";
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
            CMD2.CommandText = @"SELECT * FROM emp.students_details 
                                WHERE clg_roll_no = @filter
                                   OR uni_reg_no = @filter
                                   OR student_name = @filter
                                   OR gender = @filter 
                                    OR branch_class = @filter
                                    OR sem_year = @filter
                                    OR fathers_name = @filter
                                ORDER BY clg_roll_no";
            CMD2.Parameters.AddWithValue("@filter", filterText);
        }
        else
        {
            CMD2.CommandText = "SELECT * FROM emp.students_details ORDER BY clg_roll_no";
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
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();

        Response.Redirect("home-page.aspx");
    }
    

    public void dropdown4Value()
    {
        string AA = ConfigurationManager.ConnectionStrings["ABC"].ConnectionString;
        CONN = new MySqlConnection(AA);
        MySqlCommand CMD1 = new MySqlCommand();
        CMD1.Connection = CONN;
        CMD1.CommandText = "SELECT DISTINCT branch_class FROM emp.students_details ORDER BY branch_class";

        MySqlDataAdapter DA = new MySqlDataAdapter(CMD1);


        DataSet DS = new DataSet();

        DA.Fill(DS);
        DropDownList4.Items.Clear();
        DropDownList4.Items.Add("Select");

        for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
        {
            DropDownList4.Items.Add(DS.Tables[0].Rows[i][0].ToString());
        }
        
    }

    public void dropdown5Value()
    {
        string AA = ConfigurationManager.ConnectionStrings["ABC"].ConnectionString;
        CONN = new MySqlConnection(AA);
        MySqlCommand CMD1 = new MySqlCommand();
        CMD1.Connection = CONN;
        CMD1.CommandText = "SELECT DISTINCT sem_year FROM emp.students_details ORDER BY sem_year";

        MySqlDataAdapter DA = new MySqlDataAdapter(CMD1);


        DataSet DS = new DataSet();

        DA.Fill(DS);
        DropDownList5.Items.Clear();
        DropDownList5.Items.Add("Select");

        for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
        {
            DropDownList5.Items.Add(DS.Tables[0].Rows[i][0].ToString());
        }
        
    }
   

    protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
    {
        string AA = ConfigurationManager.ConnectionStrings["ABC"].ConnectionString;
        CONN = new MySqlConnection(AA);
        CONN.Open();
        string dropdown4 = DropDownList4.SelectedValue;
        MySqlCommand CMD1 = new MySqlCommand();
        CMD1.Connection = CONN;
        if (dropdown4 == "Select")
        {
            CMD1.CommandText = @"SELECT * FROM emp.students_details ORDER BY clg_roll_no";
        }
        else
        {
            CMD1.CommandText = @"SELECT * FROM emp.students_details WHERE branch_class = @value ORDER BY clg_roll_no";
            CMD1.Parameters.AddWithValue("@value", dropdown4);
        }

        MySqlDataAdapter DA = new MySqlDataAdapter(CMD1);
        DataTable DT = new DataTable();
        DA.Fill(DT);
        GridView1.DataSource = DT;
        GridView1.DataBind();

        CONN.Close();
    }

    protected void DropDownList5_SelectedIndexChanged(object sender, EventArgs e)
    {
        string AA = ConfigurationManager.ConnectionStrings["ABC"].ConnectionString;
        CONN = new MySqlConnection(AA);
        CONN.Open();
        string dropdown5 = DropDownList5.SelectedValue;
        MySqlCommand CMD1 = new MySqlCommand();
        CMD1.Connection = CONN;
        if (dropdown5 == "Select")
        {
            CMD1.CommandText = @"SELECT * FROM emp.students_details ORDER BY clg_roll_no";
        }
        else
        {
            CMD1.CommandText = @"SELECT * FROM emp.students_details WHERE sem_year = @value ORDER BY clg_roll_no";
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
        if (e.CommandName == "DeleteStudent")
        {
            string rollNo = e.CommandArgument.ToString();

            string AA = ConfigurationManager.ConnectionStrings["ABC"].ConnectionString;

            using (MySqlConnection CONN = new MySqlConnection(AA))
            {
                CONN.Open();

                MySqlCommand CMD = new MySqlCommand(
                    "DELETE FROM emp.students_details WHERE clg_roll_no=@rollno", CONN);

                CMD.Parameters.AddWithValue("@rollno", rollNo);


                int rows = CMD.ExecuteNonQuery();
                if (rows > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg",
                        "alert('Student record deleted successfully.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg",
                        "alert('Student record not found.');", true);
                }

            }

            // Refresh Dropdowns
        dropdown4Value();   // Branch
        dropdown5Value();   // Semester

            DropDownList4.SelectedIndex = 0;
            DropDownList5.SelectedIndex = 0;
            TextBox1.Text = "";

            // Load full table
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
                    "return confirm('Are you sure you want to delete this student record?');";
            }
        }
    }

}