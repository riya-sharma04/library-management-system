<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Switch_user.aspx.cs" Inherits="Switch_user" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.2/css/all.min.css" />
<link href="StyleSheet3.css" rel="stylesheet" type="text/css" />
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
        <li><asp:HyperLink ID="HyperLink1" runat="server" Text="Hyperlink" NavigateUrl="~/home-page.aspx" CssClass="active">⌂ Home</asp:HyperLink></li>
        
      </ul>
    </nav>
  </header>
       
        <asp:Panel ID="Panel1" runat="server">
            <h2 class="head">🧑‍💻 SWITCH USER</h2>
             <div class="container">
                    <div class="content">
                    <asp:Label ID="Label1" runat="server" Text="👤 User Name" CssClass="mylable"></asp:Label>
                    <asp:TextBox ID="TextBox1" runat="server" CssClass="myTextbox" placeholder="Enter Username" onkeydown="if(event.key === 'Enter'){ event.preventDefault(); document.getElementById('TextBox2').focus(); return false; }"> </asp:TextBox>

                    <asp:Label ID="Label2" runat="server" Text="🔑 Password" CssClass="mylable"></asp:Label>
                    <div class="password-container">

    <asp:TextBox
        ID="TextBox2"
        runat="server"
        CssClass="myTextbox"
        ClientIDMode="Static"
        TextMode="Password"
        placeholder="Enter Password">
    </asp:TextBox>

    <span class="toggle-password" onclick="togglePassword('TextBox2','eyeIcon2')">
        <i id="eyeIcon" class="fa-solid fa-eye"></i>
    </span>
</div>    
                 </div>   
             </div>
            <div class="button">
    <asp:Button ID="ButtonReset" runat="server" Text="🔄 RESET"  CssClass="but" OnClick="Button1_Click" />
    <asp:Button ID="ButtonLogin" runat="server" Text="🔓 SWITCH "  CssClass="but" OnClick="Button2_Click" />
    <asp:Button ID="ButtonClose" runat="server" Text="❌ CLOSE" CssClass="but"  OnClick="Button3_Click1" />
</div>
        </asp:Panel>
    </form>
                  <footer class="footer">
    © 2026 Vidhyant Library | Library Management System| All Rights Reserved.
</footer>
    <script src="JavaScript2.js"></script>
</body>
</html>
