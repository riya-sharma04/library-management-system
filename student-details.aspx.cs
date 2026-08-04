using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

public partial class student_details : System.Web.UI.Page
{
    MySqlConnection CONN = new MySqlConnection();

    public bool IsUpdate
    {
        get
        {
            return ViewState["IsUpdate"] != null &&
                   (bool)ViewState["IsUpdate"];
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
    //public void MaxStudID()
    //{
    //    string AA = ConfigurationManager.ConnectionStrings["ABC"].ConnectionString;
    //    CONN = new MySqlConnection(AA);

    //    MySqlCommand CMDMAX = new MySqlCommand();
    //    CMDMAX.Connection = CONN;
    //    CMDMAX.CommandText = "select Max(clg_roll_no)+1 from students_details";

    //    MySqlDataAdapter DA = new MySqlDataAdapter(CMDMAX);
    //    DataSet DS = new DataSet();

    //    DA.Fill(DS);

    //    if (DS.Tables[0].Rows[0][0] == DBNull.Value)
    //    {
    //        TextBox1.Text = "1992025001";
    //    }
    //    else
    //    {
    //        TextBox1.Text = DS.Tables[0].Rows[0][0].ToString();
    //    }
    //}
    public void MaxStudID()
    {
        string AA = ConfigurationManager.ConnectionStrings["ABC"].ConnectionString;
        CONN = new MySqlConnection(AA);

        try
        {
            CONN.Open();

            // ================= COLLEGE ROLL NO =================

            MySqlCommand CMDROLL = new MySqlCommand();

            CMDROLL.Connection = CONN;

            CMDROLL.CommandText = @"
            SELECT MAX(CAST(SUBSTRING_INDEX(clg_roll_no, '-', -1) AS UNSIGNED))
            FROM students_details
            WHERE clg_roll_no LIKE 'VLB25-%'";

            object rollResult = CMDROLL.ExecuteScalar();

            int nextRoll = 1;

            if (rollResult != null && rollResult != DBNull.Value)
            {
                nextRoll = Convert.ToInt32(rollResult) + 1;
            }

            TextBox1.Text = "VLB25-" + nextRoll.ToString("D3");


            // ================= REGISTRATION NO =================

            MySqlCommand CMDREG = new MySqlCommand();

            CMDREG.Connection = CONN;

            CMDREG.CommandText = @"
            SELECT MAX(CAST(SUBSTRING(uni_reg_no, 12) AS UNSIGNED))
            FROM students_details
            WHERE uni_reg_no LIKE 'VLBREG2025%'";

            object regResult = CMDREG.ExecuteScalar();

            int nextReg = 1;

            if (regResult != null && regResult != DBNull.Value)
            {
                nextReg = Convert.ToInt32(regResult) + 1;
            }

            TextBox2.Text = "VLBREG2025" + nextReg.ToString("D4");
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('Error: " + ex.Message.Replace("'", "\\'") + "')</script>");
        }
        finally
        {
            if (CONN.State == ConnectionState.Open)
            {
                CONN.Close();
            }
        }
    }


    protected void Button2_Click(object sender, EventArgs e)
    {
        string AA = ConfigurationManager.ConnectionStrings["ABC"].ConnectionString;

        CONN = new MySqlConnection(AA);

        
            if (TextBox1.Text == "")
            {
                Response.Write("<script>alert('Please Enter COLLEGE ROLL NO!')</script>");
                TextBox1.Focus();
                return;
            }
            if (TextBox2.Text == "")
            {
                Response.Write("<script>alert('Please Enter UNIVERSITY REGISTRATION NO!')</script>");
                TextBox2.Focus();
                return;
            }
            if (TextBox3.Text == "")
            {
                Response.Write("<script>alert('Please Enter STUDENT NAME!')</script>");
                TextBox3.Focus();
                return;
            }
            if (TextBox4.Text == "")
            {
                Response.Write("<script>alert('Please Enter BRANCH/CLASS!')</script>");
                TextBox4.Focus();
                return;
            }
            if (TextBox5.Text == "")
            {
                Response.Write("<script>alert('Please Enter SEM/YEAR!')</script>");
                TextBox5.Focus();
                return;
            }
            if (TextBox6.Text == "")
            {
                Response.Write("<script>alert('Please Enter FATHER'S NAME!')</script>");
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
            string isActive = "no";
            if (CheckBox1.Checked)
            {
                isActive = "yes";
            }
            string GENDER = "";
            if (RadioButton1.Checked)
            {
                GENDER = "male";
            }
            else if(RadioButton2.Checked)
            {
                GENDER = "female";
            }
            else
            {
                Response.Write("<script>alert('Please Select Gender!')</script>");
                return;
            }
        CONN.Open();
        if (IsUpdate == true)
        {
            MySqlCommand CMD = new MySqlCommand();

            CMD.Connection = CONN;

            CMD.CommandText = @"UPDATE emp.students_details SET
                        uni_reg_no=@uni_reg_no,
                        student_name=@student_name,
                        branch_class=@branch_class,
                        sem_year=@sem_year,
                        fathers_name=@fathers_name,
                        address=@address,
                        dob=@dob,
                        email_id=@email_id,
                        contact_no=@contact_no,
                        gender=@gender,
                        is_active=@is_active
                        WHERE clg_roll_no=@clg_roll_no";

            CMD.Parameters.AddWithValue("@clg_roll_no", TextBox1.Text);
            CMD.Parameters.AddWithValue("@uni_reg_no", TextBox2.Text);
            CMD.Parameters.AddWithValue("@student_name", TextBox3.Text);
            CMD.Parameters.AddWithValue("@branch_class", TextBox4.Text);
            CMD.Parameters.AddWithValue("@sem_year", TextBox5.Text);
            CMD.Parameters.AddWithValue("@fathers_name", TextBox6.Text);
            CMD.Parameters.AddWithValue("@address", TextBox7.Text);
            CMD.Parameters.AddWithValue("@dob", TextBox8.Text);
            CMD.Parameters.AddWithValue("@email_id", TextBox9.Text);
            CMD.Parameters.AddWithValue("@contact_no", TextBox10.Text);
            CMD.Parameters.AddWithValue("@gender", GENDER);
            CMD.Parameters.AddWithValue("@is_active", isActive);

            CMD.ExecuteNonQuery();

            Response.Write("<script>alert('✅ Student Updated Successfully')</script>");
        }


        // ================= INSERT =================

        else
        {
            // Duplicate Roll No Check

            MySqlCommand CHECK = new MySqlCommand();

            CHECK.Connection = CONN;

            CHECK.CommandText = "SELECT * FROM emp.students_details WHERE clg_roll_no=@roll";

            CHECK.Parameters.AddWithValue("@roll", TextBox1.Text);

            MySqlDataAdapter DA = new MySqlDataAdapter(CHECK);

            DataSet DS = new DataSet();

            DA.Fill(DS);

            if (DS.Tables[0].Rows.Count > 0)
            {
                Response.Write("<script>alert('THIS COLLEGE ROLL NO ALREADY EXISTS!')</script>");

                CONN.Close();

                return;
            }

            MySqlCommand CMD = new MySqlCommand();

            CMD.Connection = CONN;

            CMD.CommandText = @"INSERT INTO emp.students_details
                       (clg_roll_no,
                        uni_reg_no,
                        student_name,
                        branch_class,
                        sem_year,
                        fathers_name,
                        address,
                        dob,
                        email_id,
                        contact_no,
                        gender,
                        is_active)

                        VALUES

                       (@clg_roll_no,
                        @uni_reg_no,
                        @student_name,
                        @branch_class,
                        @sem_year,
                        @fathers_name,
                        @address,
                        @dob,
                        @email_id,
                        @contact_no,
                        @gender,
                        @is_active)";

            CMD.Parameters.AddWithValue("@clg_roll_no", TextBox1.Text);
            CMD.Parameters.AddWithValue("@uni_reg_no", TextBox2.Text);
            CMD.Parameters.AddWithValue("@student_name", TextBox3.Text);
            CMD.Parameters.AddWithValue("@branch_class", TextBox4.Text);
            CMD.Parameters.AddWithValue("@sem_year", TextBox5.Text);
            CMD.Parameters.AddWithValue("@fathers_name", TextBox6.Text);
            CMD.Parameters.AddWithValue("@address", TextBox7.Text);
            CMD.Parameters.AddWithValue("@dob", TextBox8.Text);
            CMD.Parameters.AddWithValue("@email_id", TextBox9.Text);
            CMD.Parameters.AddWithValue("@contact_no", TextBox10.Text);
            CMD.Parameters.AddWithValue("@gender", GENDER);
            CMD.Parameters.AddWithValue("@is_active", isActive);

            CMD.ExecuteNonQuery();

            Response.Write("<script>alert('✅ Student Added Successfully')</script>");
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

        ScriptManager.RegisterStartupScript
            (
            this,
            this.GetType(),
            "focus",
            "document.getElementById('" + TextBox2.ClientID + "').focus();",
            true
            );
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
        SearchStudent();
    }

    public void SearchStudent()
    {
        string AA = ConfigurationManager.ConnectionStrings["ABC"].ConnectionString;

        CONN = new MySqlConnection(AA);

        MySqlCommand CMD = new MySqlCommand();

        CMD.Connection = CONN;

        CMD.CommandText = "SELECT * FROM emp.students_details WHERE clg_roll_no=@roll";

        CMD.Parameters.AddWithValue("@roll", TextBox11.Text);

        MySqlDataAdapter DA = new MySqlDataAdapter(CMD);

        DataSet DS = new DataSet();

        DA.Fill(DS);

        if (DS.Tables[0].Rows.Count > 0)
        {
            TextBox1.Text = DS.Tables[0].Rows[0]["clg_roll_no"].ToString();
            TextBox2.Text = DS.Tables[0].Rows[0]["uni_reg_no"].ToString();
            TextBox3.Text = DS.Tables[0].Rows[0]["student_name"].ToString();
            TextBox4.Text = DS.Tables[0].Rows[0]["branch_class"].ToString();
            TextBox5.Text = DS.Tables[0].Rows[0]["sem_year"].ToString();
            TextBox6.Text = DS.Tables[0].Rows[0]["fathers_name"].ToString();
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

            RadioButton1.Checked = (gender == "male");
            RadioButton2.Checked = (gender == "female");

            string isActive = DS.Tables[0].Rows[0]["is_active"].ToString();

            CheckBox1.Checked = (isActive == "yes");

            // IMPORTANT
            IsUpdate = true;
        }
        else
        {
            Response.Write("<script>alert('❌ No Record Found!')</script>");
        }
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();

        Response.Redirect("home-page.aspx");
    }
}