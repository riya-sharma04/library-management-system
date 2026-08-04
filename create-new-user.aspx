<%@ Page Language="C#" AutoEventWireup="true" CodeFile="create-new-user.aspx.cs" Inherits="create_new_user" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="https://code.jquery.com/jquery-latest.min.js"></script>

    <title>create new users</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.2/css/all.min.css" />
    <link href="StyleSheet3.css" rel="stylesheet" type="text/css"/>
   <style>
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="background-image"></div>
  <header>
    <span id="logo">
          <asp:Image ID="Image1" runat="server" ImageUrl="~/images/logo.png" /></span>
          <span class="heading">VIDHYANT LIBRARY</span>
    <nav>
      <ul>
         <li><asp:HyperLink ID="HyperLink1" runat="server" Text="Hyperlink" NavigateUrl="~/index.aspx" CssClass="active">⌂ Home</asp:HyperLink></li>
         <li>
       <div class="dropdown no-arrow">
           <span class="menu-icon"> ☰ </span>
           <div class="dropdown-content">
               <asp:HyperLink ID="HyperLink18" runat="server" Text="Hyperlink" NavigateUrl="~/Switch_user.aspx">Switch User</asp:HyperLink>
               <asp:LinkButton ID="btnLogout" runat="server" CssClass="logout-link" OnClientClick="return confirmLogout();" OnClick="btnLogout_Click">Logout</asp:LinkButton>
                   

           </div>
       </div>
 </li>
      </ul>
    </nav>
  </header>
        <asp:Panel ID="Panel1" runat="server">
            <h2 class="head">📝 SIGN-UP PAGE</h2>
             <div class="container">
                 <div class="content">
                    <asp:Label ID="Label1" runat="server" Text="👤User Name" CssClass="mylable" ToolTip="Must be Identical to Password"></asp:Label>
                    <asp:TextBox ID="TextBox1" runat="server" CssClass="myTextbox" placeholder="Enter Username"></asp:TextBox>
                    <asp:Label ID="Label2" runat="server" Text="🔑Password" CssClass="mylable"></asp:Label>
                     <div class="password-container">

    <asp:TextBox
        ID="TextBox2"
        runat="server"
        CssClass="myTextbox"
        TextMode="Password"
        placeholder="Enter Password">
    </asp:TextBox>

    <span class="toggle-password"
          onclick="togglePassword('<%= TextBox2.ClientID %>','eye1')">
        <i id="eye1" class="fa-solid fa-eye"></i>
    </span>

</div>
                    <asp:Label ID="Label3" runat="server" Text="🔒Confirm Password" CssClass="mylable" ToolTip="Must be identical to above Password"></asp:Label>
                   <div class="password-container">

    <asp:TextBox
        ID="TextBox3"
        runat="server"
        CssClass="myTextbox"
        TextMode="Password"
        placeholder="Enter Confirm Password">
    </asp:TextBox>

    <span class="toggle-password"
          onclick="togglePassword('<%= TextBox3.ClientID %>','eye2')">
        <i id="eye2" class="fa-solid fa-eye"></i>
    </span>

</div>
                 </div> 
             </div>
            <div class="button">
     <asp:Button ID="ButtonReset" runat="server" Text="🔄 RESET"  CssClass="but" OnClick="Button1_Click" />
    <asp:Button ID="ButtonSignup" runat="server" Text="📝 SIGN UP"  CssClass="but" OnClick="Button2_Click" />
    <asp:Button ID="ButtonClose" runat="server" Text="❌ CLOSE"  CssClass="but" OnClick="Button3_Click" />
</div>
        </asp:Panel>
    </form>
              <footer class="footer">
    © 2026 Vidhyant Library | Library Management System| All Rights Reserved.
</footer>
    <script src="JavaScript2.js"></script>
    <script src="JavaScript3.js"></script>
</body>
</html>
