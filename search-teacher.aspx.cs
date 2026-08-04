using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

public partial class search_teacher : System.Web.UI.Page
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
            dropdown1Value();
            dropdown3Value();
            dropdown4Value();
            dropdown6Value();
        }
    }
    public void fullTable()
    {
        string AA = ConfigurationManager.ConnectionStrings["ABC"].ConnectionString;
        CONN = new MySqlConnection(AA);
        CONN.Open();
        MySqlCommand CMD1 = new MySqlCommand();
        CMD1.Connection = CONN;

        CMD1.CommandText = @"SELECT * FROM emp.teachers_details ORDER BY teacher_id";
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
            CMD2.CommandText = @"SELECT * FROM emp.teachers_details 
                                WHERE teacher_id = @filter
                                   OR teacher_name = @filter
                                   OR qualification = @filter
                                    OR department = @filter
                                    OR designation = @filter
                                    OR gender = @filter
                                    OR experience = @filter
                                ORDER BY teacher_id ";
            CMD2.Parameters.AddWithValue("@filter", filterText);
        }
        else
        {
            CMD2.CommandText = "SELECT * FROM emp.teachers_details ORDER BY teacher_id";
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
    public void dropdown1Value()
    {
        string AA = ConfigurationManager.ConnectionStrings["ABC"].ConnectionString;
        CONN = new MySqlConnection(AA);
        MySqlCommand CMD1 = new MySqlCommand();
        CMD1.Connection = CONN;
        CMD1.CommandText = "SELECT Distinct is_active_faculty FROM emp.teachers_details ORDER BY is_active_faculty ";

        MySqlDataAdapter DA = new MySqlDataAdapter(CMD1);


        DataSet DS = new DataSet();

        DA.Fill(DS);
        DropDownList1.Items.Clear();
        DropDownList1.Items.Add("Select");

        for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
        {
            DropDownList1.Items.Add(DS.Tables[0].Rows[i][0].ToString());

        }
    }

   

    public void dropdown3Value()
    {
        string AA = ConfigurationManager.ConnectionStrings["ABC"].ConnectionString;
        CONN = new MySqlConnection(AA);
        MySqlCommand CMD1 = new MySqlCommand();
        CMD1.Connection = CONN;
        CMD1.CommandText = "SELECT DISTINCT department FROM emp.teachers_details ORDER BY department ";

        MySqlDataAdapter DA = new MySqlDataAdapter(CMD1);


        DataSet DS = new DataSet();

        DA.Fill(DS);
        DropDownList3.Items.Clear();
        DropDownList3.Items.Add("Select");

        for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
        {
            DropDownList3.Items.Add(DS.Tables[0].Rows[i][0].ToString());

        }
    }

    public void dropdown4Value()
    {
        string AA = ConfigurationManager.ConnectionStrings["ABC"].ConnectionString;
        CONN = new MySqlConnection(AA);
        MySqlCommand CMD1 = new MySqlCommand();
        CMD1.Connection = CONN;
        CMD1.CommandText = "SELECT DISTINCT designation FROM emp.teachers_details ORDER BY designation";

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


    public void dropdown6Value()
    {
        string AA = ConfigurationManager.ConnectionStrings["ABC"].ConnectionString;
        CONN = new MySqlConnection(AA);
        MySqlCommand CMD1 = new MySqlCommand();
        CMD1.Connection = CONN;
        CMD1.CommandText = "SELECT DISTINCT gender FROM emp.teachers_details ORDER BY gender";

        MySqlDataAdapter DA = new MySqlDataAdapter(CMD1);


        DataSet DS = new DataSet();

        DA.Fill(DS);
        DropDownList6.Items.Clear();
        DropDownList6.Items.Add("Select");

        for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
        {
            DropDownList6.Items.Add(DS.Tables[0].Rows[i][0].ToString());

        }
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string AA = ConfigurationManager.ConnectionStrings["ABC"].ConnectionString;
        CONN = new MySqlConnection(AA);
        CONN.Open();
        string dropdown1 = DropDownList1.SelectedValue;
        MySqlCommand CMD1 = new MySqlCommand();
        CMD1.Connection = CONN;
        if (dropdown1 == "Select")
        {
            CMD1.CommandText = @"SELECT * FROM emp.teachers_details ORDER BY teacher_id";
        }
        else
        {
            CMD1.CommandText = @"SELECT * FROM emp.teachers_details WHERE is_active_faculty = @value ORDER BY teacher_id";
            CMD1.Parameters.AddWithValue("@value", dropdown1);
        }

        MySqlDataAdapter DA = new MySqlDataAdapter(CMD1);
        DataTable DT = new DataTable();
        DA.Fill(DT);
        GridView1.DataSource = DT;
        GridView1.DataBind();

        CONN.Close();
    }

   
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        string AA = ConfigurationManager.ConnectionStrings["ABC"].ConnectionString;
        CONN = new MySqlConnection(AA);
        CONN.Open();
        string dropdown3 = DropDownList3.SelectedValue;
        MySqlCommand CMD1 = new MySqlCommand();
        CMD1.Connection = CONN;
        if (dropdown3 == "Select")
        {
            CMD1.CommandText = @"SELECT * FROM emp.teachers_details ORDER BY teacher_id";
        }
        else
        {
            CMD1.CommandText = @"SELECT * FROM emp.teachers_details WHERE department = @value ORDER BY teacher_id";
            CMD1.Parameters.AddWithValue("@value", dropdown3);
        }

        MySqlDataAdapter DA = new MySqlDataAdapter(CMD1);
        DataTable DT = new DataTable();
        DA.Fill(DT);
        GridView1.DataSource = DT;
        GridView1.DataBind();

        CONN.Close();
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
            CMD1.CommandText = @"SELECT * FROM emp.teachers_details ORDER BY teacher_id";
        }
        else
        {
            CMD1.CommandText = @"SELECT * FROM emp.teachers_details WHERE designation = @value ORDER BY teacher_id";
            CMD1.Parameters.AddWithValue("@value", dropdown4);
        }

        MySqlDataAdapter DA = new MySqlDataAdapter(CMD1);
        DataTable DT = new DataTable();
        DA.Fill(DT);
        GridView1.DataSource = DT;
        GridView1.DataBind();

        CONN.Close();
    }

   

    protected void DropDownList6_SelectedIndexChanged(object sender, EventArgs e)
    {
        string AA = ConfigurationManager.ConnectionStrings["ABC"].ConnectionString;
        CONN = new MySqlConnection(AA);
        CONN.Open();
        string dropdown6 = DropDownList6.SelectedValue;
        MySqlCommand CMD1 = new MySqlCommand();
        CMD1.Connection = CONN;
        if (dropdown6 == "Select")
        {
            CMD1.CommandText = @"SELECT * FROM emp.teachers_details ORDER BY teacher_id";
        }
        else
        {
            CMD1.CommandText = @"SELECT * FROM emp.teachers_details WHERE gender = @value ORDER BY teacher_id";
            CMD1.Parameters.AddWithValue("@value", dropdown6);
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
        if (e.CommandName == "DeleteTeacher")
        {
            string teacherId = e.CommandArgument.ToString();

            string AA = ConfigurationManager.ConnectionStrings["ABC"].ConnectionString;

            using (MySqlConnection CONN = new MySqlConnection(AA))
            {
                CONN.Open();

                MySqlCommand CMD = new MySqlCommand(
                    "DELETE FROM emp.teachers_details WHERE teacher_id=@teacherId", CONN);

                CMD.Parameters.AddWithValue("@teacherId", teacherId);

                int rows = CMD.ExecuteNonQuery();

                if (rows > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg",
                        "alert('Teacher record deleted successfully.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "msg",
                        "alert('Teacher record not found.');", true);
                }
            }

            // Refresh dropdowns
            dropdown1Value();   // Active Faculty
            dropdown3Value();   // Department
            dropdown4Value();   // Designation
            dropdown6Value();   // Gender

            // Reset filters
            DropDownList1.SelectedIndex = 0;
            DropDownList3.SelectedIndex = 0;
            DropDownList4.SelectedIndex = 0;
            DropDownList6.SelectedIndex = 0;
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
                    "return confirm('Are you sure you want to delete this Teacher record?');";
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