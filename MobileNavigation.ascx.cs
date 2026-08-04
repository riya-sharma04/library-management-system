using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MobileNavigation : System.Web.UI.UserControl
{

    protected void Page_Load(object sender, EventArgs e)
    {
        SetNavigation();
    }


    private void SetNavigation()
    {
        bool loggedIn = Session["UserName"] != null;

        // =========================================
        // LOGGED OUT
        // =========================================

        if (!loggedIn)
        {
            lnkManageBook.Visible = false;
            lnkManageStudent.Visible = false;
            lnkManageTeacher.Visible = false;

            lnkStudentIssue.Visible = false;
            lnkTeacherIssue.Visible = false;

            lnkReturnBook.Visible = false;

            lnkIssuedRecords.Visible = false;
            lnkBookRecords.Visible = false;
            lnkStudentRecords.Visible = false;
            lnkTeacherRecords.Visible = false;
            lnkLateFee.Visible = false;
            lnkBookStatus.Visible = false;

            lnkSwitchUser.Visible = false;
            lnkSignUp.Visible = true;
            btnMobileLogout.Visible = false;

            return;
        }


        // =========================================
        // LOGGED IN
        // =========================================

        lnkManageBook.Visible = true;
        lnkManageStudent.Visible = true;
        lnkManageTeacher.Visible = true;

        lnkStudentIssue.Visible = true;
        lnkTeacherIssue.Visible = true;

        lnkReturnBook.Visible = true;

        lnkIssuedRecords.Visible = true;
        lnkBookRecords.Visible = true;
        lnkStudentRecords.Visible = true;
        lnkTeacherRecords.Visible = true;
        lnkLateFee.Visible = true;
        lnkBookStatus.Visible = true;

        lnkSwitchUser.Visible = true;
        lnkSignUp.Visible = true;
        btnMobileLogout.Visible = true;


        // =========================================
        // CURRENT PAGE
        // =========================================

        string currentPage =
            Path.GetFileName(Request.Path).ToLower();


        // =========================================
        // DETAILS
        // =========================================

        if (currentPage == "book-details.aspx")
        {
            lnkManageBook.Visible = false;
        }

        if (currentPage == "student-details.aspx")
        {
            lnkManageStudent.Visible = false;
        }

        if (currentPage == "teacher-details.aspx")
        {
            lnkManageTeacher.Visible = false;
        }


        // =========================================
        // ISSUE BOOK
        // =========================================

        if (currentPage == "student-issue-book.aspx")
        {
            lnkStudentIssue.Visible = false;
        }

        if (currentPage == "teacher-issue-book.aspx")
        {
            lnkTeacherIssue.Visible = false;
        }


        // =========================================
        // RETURN BOOK
        // =========================================

        if (currentPage == "return-book.aspx")
        {
            lnkReturnBook.Visible = false;
            grpReturnBook.Visible = false;
        }


        // =========================================
        // BOOK TOOLS
        // =========================================

        if (currentPage == "issued-book-details.aspx")
        {
            lnkIssuedRecords.Visible = false;
        }

        if (currentPage == "search-books.aspx")
        {
            lnkBookRecords.Visible = false;
        }

        if (currentPage == "search-students.aspx")
        {
            lnkStudentRecords.Visible = false;
        }

        if (currentPage == "search-teacher.aspx")
        {
            lnkTeacherRecords.Visible = false;
        }

        if (currentPage == "fine-collection.aspx")
        {
            lnkLateFee.Visible = false;
        }

        if (currentPage == "book-status.aspx")
        {
            lnkBookStatus.Visible = false;
        }
    }


    protected void btnMobileLogout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();

        Response.Redirect("~/home-page.aspx");
    }


}