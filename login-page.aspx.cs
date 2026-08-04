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
        MySqlConnection CONN = new MySqlConnection();
        string AA = ConfigurationManager.ConnectionStrings["ABC"].ConnectionString;
        CONN = new MySqlConnection(AA);

        MySqlCommand CMD = new MySqlCommand();
        CMD.Connection = CONN;
        CMD.CommandText = "SELECT* FROM emp.login_table WHERE username = '" + TextBox1.Text + "' AND password = '" + TextBox2.Text + "'";

        MySqlDataAdapter DA = new MySqlDataAdapter();
        DA.SelectCommand = CMD;

        DataSet DS = new DataSet();

        DA.Fill(DS);
        if (TextBox1.Text == "")
        {
            Response.Write("<script>alert('Please Enter USERNAME 👤!')</script>");
            TextBox1.Focus();
            return;
        }
        if (TextBox2.Text == "")
        {
            Response.Write("<script>alert('Please Enter PASSSWORD 🔑!')</script>");
            TextBox2.Focus();
            return;
        }

        // ✅ If both filled, now check for validity
        if (DS.Tables[0].Rows.Count > 0)
        {
            string userName = TextBox1.Text;
            // Set the session variable
            Session["UserName"] = userName;
            Response.Redirect("index.aspx");
        }
        else
        {
            // ✅ Check if username exists (additional check)
            CONN.Open(); // <--- Add this
            MySqlCommand checkUser = new MySqlCommand("SELECT COUNT(*) FROM emp.login_table WHERE username = @username", CONN);
            checkUser.Parameters.AddWithValue("@username", TextBox1.Text);
            int userExists = Convert.ToInt32(checkUser.ExecuteScalar());
            CONN.Close(); // <--- Add this

            if (userExists == 0)
            {
                Response.Write("<script>alert(' ❌ Incorrect USERNAME')</script>");
            }
            else
            {
                Response.Write("<script>alert(' 🔒 Incorrect PASSWORD')</script>");
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
