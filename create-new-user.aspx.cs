using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;


public partial class create_new_user : System.Web.UI.Page
{
    string connectionString = ConfigurationManager.ConnectionStrings["ABC"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TextBox1.Focus();
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        //==========================
        // USERNAME VALIDATION
        //==========================

        if (string.IsNullOrWhiteSpace(TextBox1.Text))
        {
            Response.Write("<script>alert('Please Enter USERNAME!')</script>");
            TextBox1.Focus();
            return;
        }

        //==========================
        // PASSWORD VALIDATION
        //==========================

        if (string.IsNullOrWhiteSpace(TextBox2.Text))
        {
            Response.Write("<script>alert('Please Enter PASSWORD!')</script>");
            TextBox2.Focus();
            return;
        }

        //==========================
        // CONFIRM PASSWORD
        //==========================

        if (string.IsNullOrWhiteSpace(TextBox3.Text))
        {
            Response.Write("<script>alert('Please Enter CONFIRM PASSWORD!')</script>");
            TextBox3.Focus();
            return;
        }

        //==========================
        // PASSWORD MATCH
        //==========================

        if (TextBox2.Text != TextBox3.Text)
        {
            Response.Write("<script>alert('Password and Confirm Password do not match!')</script>");
            TextBox3.Focus();
            return;
        }

        using (MySqlConnection CONN = new MySqlConnection(connectionString))
        {
            CONN.Open();

            //=====================================
            // CHECK WHETHER USERNAME ALREADY EXISTS
            //=====================================

            MySqlCommand checkUser = new MySqlCommand();

            checkUser.Connection = CONN;

            checkUser.CommandText =
                "SELECT COUNT(*) FROM emp.login_table WHERE username=@username";

            checkUser.Parameters.AddWithValue("@username", TextBox1.Text.Trim());

            int userCount = Convert.ToInt32(checkUser.ExecuteScalar());

            if (userCount > 0)
            {
                Response.Write("<script>alert('Username already exists. Please choose another username.')</script>");

                TextBox1.Focus();

                CONN.Close();

                return;
            }

            //=============================
            // PART-2 STARTS FROM HERE
            //=============================
            //=============================
            // INSERT NEW USER
            //=============================

            MySqlCommand insertUser = new MySqlCommand();

            insertUser.Connection = CONN;

            insertUser.CommandText =
                "INSERT INTO emp.login_table(username,password) VALUES(@username,@password)";

            insertUser.Parameters.AddWithValue("@username", TextBox1.Text.Trim());

            insertUser.Parameters.AddWithValue("@password", TextBox2.Text.Trim());

            int rowsAffected = insertUser.ExecuteNonQuery();

            CONN.Close();

            if (rowsAffected > 0)
            {
                //==================================
                // AUTOMATIC LOGIN AFTER SIGNUP
                //==================================

                Session["UserName"] = TextBox1.Text.Trim();

                //==================================
                // REDIRECT TO DASHBOARD
                //==================================

                Response.Redirect("index.aspx");
            }
            else
            {
                Response.Write("<script>alert('Signup Failed! Please try again.')</script>");
            }

        }   // End of using(Connection)
    }

    //==========================
    // RESET BUTTON
    //==========================

    protected void Button1_Click(object sender, EventArgs e)
    {
        TextBox1.Text = "";
        TextBox2.Text = "";
        TextBox3.Text = "";

        TextBox1.Focus();
    }

    //==========================
    // CLOSE BUTTON
    //==========================

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