using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class index : System.Web.UI.Page
{
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
    }
    protected void searchBtn_Click(object sender, EventArgs e)
    {
        SearchBook();
    }

    protected void searchInput_TextChanged(object sender, EventArgs e)
    {
        SearchBook();
    }

    private void SearchBook()
    {
        Response.Redirect("search-books.aspx?search=" +
            Server.UrlEncode(searchInput.Text.Trim()));
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();

        Response.Redirect("home-page.aspx");
    }

}