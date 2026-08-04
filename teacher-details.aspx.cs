using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

public partial class teacher_details : System.Web.UI.Page
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
                @"SELECT teacher_id
              FROM teachers_details
              WHERE teacher_id LIKE 'VLB-TCH-%'
              ORDER BY CAST(SUBSTRING(teacher_id, 9) AS UNSIGNED) DESC
              LIMIT 1", con);

            object result = cmd.ExecuteScalar();

            int nextNumber = 1;

            if (result != null && result != DBNull.Value)
            {
                string lastID = result.ToString();

                // VLB-TCH- = 8 characters
                if (lastID.StartsWith("VLB-TCH-") && lastID.Length > 8)
                {
                    string numberPart = lastID.Substring(8);

                    int lastNumber;

                    if (int.TryParse(numberPart, out lastNumber))
                    {
                        nextNumber = lastNumber + 1;
                    }
                }
            }

            TextBox1.Text = "VLB-TCH-" + nextNumber.ToString("D4");
        }
    }
    //public void MaxStudID()
    //{
    //    string AA = ConfigurationManager.ConnectionStrings["ABC"].ConnectionString;

    //    CONN = new MySqlConnection(AA);

    //    MySqlCommand CMDMAX = new MySqlCommand();

    //    CMDMAX.Connection = CONN;

    //    CMDMAX.CommandText = "select Max(teacher_id)+1 from teachers_details";

    //    MySqlDataAdapter DA = new MySqlDataAdapter(CMDMAX);

    //    DataSet DS = new DataSet();

    //    DA.Fill(DS);

    //    if (DS.Tables[0].Rows[0][0] == DBNull.Value)
    //    {
    //        TextBox1.Text = "101";
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


        if (TextBox1.Text == "")
        {
            Response.Write("<script>alert('Please Enter TEACHER ID!')</script>");
            TextBox1.Focus();
            return;
        }
        if (TextBox2.Text == "")
        {
            Response.Write("<script>alert('Please Enter TEACHER NAME!')</script>");
            TextBox2.Focus();
            return;
        }
        if (TextBox3.Text == "")
        {
            Response.Write("<script>alert('Please Enter DEPARTMENT!')</script>");
            TextBox3.Focus();
            return;
        }
        if (TextBox4.Text == "")
        {
            Response.Write("<script>alert('Please Enter DESIGNATION!')</script>");
            TextBox4.Focus();
            return;
        }
        if (TextBox5.Text == "")
        {
            Response.Write("<script>alert('Please Enter EXPERIENCE(YEAR)!')</script>");
            TextBox5.Focus();
            return;
        }
        if (TextBox6.Text == "")
        {
            Response.Write("<script>alert('Please Enter QUALIFICATION!')</script>");
            TextBox6.Focus();
            return;
        }
        if (TextBox7.Text == "")
        {
            Response.Write("<script>alert('Please Enter YOUR ADDRESS!')</script>");
            TextBox7.Focus();
            return;
        }

        if (TextBox8.Text == "")
        {
            Response.Write("<script>alert('Please Enter YOUR DATE OF BIRTH!')</script>");
            TextBox8.Focus();
            return;
        }
        if (TextBox9.Text == "")
        {
            Response.Write("<script>alert('Please Enter YOUR EMAIL ID!')</script>");
            TextBox9.Focus();
            return;
        }
        if (TextBox10.Text == "")
        {
            Response.Write("<script>alert('Please Enter YOUR CONTACT NO!')</script>");
            TextBox10.Focus();
            return;
        }
        string isActive = "NO";
        if (CheckBox1.Checked)
        {
            isActive = "YES";
        }
        string GENDER = "";
        if (RadioButton1.Checked)
        {
            GENDER = "MALE";
        }
        else if (RadioButton2.Checked)
        {
            GENDER = "FEMALE";
        }
        else
        {
            Response.Write("<script>alert('Please Select Gender!')</script>");
            return;
        }
        CONN.Open();

        // ================= UPDATE =================

        if (IsUpdate == true)
        {
            MySqlCommand CMD = new MySqlCommand();

            CMD.Connection = CONN;

            CMD.CommandText = @"UPDATE emp.teachers_details SET
                        teacher_name=@teacher_name,
                        department=@department,
                        designation=@designation,
                        experience=@experience,
                        qualification=@qualification,
                        address=@address,
                        dob=@dob,
                        email_id=@email_id,
                        contact_no=@contact_no,
                        gender=@gender,
                        is_active_faculty=@is_active
                        WHERE teacher_id=@teacher_id";

            CMD.Parameters.AddWithValue("@teacher_id", TextBox1.Text);
            CMD.Parameters.AddWithValue("@teacher_name", TextBox2.Text);
            CMD.Parameters.AddWithValue("@department", TextBox3.Text);
            CMD.Parameters.AddWithValue("@designation", TextBox4.Text);
            CMD.Parameters.AddWithValue("@experience", TextBox5.Text);
            CMD.Parameters.AddWithValue("@qualification", TextBox6.Text);
            CMD.Parameters.AddWithValue("@address", TextBox7.Text);
            CMD.Parameters.AddWithValue("@dob", TextBox8.Text);
            CMD.Parameters.AddWithValue("@email_id", TextBox9.Text);
            CMD.Parameters.AddWithValue("@contact_no", TextBox10.Text);
            CMD.Parameters.AddWithValue("@gender", GENDER);
            CMD.Parameters.AddWithValue("@is_active", isActive);

            CMD.ExecuteNonQuery();

            Response.Write("<script>alert('✅ Teacher Updated Successfully')</script>");
        }


        // ================= INSERT =================

        else
        {
            // Duplicate Teacher ID Check

            MySqlCommand CHECK = new MySqlCommand();

            CHECK.Connection = CONN;

            CHECK.CommandText = "SELECT * FROM emp.teachers_details WHERE teacher_id=@teacher_id";

            CHECK.Parameters.AddWithValue("@teacher_id", TextBox1.Text);

            MySqlDataAdapter DA = new MySqlDataAdapter(CHECK);

            DataSet DS = new DataSet();

            DA.Fill(DS);

            if (DS.Tables[0].Rows.Count > 0)
            {
                Response.Write("<script>alert('THIS TEACHER ID ALREADY EXISTS!')</script>");

                CONN.Close();

                return;
            }

            MySqlCommand CMD = new MySqlCommand();

            CMD.Connection = CONN;

            CMD.CommandText = @"INSERT INTO emp.teachers_details
                       (
                        teacher_id,
                        teacher_name,
                        department,
                        designation,
                        experience,
                        qualification,
                        address,
                        dob,
                        email_id,
                        contact_no,
                        gender,
                        is_active_faculty
                       )

                       VALUES
                       (
                        @teacher_id,
                        @teacher_name,
                        @department,
                        @designation,
                        @experience,
                        @qualification,
                        @address,
                        @dob,
                        @email_id,
                        @contact_no,
                        @gender,
                        @is_active
                       )";

            CMD.Parameters.AddWithValue("@teacher_id", TextBox1.Text);
            CMD.Parameters.AddWithValue("@teacher_name", TextBox2.Text);
            CMD.Parameters.AddWithValue("@department", TextBox3.Text);
            CMD.Parameters.AddWithValue("@designation", TextBox4.Text);
            CMD.Parameters.AddWithValue("@experience", TextBox5.Text);
            CMD.Parameters.AddWithValue("@qualification", TextBox6.Text);
            CMD.Parameters.AddWithValue("@address", TextBox7.Text);
            CMD.Parameters.AddWithValue("@dob", TextBox8.Text);
            CMD.Parameters.AddWithValue("@email_id", TextBox9.Text);
            CMD.Parameters.AddWithValue("@contact_no", TextBox10.Text);
            CMD.Parameters.AddWithValue("@gender", GENDER);
            CMD.Parameters.AddWithValue("@is_active", isActive);

            CMD.ExecuteNonQuery();

            Response.Write("<script>alert('✅ Teacher Added Successfully')</script>");
        }

        CONN.Close();


        // ================= RESET =================

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

        RadioButton1.Checked = false;
        RadioButton2.Checked = false;
        CheckBox1.Checked = false;

        IsUpdate = false;

        MaxStudID();

        ScriptManager.RegisterStartupScript(
            this,
            this.GetType(),
            "focus",
            "document.getElementById('" + TextBox2.ClientID + "').focus();",
            true);
    }



    

    protected void Button1_Click(object sender, EventArgs e)
    
{
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
        RadioButton1.Checked = false;
        RadioButton2.Checked = false;
        CheckBox1.Checked = false;
    IsUpdate = false;

    MaxStudID();
}

    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("index.aspx");
    }
protected void TextBox11_TextChanged(object sender, EventArgs e)
{
    SearchTeacher();
}
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();

        Response.Redirect("home-page.aspx");
    }
    public void SearchTeacher()
{
    string AA = ConfigurationManager.ConnectionStrings["ABC"].ConnectionString;

    CONN = new MySqlConnection(AA);

    MySqlCommand CMD = new MySqlCommand();

    CMD.Connection = CONN;

    CMD.CommandText = "SELECT * FROM emp.teachers_details WHERE teacher_id=@teacher_id";

    CMD.Parameters.AddWithValue("@teacher_id", TextBox11.Text);

    MySqlDataAdapter DA = new MySqlDataAdapter(CMD);

    DataSet DS = new DataSet();

    DA.Fill(DS);

    if (DS.Tables[0].Rows.Count > 0)
    {
        TextBox1.Text = DS.Tables[0].Rows[0]["teacher_id"].ToString();
        TextBox2.Text = DS.Tables[0].Rows[0]["teacher_name"].ToString();
        TextBox3.Text = DS.Tables[0].Rows[0]["department"].ToString();
        TextBox4.Text = DS.Tables[0].Rows[0]["designation"].ToString();
        TextBox5.Text = DS.Tables[0].Rows[0]["experience"].ToString();
        TextBox6.Text = DS.Tables[0].Rows[0]["qualification"].ToString();
        TextBox7.Text = DS.Tables[0].Rows[0]["address"].ToString();

        if (!Convert.IsDBNull(DS.Tables[0].Rows[0]["dob"]))
        {
            DateTime dob = Convert.ToDateTime(DS.Tables[0].Rows[0]["dob"]);
            TextBox8.Text = dob.ToString("yyyy-MM-dd");
        }
        else
        {
            TextBox8.Text = "";
        }

        TextBox9.Text = DS.Tables[0].Rows[0]["email_id"].ToString();
        TextBox10.Text = DS.Tables[0].Rows[0]["contact_no"].ToString();

        string gender = DS.Tables[0].Rows[0]["gender"].ToString();

        if (gender == "MALE")
        {
            RadioButton1.Checked = true;
            RadioButton2.Checked = false;
        }
        else if (gender == "FEMALE")
        {
            RadioButton1.Checked = false;
            RadioButton2.Checked = true;
        }

        string isActive = DS.Tables[0].Rows[0]["is_active_faculty"].ToString();

        if (isActive == "YES")
        {
            CheckBox1.Checked = true;
        }
        else
        {
            CheckBox1.Checked = false;
        }

        // Enable Update Mode
        IsUpdate = true;
    }
    else
    {
        Response.Write("<script>alert('❌ No Record Found!')</script>");
    }
}

    
}