<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index" %>
<%@ Register Src="~/MobileNavigation.ascx"
    TagPrefix="uc1"
    TagName="MobileNavigation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="StyleSheet-firstpage.css" rel="stylesheet" type="text/css"/>
    <style>
        body {
    overflow: hidden;
}
        #background-image {
    position: fixed;
    width: 100%;
    height: 100%;
    background: url('images/library management.png') no-repeat center center;
    background-size: cover;
    opacity: 0.8; /* Adjust transparency */
    z-index: -1;
}
        .welcome-label {
            
            font-size: 15px;
            color: #fff;
            display:inline;
            float: none;
            vertical-align:middle;
            margin:0;
            
            text-shadow: 0 2px 8px rgba(0,0,0,.65);
           
        }
        .welcome-label2
        {
            font-size: 15px;
            display:inline;
            vertical-align:middle;
            color: #FFF;
            float: none;
            text-shadow:0 2px 8px rgba(0,0,0,.65) ;
        }
        .welcome-label1{

            color:#FFC83D;
             opacity:.9;
            font-weight:bold;
            font-size:17px;
            position:relative;
            top:3px;
}
        .welcome-card {
    position: absolute;
    top: 80px;
    left: 15px;
    width: 250px;
    max-width: calc(100% - 30px);
    background: rgba(8,35,70,.45);
    backdrop-filter: blur(12px);
    border-radius: 8px;
    padding: 15px 18px;
    border: 1px solid rgba(255,255,255,.15);
    box-shadow: 0 8px 20px rgba(0,0,0,.30);
    z-index: 5;
}
        header {
    position: relative;
}

.no-arrow {
    position: absolute;
    right: 15px;
    top: 50%;
    transform: translateY(-50%);
}

.menu-icon {
    cursor: pointer;
    font-size: 22px;
    color: #fff;
    display: block;
}
/*        .welcome-card{

    position:absolute;

    top:80px;

    left:15px;

    width:250px;

    background:rgba(8,35,70,.45);

    backdrop-filter:blur(12px);

    border-radius:8px;

    padding:15px 18px;

    border:1px solid rgba(255,255,255,.15);

    box-shadow:0 8px 20px rgba(0,0,0,.30);

    z-index:5;

}*/
       
        .hero .text{
    position: relative;
    color: #FFFFFF;
    text-shadow: 0 4px 15px rgba(0,0,0,.55);
  }
.footer {
    position: fixed;
    bottom: 0;
    left: 0;
    width: 100%;
    height: 35px;
    line-height: 35px;
    padding: 0;
    margin: 0;
    background: #0078D4;
    color: #fff;
    font-size: 14px;
    text-align: center;
    overflow: hidden;
}

    </style>
</head>
    
<body>
    <form id="form1" runat="server">
        <uc1:MobileNavigation
    ID="MobileNavigation1"
    runat="server" />

    <header>
      <span id="logo">
          <asp:Image ID="Image1" runat="server" ImageUrl="~/images/logo.png" /></span>
          <span class="heading">VIDHYANT LIBRARY</span>
        <nav>
        <ul>
          <li>
            <div class="dropdown">
                <span>Details</span>
                <div class="dropdown-content">
                    <asp:HyperLink ID="HyperLink2" runat="server" Text="Hyperlink" NavigateUrl="~/book-details.aspx">Manage Book </asp:HyperLink>
                    <asp:HyperLink ID="HyperLink3" runat="server" Text="Hyperlink" NavigateUrl="~/student-details.aspx">Manage Student </asp:HyperLink>
                    <asp:HyperLink ID="HyperLink4" runat="server" Text="Hyperlink" NavigateUrl="~/teacher-details.aspx">Manage Teacher </asp:HyperLink>
                </div>
            </div>
          </li>
          <li>
        <div class="dropdown">
            <span> Issue Book</span>
            <div class="dropdown-content">
                <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/student-issue-book.aspx"> Student </asp:HyperLink>
                <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="~/teacher-issue-book.aspx"> Teacher</asp:HyperLink>
            </div>
        </div>
      </li>
          <li><asp:HyperLink ID="HyperLink7" runat="server" Text="Hyperlink" NavigateUrl="~/return-book.aspx">Return Book</asp:HyperLink></li>
          <li>
              <div class="dropdown">
                <span>Book Tools</span>
                <div class="dropdown-content">
                    <asp:HyperLink ID="HyperLink8" runat="server" Text="Hyperlink" NavigateUrl="~/issued-book-details.aspx">Issued Book Records</asp:HyperLink>
                    <asp:HyperLink ID="HyperLink9" runat="server" Text="Hyperlink" NavigateUrl="~/search-books.aspx">Book Records</asp:HyperLink>
                    <asp:HyperLink ID="HyperLink10" runat="server" Text="Hyperlink" NavigateUrl="~/search-students.aspx"> Student Records</asp:HyperLink>
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

    <asp:Label ID="lblUserName"
        runat="server"
        CssClass="welcome-label">
    </asp:Label>
    <asp:Label ID="lblUserName1"
        runat="server"
        CssClass="welcome-label1">
    </asp:Label>
        <br/> 
    <asp:Label ID="lblUserName2"
    runat="server"
    CssClass="welcome-label2">
    </asp:Label>

</div>
      
    <section class="hero">
  <div class="container">
    <h2 class="text" style="font-size: 300%; font-weight: 700; top: -2px; left: 0px; height: 56px;">Smart Library Management System</h2>
    <p class="text" style="font-weight: 500; font-size: 100%; top: 0px; left: 299px; width: 590px;">
     Manage Books, Members, Issue & Return, Search Records and Library Inventory with Ease.
    </p>

    <div class="search-container">
 <asp:TextBox
ID="searchInput"
runat="server"
CssClass="search-box"
AutoPostBack="true"
OnTextChanged="searchInput_TextChanged"
placeholder="Search books...">
</asp:TextBox>

<asp:LinkButton
ID="searchBtn"
runat="server"
CssClass="search-icon"
OnClick="searchBtn_Click">

🔍

</asp:LinkButton>
    </div>
  </div>        
</section> 
        
    </form>
              <footer class="footer">
    © 2026 Vidhyant Library | Library Management System| All Rights Reserved.
</footer>
    <script src="JavaScript3.js"></script>
    <script>
    document.addEventListener("DOMContentLoaded", function () {

        const dropdowns = document.querySelectorAll(".dropdown");

        dropdowns.forEach(function (dropdown) {

            const trigger = dropdown.querySelector("span");

            if (!trigger) return;

            trigger.addEventListener("click", function (e) {

                e.preventDefault();
                e.stopPropagation();

                // Close all other dropdowns
                dropdowns.forEach(function (otherDropdown) {
                    if (otherDropdown !== dropdown) {
                        otherDropdown.classList.remove("open");
                    }
                });

                // Toggle current dropdown
                dropdown.classList.toggle("open");
            });
        });


        // Close dropdown when clicking outside
        document.addEventListener("click", function (e) {

            if (!e.target.closest(".dropdown")) {

                dropdowns.forEach(function (dropdown) {
                    dropdown.classList.remove("open");
                });

            }

        });

    });
    </script>
</body>
</html>
