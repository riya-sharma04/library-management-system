<%@ Page Language="C#" AutoEventWireup="true" CodeFile="issued-book-details.aspx.cs" Inherits="issued_book_details" %>
<%@ Register Src="~/MobileNavigation.ascx"
    TagPrefix="uc1"
    TagName="MobileNavigation" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="StyleSheet1.css" rel="stylesheet" type="text/css" />
<style></style>
</head>
    
<body>
    <form id="form1" runat="server">
        <uc1:MobileNavigation
    ID="MobileNavigation1"
    runat="server" />
    <div id="background-image"></div>
  <header>
    <span id="logo">
          <asp:Image ID="Image1" runat="server" ImageUrl="~/images/logo.png" /></span>
          <span class="heading">VIDHYANT LIBRARY</span>
    <nav>
      <ul>
          <li><asp:HyperLink ID="HyperLink1" runat="server" Text="Hyperlink" NavigateUrl="~/index.aspx" CssClass="active">⌂ Home</asp:HyperLink></li>
          <li>
            <div class="dropdown">
                <span>Details</span>
                <div class="dropdown-content">
                    <asp:HyperLink ID="HyperLink2" runat="server" Text="Hyperlink" NavigateUrl="~/book-details.aspx">Manage Book</asp:HyperLink>
                    <asp:HyperLink ID="HyperLink3" runat="server" Text="Hyperlink" NavigateUrl="~/student-details.aspx">Manage Student </asp:HyperLink>
                    <asp:HyperLink ID="HyperLink4" runat="server" Text="Hyperlink" NavigateUrl="~/teacher-details.aspx">Manage Teacher</asp:HyperLink>
                </div>
            </div>
          </li>
          <li>
            <div class="dropdown">
                <span>Issue Book</span>
                <div class="dropdown-content">
                    <asp:HyperLink ID="HyperLink5" runat="server" Text="Hyperlink" NavigateUrl="~/student-issue-book.aspx">Student </asp:HyperLink>
                    <asp:HyperLink ID="HyperLink6" runat="server" Text="Hyperlink" NavigateUrl="~/teacher-issue-book.aspx">Teacher </asp:HyperLink>
                </div>
            </div>
          </li>
          <li><asp:HyperLink ID="HyperLink7" runat="server" Text="Hyperlink" NavigateUrl="~/return-book.aspx">Return Book</asp:HyperLink></li>
          <li>
              <div class="dropdown">
                <span>Book Tools</span>
                <div class="dropdown-content">
                    <asp:HyperLink ID="HyperLink9" runat="server" Text="Hyperlink" NavigateUrl="~/search-books.aspx">Book Records</asp:HyperLink>
                    <asp:HyperLink ID="HyperLink10" runat="server" Text="Hyperlink" NavigateUrl="~/search-students.aspx">Student Records</asp:HyperLink>
                    <asp:HyperLink ID="HyperLink16" runat="server" Text="Hyperlink" NavigateUrl="~/search-teacher.aspx">Teacher Records</asp:HyperLink>
                    <asp:HyperLink ID="HyperLink14" runat="server" Text="Hyperlink" NavigateUrl="~/fine-collection.aspx">Late Fee</asp:HyperLink>
                    <asp:HyperLink ID="HyperLink11" runat="server" Text="Hyperlink" NavigateUrl="~/Book-status.aspx">Book Status</asp:HyperLink>
                    
                </div>
            </div>
          </li>
          <li>
      <div class="dropdown no-arrow">
          <span class="menu-icon"> ☰ </span>
          <div class="dropdown-content">
              <asp:HyperLink ID="HyperLink18" runat="server" Text="Hyperlink" NavigateUrl="~/Switch_user.aspx">Switch User</asp:HyperLink>
              <asp:HyperLink ID="HyperLink17" runat="server" Text="Hyperlink" NavigateUrl="~/create-new-user.aspx">Sign UP</asp:HyperLink>
              <asp:LinkButton ID="btnLogout" runat="server" CssClass="logout-link" OnClientClick="return confirmLogout();" OnClick="btnLogout_Click">Logout</asp:LinkButton>
                   
          </div>
      </div>
</li>
              
        </ul>
    </nav>
  </header>
                <div class="welcome-card">

    <asp:Label ID="lblUserName" runat="server" CssClass="welcome-label"> </asp:Label>
    <asp:Label ID="lblUserName1" runat="server" CssClass="welcome-label1"> </asp:Label>
    <br/> 
    <asp:Label ID="lblUserName2" runat="server" CssClass="welcome-label2"> </asp:Label>

</div>
        <asp:Panel ID="Panel1" runat="server" >
            <h2 class="head">📚📤 ISSUED BOOK RECORDS </h2>
             <div class="container">
                 <asp:Label ID="Label2" runat="server" Text="SEARCH" CssClass="label" Width="90px"></asp:Label>

<div class="search-wrapper">

    <asp:TextBox ID="TextBox1"
        runat="server"
        TextMode="Search"
        CssClass="search-box"
        Placeholder="Search by Issue No, Acc No, Name..."
        AutoPostBack="True"
        OnTextChanged="TextBox1_TextChanged">
    </asp:TextBox>

    <asp:LinkButton ID="btnSearch"
        runat="server"
        CssClass="search-icon"
        OnClick="btnSearch_Click">
        🔍
    </asp:LinkButton>

</div>
                 <asp:Label ID="Label1" runat="server" Text="Member Type" CssClass="label" Width="120px"></asp:Label>
                 &nbsp<asp:DropDownList ID="DropDownList1" runat="server" CssClass="DD" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                    <asp:ListItem>Select</asp:ListItem>
                     <asp:ListItem>STUDENT</asp:ListItem>
                     <asp:ListItem>TEACHER</asp:ListItem>
                 </asp:DropDownList>
              <div class="grid-container">  
                 <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
                     <Columns>
                         <asp:BoundField DataField="issue_no" HeaderText="Issue No"/>

                         <asp:BoundField DataField="acc_no" HeaderText="Acc No"/>
                         <asp:BoundField DataField="title" HeaderText="Book Title"/>
                         <asp:BoundField DataField="author" HeaderText="Author"/>
                         <asp:BoundField DataField="publisher" HeaderText="Publisher"/>
                         <asp:BoundField DataField="subject" HeaderText="Subject"/>
                         <asp:BoundField DataField="price" HeaderText="Price"/>

                         <asp:BoundField DataField="student_or_teacher_name" HeaderText="Name"/>
                         <asp:BoundField DataField="clgRoll_or_teacherID" HeaderText="Roll No / Teacher ID"/>
                         <asp:BoundField DataField="uni_reg_no" HeaderText="University Reg No"/>
                         <asp:BoundField DataField="branch_or_dept" HeaderText="Branch / Department"/>

                         <asp:BoundField DataField="issue_date" HeaderText="Date" DataFormatString="{0:dd-MM-yyyy}"/>
                     </Columns>
                 </asp:GridView>
                  </div>
             </div>
        </asp:Panel>
    </form>
          <footer class="footer">
    © 2026 Vidhyant Library | Library Management System| All Rights Reserved.
</footer>
    <script src="JavaScript3.js"></script>
</body>
</html>
