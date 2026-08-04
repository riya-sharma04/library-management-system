<%@ Page Language="C#" AutoEventWireup="true" CodeFile="student-issue-book.aspx.cs" Inherits="student_issue_book" %>
<%@ Register Src="~/MobileNavigation.ascx"
    TagPrefix="uc1"
    TagName="MobileNavigation" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="https://code.jquery.com/jquery-latest.min.js"></script>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="StyleSheet2.css" rel="stylesheet" type="text/css" />

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
                    <asp:HyperLink ID="HyperLink2" runat="server" Text="Hyperlink" NavigateUrl="~/book-details.aspx">Manage Book </asp:HyperLink>
                    <asp:HyperLink ID="HyperLink3" runat="server" Text="Hyperlink" NavigateUrl="~/student-details.aspx">Manage Student</asp:HyperLink>
                    <asp:HyperLink ID="HyperLink4" runat="server" Text="Hyperlink" NavigateUrl="~/teacher-details.aspx">Manage Teacher </asp:HyperLink>
                </div>
            </div>
          </li>
          <li>
            <div class="dropdown">
                <span>Issue Book</span>
                <div class="dropdown-content">
                    <asp:HyperLink ID="HyperLink6" runat="server" Text="Hyperlink" NavigateUrl="~/teacher-issue-book.aspx">Teacher</asp:HyperLink>
                </div>
            </div>
          </li>
          <li><asp:HyperLink ID="HyperLink7" runat="server" Text="Hyperlink" NavigateUrl="~/return-book.aspx">Return Book</asp:HyperLink></li>
          <li>
              <div class="dropdown">
                <span>Book Tools</span>
                <div class="dropdown-content">
                    <asp:HyperLink ID="HyperLink8" runat="server" Text="Hyperlink" NavigateUrl="~/issued-book-details.aspx">Issued Book </asp:HyperLink>
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
              <asp:HyperLink ID="HyperLink18" runat="server" Text="Hyperlink" NavigateUrl="Switch_user.aspx">Switch User</asp:HyperLink>
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
        <asp:Panel ID="Panel1" runat="server">
            <h2 class="head">📚 Student Issue </h2>
             <div class="container">
                 <div class="content1">
                    <asp:Label ID="Label1" runat="server" Text="Issue No" CssClass="mylable"></asp:Label>
                    <asp:TextBox ID="TextBox1" runat="server" CssClass="myTextbox" TextMode="Number" placeholder="Issue No"></asp:TextBox>
                    <asp:Label ID="Label2" runat="server" Text="Acc No" CssClass="mylable" ></asp:Label>
                    <asp:TextBox ID="TextBox2" runat="server" CssClass="myTextbox" placeholder="For eg: VLB-BK-0001" AutoPostBack="true"
    OnTextChanged="TextBox2_TextChanged"></asp:TextBox>
                    <asp:Label ID="Label3" runat="server" Text=" Author" CssClass="mylable"></asp:Label>
                    <asp:TextBox ID="TextBox3" runat="server" CssClass="myTextbox" ReadOnly="true" placeholder="Author"></asp:TextBox>
                    <asp:Label ID="Label4" runat="server" Text=" Title" CssClass="mylable"></asp:Label>
                    <asp:TextBox ID="TextBox4" runat="server" CssClass="myTextbox" ReadOnly="true" placeholder="Title"></asp:TextBox>
                    <asp:Label ID="Label5" runat="server" Text=" Publishe" CssClass="mylable"></asp:Label>
                    <asp:TextBox ID="TextBox5" runat="server" CssClass="myTextbox" ReadOnly="true" placeholder="Publisher"></asp:TextBox>
                    <asp:Label ID="Label6" runat="server" Text=" Subject" CssClass="mylable"></asp:Label>
                    <asp:TextBox ID="TextBox6" runat="server" CssClass="myTextbox" ReadOnly="true" placeholder="Subject"></asp:TextBox>
                    <asp:Label ID="Label7" runat="server" Text=" Price" CssClass="mylable"></asp:Label>
                    <asp:TextBox ID="TextBox7" runat="server" CssClass="myTextbox"  ReadOnly="true" placeholder="Price"></asp:TextBox>
                    <asp:Label ID="Label8" runat="server" Text="For Days" CssClass="mylable"></asp:Label>
                    <asp:TextBox ID="TextBox8" runat="server" CssClass="myTextbox" placeholder="For Days"></asp:TextBox>
                </div>
                <div class="content2">
                    <asp:Label ID="Label9" runat="server" Text="Issue Date" CssClass="mylable"></asp:Label>
                    <asp:TextBox ID="TextBox9" runat="server" CssClass="myTextbox" TextMode="Date"></asp:TextBox>
                    <asp:Label ID="Label10" runat="server" Text="Clg Roll No" CssClass="mylable" ></asp:Label>
                    <asp:TextBox ID="TextBox10" runat="server" CssClass="myTextbox" placeholder=" For eg: VLB25-001" AutoPostBack="true"
    OnTextChanged="TextBox10_TextChanged"></asp:TextBox>
                    <asp:Label ID="Label11" runat="server" Text="Branch/Class" CssClass="mylable"></asp:Label>
                    <asp:TextBox ID="TextBox11" ReadOnly="true" runat="server" CssClass="myTextbox" placeholder="Branch Or Class"></asp:TextBox>
                    <asp:Label ID="Label12" runat="server" Text="UNI Reg No" CssClass="mylable"></asp:Label>
                    <asp:TextBox ID="TextBox12" ReadOnly="true" runat="server" CssClass="myTextbox" placeholder="University Registration No"></asp:TextBox>
                    <asp:Label ID="Label13" runat="server" Text="Student Name" CssClass="mylable"></asp:Label>
                    <asp:TextBox ID="TextBox13" ReadOnly="true" runat="server" CssClass="myTextbox" placeholder="Student Name"></asp:TextBox>
                    <asp:Label ID="Label14" runat="server" Text="Sem/Year" CssClass="mylable"></asp:Label>
                    <asp:TextBox ID="TextBox14" ReadOnly="true" runat="server" CssClass="myTextbox" placeholder="Sem Or Year"></asp:TextBox>
                    <asp:Label ID="Label15" runat="server" Text="Email ID" CssClass="mylable"></asp:Label>
                    <asp:TextBox ID="TextBox15" ReadOnly="true" runat="server" CssClass="myTextbox" TextMode="Email" placeholder="Email"></asp:TextBox>
                    <asp:Label ID="Label16" runat="server" Text=" Contact No" CssClass="mylable"></asp:Label>
                    <asp:TextBox ID="TextBox16" ReadOnly="true" runat="server" CssClass="myTextbox" TextMode="Phone" MaxLength="11" placeholder="Contact No"></asp:TextBox>
                </div> 
                 <div class="button">
                        <asp:Button ID="ButtonReset" runat="server" Text="🔄 RESET" CssClass="but" OnClick="Button1_Click" CausesValidation="false" />
                        <asp:Button ID="ButtonSave" runat="server" Text="📤 ISSUE" CssClass="but"  OnClick="Button2_Click" />
                        <asp:Button ID="ButtonClose" runat="server" Text="❌ CLOSE" CssClass="but" OnClick="Button3_Click" />
                    </div> 
             </div>
        </asp:Panel>
    </form>
                      <footer class="footer">
    © 2026 Vidhyant Library | Library Management System| All Rights Reserved.
</footer>
    <script src="JavaScript.js"></script>
    <script src="JavaScript3.js"></script>
</body>
</html>
