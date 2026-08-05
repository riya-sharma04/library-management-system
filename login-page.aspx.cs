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
    // Username validation
    if (TextBox1.Text == "")
    {
        Response.Write("<script>alert('Please Enter USERNAME 👤!');</script>");
        TextBox1.Focus();
        return;
    }

    // Password validation
    if (TextBox2.Text == "")
    {
        Response.Write("<script>alert('Please Enter PASSWORD 🔑!');</script>");
        TextBox2.Focus();
        return;
    }

    string AA = ConfigurationManager.ConnectionStrings["ABC"].ConnectionString;

    using (MySqlConnection CONN = new MySqlConnection(AA))
    {
        CONN.Open();

        // Check username
        MySqlCommand checkUser = new MySqlCommand(
            "SELECT COUNT(*) FROM emp.login_table WHERE username = @username",
            CONN);

        checkUser.Parameters.AddWithValue("@username", TextBox1.Text);

        int userExists = Convert.ToInt32(checkUser.ExecuteScalar());

        // ❌ Incorrect username
        if (userExists == 0)
        {
            TextBox1.Text = "";

            Response.Write("<script>alert('❌ Incorrect USERNAME');</script>");

            TextBox1.Focus();
            return;
        }

        // Check username + password
        MySqlCommand checkLogin = new MySqlCommand(
            "SELECT COUNT(*) FROM emp.login_table WHERE username = @username AND password = @password",
            CONN);

        checkLogin.Parameters.AddWithValue("@username", TextBox1.Text);
        checkLogin.Parameters.AddWithValue("@password", TextBox2.Text);

        int loginValid = Convert.ToInt32(checkLogin.ExecuteScalar());

        // ❌ Incorrect password
        if (loginValid == 0)
        {
            TextBox2.Text = "";

            Response.Write("<script>alert('🔒 Incorrect PASSWORD');</script>");

            TextBox2.Focus();
            return;
        }

        // ✅ Login successful
        Session["UserName"] = TextBox1.Text;
        Response.Redirect("index.aspx");
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
