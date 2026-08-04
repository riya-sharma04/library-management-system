using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

public partial class book_details : System.Web.UI.Page
{
    MySqlConnection CONN = new MySqlConnection();
    public bool IsUpdate
    {
        get
        {
            return ViewState["IsUpdate"] != null && (bool)ViewState["IsUpdate"];
        }
        set
        {
            ViewState["IsUpdate"] = value;
        }
    }

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
            string AA = ConfigurationManager.ConnectionStrings["ABC"].ConnectionString;
            CONN = new MySqlConnection(AA);
            MaxStudID();
        }
    }
    public void MaxStudID()
    {
        string AA = ConfigurationManager.ConnectionStrings["ABC"].ConnectionString;

        using (MySqlConnection con = new MySqlConnection(AA))
        {
            con.Open();

            MySqlCommand cmd = new MySqlCommand(
                @"SELECT acc_no
              FROM books_details
              WHERE acc_no LIKE 'VLB-BK-%'
              ORDER BY CAST(SUBSTRING(acc_no, 8) AS UNSIGNED) DESC
              LIMIT 1", con);

            object result = cmd.ExecuteScalar();

            int nextNumber = 1;

            if (result != null && result != DBNull.Value)
            {
                string lastAccNo = result.ToString();

                if (lastAccNo.Length >= 8)
                {
                    int lastNumber;

                    if (int.TryParse(lastAccNo.Substring(7), out lastNumber))
                    {
                        nextNumber = lastNumber + 1;
                    }
                }
            }

            TextBox1.Text = "VLB-BK-" + nextNumber.ToString("D4");
        }
    }
    //public void MaxStudID()
    //{
    //    string AA = ConfigurationManager.ConnectionStrings["ABC"].ConnectionString;
    //    CONN = new MySqlConnection(AA);
    //    MySqlCommand CMDMAX = new MySqlCommand();
    //    CMDMAX.Connection = CONN;
    //    CMDMAX.CommandText = "select Max(acc_no)+1 from books_details";
    //    MySqlDataAdapter DA = new MySqlDataAdapter(CMDMAX);
    //    DataSet DS = new DataSet();

    //    DA.Fill(DS);

    //    if (DS.Tables[0].Rows[0][0] == DBNull.Value)
    //    {
    //        TextBox1.Text = "1001";
    //    }
    //    else
    //    {
    //        TextBox1.Text = DS.Tables[0].Rows[0][0].ToString();
    //    }
    //}

    protected void Button2_Click(object sender, EventArgs e)
    {
        string AA = ConfigurationManager.ConnectionStrings["ABC"].ConnectionString;
        CONN = new MySqlConnection(AA);


        // Validation

        if (TextBox1.Text == "")
        {
            Response.Write("<script>alert('Please Enter Acc No 🔢!')</script>");
            TextBox1.Focus();
            return;
        }

        if (TextBox2.Text == "")
        {
            Response.Write("<script>alert('Please Enter Author Name ✍️!')</script>");
            TextBox2.Focus();
            return;
        }

        if (TextBox3.Text == "")
        {
            Response.Write("<script>alert('Please Enter Title 📖!')</script>");
            TextBox3.Focus();
            return;
        }

        if (TextBox4.Text == "")
        {
            Response.Write("<script>alert('Please Enter Publisher Name 🏢!')</script>");
            TextBox4.Focus();
            return;
        }

        if (TextBox5.Text == "")
        {
            Response.Write("<script>alert('Please Enter Subject 🧠!')</script>");
            TextBox5.Focus();
            return;
        }

        if (TextBox6.Text == "")
        {
            Response.Write("<script>alert('Please Enter Location/Rack 📍!')</script>");
            TextBox6.Focus();
            return;
        }

        if (TextBox7.Text == "")
        {
            Response.Write("<script>alert('Please Enter Remarks 💬!')</script>");
            TextBox7.Focus();
            return;
        }

        if (TextBox9.Text == "")
        {
            Response.Write("<script>alert('Please Enter Call No 📞!')</script>");
            TextBox9.Focus();
            return;
        }

        if (TextBox10.Text == "")
        {
            Response.Write("<script>alert('Please Enter Edition 🆕!')</script>");
            TextBox10.Focus();
            return;
        }

        if (TextBox11.Text == "")
        {
            Response.Write("<script>alert('Please Enter Price 💰!')</script>");
            TextBox11.Focus();
            return;
        }

        if (TextBox12.Text == "")
        {
            Response.Write("<script>alert('Please Enter Year 📅!')</script>");
            TextBox12.Focus();
            return;
        }

        if (TextBox13.Text == "")
        {
            Response.Write("<script>alert('Please Enter No Of Copy 📚!')</script>");
            TextBox13.Focus();
            return;
        }

        if (TextBox14.Text == "")
        {
            Response.Write("<script>alert('Please Enter Quantity 🔢!')</script>");
            TextBox14.Focus();
            return;
        }



        CONN.Open();


        // UPDATE EXISTING BOOK
        if (IsUpdate == true)
        {

            MySqlCommand CMD = new MySqlCommand();

            CMD.Connection = CONN;

            CMD.CommandText = @"UPDATE emp.books_details SET
        author=@author,
        title=@title,
        publisher=@publisher,
        subject=@subject,
        location_rack=@location,
        remarks=@remarks,
        call_no=@call,
        edition=@edition,
        price=@price,
        year=@year,
        no_of_copy=@copy,
        qty=@qty
        WHERE acc_no=@acc_no";


            CMD.Parameters.AddWithValue("@acc_no", TextBox1.Text);
            CMD.Parameters.AddWithValue("@author", TextBox2.Text);
            CMD.Parameters.AddWithValue("@title", TextBox3.Text);
            CMD.Parameters.AddWithValue("@publisher", TextBox4.Text);
            CMD.Parameters.AddWithValue("@subject", TextBox5.Text);
            CMD.Parameters.AddWithValue("@location", TextBox6.Text);
            CMD.Parameters.AddWithValue("@remarks", TextBox7.Text);
            CMD.Parameters.AddWithValue("@call", TextBox9.Text);
            CMD.Parameters.AddWithValue("@edition", TextBox10.Text);
            CMD.Parameters.AddWithValue("@price", TextBox11.Text);
            CMD.Parameters.AddWithValue("@year", TextBox12.Text);
            CMD.Parameters.AddWithValue("@copy", TextBox13.Text);
            CMD.Parameters.AddWithValue("@qty", TextBox14.Text);


            CMD.ExecuteNonQuery();


            Response.Write("<script>alert('✅ Book Updated Successfully')</script>");

        }


        // INSERT NEW BOOK
        else
        {

            MySqlCommand CMD = new MySqlCommand();

            CMD.Connection = CONN;

            CMD.CommandText = @"INSERT INTO emp.books_details
        (acc_no,author,title,publisher,subject,
        location_rack,remarks,call_no,edition,
        price,year,no_of_copy,qty)

        VALUES
        (@acc_no,@author,@title,@publisher,@subject,
        @location,@remarks,@call,@edition,
        @price,@year,@copy,@qty)";


            CMD.Parameters.AddWithValue("@acc_no", TextBox1.Text);
            CMD.Parameters.AddWithValue("@author", TextBox2.Text);
            CMD.Parameters.AddWithValue("@title", TextBox3.Text);
            CMD.Parameters.AddWithValue("@publisher", TextBox4.Text);
            CMD.Parameters.AddWithValue("@subject", TextBox5.Text);
            CMD.Parameters.AddWithValue("@location", TextBox6.Text);
            CMD.Parameters.AddWithValue("@remarks", TextBox7.Text);
            CMD.Parameters.AddWithValue("@call", TextBox9.Text);
            CMD.Parameters.AddWithValue("@edition", TextBox10.Text);
            CMD.Parameters.AddWithValue("@price", TextBox11.Text);
            CMD.Parameters.AddWithValue("@year", TextBox12.Text);
            CMD.Parameters.AddWithValue("@copy", TextBox13.Text);
            CMD.Parameters.AddWithValue("@qty", TextBox14.Text);


            CMD.ExecuteNonQuery();


            Response.Write("<script>alert('✅ Book Added Successfully')</script>");

        }


        CONN.Close();


        // Reset page after save/update

        TextBox2.Text = "";
        TextBox3.Text = "";
        TextBox4.Text = "";
        TextBox5.Text = "";
        TextBox6.Text = "";
        TextBox7.Text = "";
        TextBox8.Text = "";
        TextBox9.Text = "";
        TextBox10.Text = "";
        TextBox11.Text = "";
        TextBox12.Text = "";
        TextBox13.Text = "";
        TextBox14.Text = "";

        IsUpdate = false;


        // Generate new Acc No
        MaxStudID();


        // Focus back to Author textbox
        ScriptManager.RegisterStartupScript(this, this.GetType(), "focus",
        "document.getElementById('" + TextBox2.ClientID + "').focus();", true);
    }
       
       
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("index.aspx");
    }

protected void Button1_Click(object sender, EventArgs e)
    {
        // Clear all book details
        TextBox2.Text = "";
        TextBox3.Text = "";
        TextBox4.Text = "";
        TextBox5.Text = "";
        TextBox6.Text = "";
        TextBox7.Text = "";
        TextBox8.Text = "";
        TextBox9.Text = "";
        TextBox10.Text = "";
        TextBox11.Text = "";
        TextBox12.Text = "";
        TextBox13.Text = "";
        TextBox14.Text = "";

        // Exit Update Mode
        IsUpdate = false;

        // Generate new Acc No
        MaxStudID();

        // Focus on Author textbox
        ScriptManager.RegisterStartupScript(this, this.GetType(), "focus",
            "document.getElementById('" + TextBox2.ClientID + "').focus();", true);
    }

    protected void TextBox8_TextChanged(object sender, EventArgs e)
    {
        SearchBook();
    }
    public void SearchBook()
    {
        string AA = ConfigurationManager.ConnectionStrings["ABC"].ConnectionString;
        CONN = new MySqlConnection(AA);
        MySqlCommand CMD1 = new MySqlCommand();
        CMD1.Connection = CONN;
        CMD1.CommandText = "SELECT* FROM emp.books_details where acc_no =@acc_no";
        CMD1.Parameters.AddWithValue("@acc_no", TextBox8.Text.Trim());


        MySqlDataAdapter DA = new MySqlDataAdapter(CMD1);

        DataSet DS = new DataSet();

        DA.Fill(DS);
        if (DS.Tables[0].Rows.Count > 0)
        {
            TextBox1.Text = (DS.Tables[0].Rows[0].ItemArray[0].ToString());
            TextBox2.Text = (DS.Tables[0].Rows[0].ItemArray[1].ToString());
            TextBox3.Text = (DS.Tables[0].Rows[0].ItemArray[2].ToString());
            TextBox4.Text = (DS.Tables[0].Rows[0].ItemArray[3].ToString());
            TextBox5.Text = (DS.Tables[0].Rows[0].ItemArray[4].ToString());
            TextBox6.Text = (DS.Tables[0].Rows[0].ItemArray[5].ToString());
            TextBox7.Text = (DS.Tables[0].Rows[0].ItemArray[6].ToString());
            TextBox9.Text = (DS.Tables[0].Rows[0].ItemArray[7].ToString());
            TextBox10.Text = (DS.Tables[0].Rows[0].ItemArray[8].ToString());
            TextBox11.Text = (DS.Tables[0].Rows[0].ItemArray[9].ToString());
            TextBox13.Text = (DS.Tables[0].Rows[0].ItemArray[11].ToString());
            TextBox14.Text = (DS.Tables[0].Rows[0].ItemArray[12].ToString());
            IsUpdate = true;
            if (!Convert.IsDBNull(DS.Tables[0].Rows[0]["year"]))
            {
                DateTime year = Convert.ToDateTime(DS.Tables[0].Rows[0]["year"]);
                TextBox12.Text = year.ToString("yyyy-MM-dd"); // HTML5 date format
            }
            else
            {
                TextBox12.Text = "";
            }
        }
        else
        {
            Response.Write("<script>alert('❌ No Record Found!')</script>");
            return;

        }

    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();

        Response.Redirect("home-page.aspx");
    }
}