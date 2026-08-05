<%@ Page Language="C#" AutoEventWireup="true" CodeFile="search-books.aspx.cs" Inherits="search_books" %>
<%@ Register Src="~/MobileNavigation.ascx"
    TagPrefix="uc1"
    TagName="MobileNavigation" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="StyleSheet1.css" rel="stylesheet" type="text/css" />
    <style>
       

    </style>
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
                    <asp:HyperLink ID="HyperLink3" runat="server" Text="Hyperlink" NavigateUrl="~/student-details.aspx">Manage Student</asp:HyperLink>
                    <asp:HyperLink ID="HyperLink4" runat="server" Text="Hyperlink" NavigateUrl="~/teacher-details.aspx">Manage Teacher </asp:HyperLink>
                </div>
            </div>
          </li>
          <li>
            <div class="dropdown">
                <span>Issue Book</span>
                <div class="dropdown-content">
                    <asp:HyperLink ID="HyperLink5" runat="server" Text="Hyperlink" NavigateUrl="~/student-issue-book.aspx">Student</asp:HyperLink>
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
        <asp:Panel ID="Panel1" runat="server">
            <h2 class="head">📚BOOKS RECORDS </h2>
             <div class="container">
                 <asp:Label ID="Label6" runat="server" Text="SEARCH" CssClass="label" Width="100"></asp:Label>
                     <div class="search-wrapper">

        <asp:TextBox ID="TextBox1"
            runat="server"
            Textmode="Search"
            CssClass="search-box"
            Placeholder="Search by Acc No, Author, Title, Publisher........."
            AutoPostBack="True"
            OnTextChanged="TextBox1_TextChanged1">
        </asp:TextBox>

        <asp:LinkButton ID="btnSearch"
            runat="server"
            CssClass="search-icon"
            OnClick="btnSearch_Click">
            🔍
        </asp:LinkButton>

    </div>

&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:Label ID="Label5" runat="server" Text="Subject" CssClass="label" Width="60px"></asp:Label>
                 &nbsp<asp:DropDownList ID="DropDownList5" runat="server" CssClass="DD" AutoPostBack="True" OnSelectedIndexChanged="DropDownList5_SelectedIndexChanged"></asp:DropDownList>
                 
                 <div class="grid-container">
                                      <asp:GridView ID="GridView1"
    runat="server"
    AutoGenerateColumns="False"
    DataKeyNames="acc_no"
    OnRowCommand="GridView1_RowCommand"
    OnRowDataBound="GridView1_RowDataBound">

    <Columns>

        <asp:BoundField DataField="acc_no" HeaderText="Acc No" />
<asp:BoundField DataField="call_no" HeaderText="Call No" />
<asp:BoundField DataField="title" HeaderText="Book Title" />
<asp:BoundField DataField="author" HeaderText="Author" />
<asp:BoundField DataField="publisher" HeaderText="Publisher" />
<asp:BoundField DataField="edition" HeaderText="Edition" />
<asp:BoundField DataField="year" HeaderText="Year" />
<asp:BoundField DataField="subject" HeaderText="Subject" />
<asp:BoundField DataField="location_rack" HeaderText="Rack" />
<asp:BoundField DataField="no_of_copy" HeaderText="Copies" />
<asp:BoundField DataField="price" HeaderText="Price (₹)" />
<asp:BoundField DataField="remarks" HeaderText="Remarks" />
        <asp:TemplateField HeaderText="Delete">
            <ItemTemplate>

                <asp:LinkButton
                    ID="btnDelete"
                    runat="server"
                    Text="🗑"
                    CssClass="deleteIcon"
                    ToolTip="Delete Book"
                    CommandName="DeleteBook"
                    CommandArgument='<%# Eval("acc_no") %>'>
                </asp:LinkButton>

            </ItemTemplate>
        </asp:TemplateField>

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
