using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

public partial class login_page : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

protected void Button2_Click(object sender, EventArgs e)
{
    // Check username
    if (TextBox1.Text.Trim() == "")
    {
        Response.Write("<script>alert('Please Enter USERNAME 👤!')</script>");
        TextBox1.Focus();
        return;
    }

    // Check password
    if (TextBox2.Text == "")
    {
        Response.Write("<script>alert('Please Enter PASSWORD 🔑!')</script>");
        TextBox2.Focus();
        return;
    }

    string AA = ConfigurationManager.ConnectionStrings["ABC"].ConnectionString;

    using (MySqlConnection CONN = new MySqlConnection(AA))
    {
        CONN.Open();

        // First check username
        MySqlCommand checkUser = new MySqlCommand(
            "SELECT COUNT(*) FROM emp.login_table WHERE username = @username",
            CONN);

        checkUser.Parameters.AddWithValue("@username", TextBox1.Text.Trim());

        int userExists = Convert.ToInt32(checkUser.ExecuteScalar());

        // ❌ Username incorrect
        if (userExists == 0)
        {
            Response.Write("<script>alert('❌ Incorrect USERNAME')</script>");

            TextBox1.Text = "";
            TextBox2.Text = "";

            TextBox1.Focus();
            return;
        }

        // Username correct → now check password
        MySqlCommand checkPassword = new MySqlCommand(
            "SELECT COUNT(*) FROM emp.login_table WHERE username = @username AND password = @password",
            CONN);

        checkPassword.Parameters.AddWithValue("@username", TextBox1.Text.Trim());
        checkPassword.Parameters.AddWithValue("@password", TextBox2.Text);

        int loginSuccess = Convert.ToInt32(checkPassword.ExecuteScalar());

        // ✅ Username + Password correct
        if (loginSuccess > 0)
        {
            Session["UserName"] = TextBox1.Text.Trim();
            Response.Redirect("index.aspx");
        }
        else
        {
            // ❌ Password incorrect
            Response.Write("<script>alert('🔒 Incorrect PASSWORD')</script>");

            // Username stays
            // Password becomes blank
            TextBox2.Text = "";

            TextBox2.Focus();
        }
    }
}
    protected void Button1_Click(object sender, EventArgs e)
    {
        TextBox1.Text = "";
        TextBox2.Text = "";

        TextBox1.Focus();
    }

    protected void Button3_Click1(object sender, EventArgs e)
    {
        Response.Redirect("home-page.aspx");
    }
}
