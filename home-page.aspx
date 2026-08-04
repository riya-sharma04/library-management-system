<%@ Page Language="C#" AutoEventWireup="true" CodeFile="home-page.aspx.cs" Inherits="loginpage2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Vidhyant Library | Home</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    
    <link href="StyleSheet-firstpage.css" rel="stylesheet" type="text/css"/>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.7.2/css/all.min.css"/>
    <style>
                .hero .text{
    position: relative;
    color: white;
    text-shadow: 2px 2px 5px #000000;
  }
.hero-button{

    margin-top:70px;

    text-align:center;
}

.get-started-btn{

    background:#0078D4;

    color:#fff;

    border:none;

    padding:14px 38px;

    font-size:20px;

    font-weight:600;

    border-radius:40px;

    cursor:pointer;

    transition:.3s;

    box-shadow:0 8px 20px rgba(0,120,212,.35);
}

.get-started-btn:hover{

    background:#005fa3;

    transform:translateY(-4px);

    box-shadow:0 10px 20px rgba(0,120,212,.45);
}
    </style>
</head>
    
<body>
    <form id="form1" runat="server">
    <header>
      <span id="logo">
          <asp:Image ID="Image1" runat="server" ImageUrl="~/images/logo.png" /></span>
      <span class="heading" style="font-size: xx-large; font-weight: 700; position: absolute; top: 10px; left: 85px">VIDHYANT LIBRARY</span>
        <nav>
        <ul style="margin-top:5px;">
          <li><asp:HyperLink ID="HyperLink18" runat="server" Text="Hyperlink" NavigateUrl="~/login-page.aspx">Login</asp:HyperLink>             
          </li>
        </ul>
      </nav>
    </header>
    <section class="hero">
  <div class="container">
    <h2 class="text" style="font-size: 300%; font-weight: 700; top: -2px; left: 0px; height: 56px;">Smart Library Management System</h2>
    <p class="text" style="font-weight: 500; font-size: 100%; top: 0px; left: 299px; width: 590px;">
      Manage Books, Members, Issue & Return, Search Records and Library Inventory with Ease.
    </p>
    
      <div class="hero-button">

    <asp:Button ID="btnGetStarted"
        runat="server"
        Text="GET STARTED"
        CssClass="get-started-btn"
        OnClick="btnGetStarted_Click" />

</div>

      
  </div>        
      
</section> 
        <!-- LIBRARY STATISTICS            -->
<!-- ============================= -->

<section class="statistics">

    <div class="stats-container">


        <div class="stats-boxes">

            <div class="stat-card">

                <i class="fa-solid fa-book"></i>

                <asp:Label ID="lblBooks"
                    runat="server"
                    CssClass="stat-number"></asp:Label>

                <span class="stat-title">Books</span>

            </div>

            <div class="stat-card">

                <i class="fa-solid fa-user-graduate"></i>

                <asp:Label ID="lblStudents"
                    runat="server"
                    CssClass="stat-number"></asp:Label>

                <span class="stat-title">Students</span>

            </div>

            <div class="stat-card">

                <i class="fa-solid fa-chalkboard-user"></i>

                <asp:Label ID="lblTeachers"
                    runat="server"
                    CssClass="stat-number"></asp:Label>

                <span class="stat-title">Teachers</span>

            </div>

            <div class="stat-card">

                <i class="fa-solid fa-book-open-reader"></i>

                <asp:Label ID="lblIssuedBooks"
                    runat="server"
                    CssClass="stat-number"></asp:Label>

                <span class="stat-title">Issued Books</span>

            </div>

        </div>

    </div>

</section>
    <!-- Why Use This System -->

    <section class="features">

        <div class="feature-container">

            <h2>Why Use This System?</h2>

            <div class="feature-boxes">

                <div class="feature-card">
                    <h3><i class="fa-solid fa-book"></i> Book Management</h3>
                    <p>
                        Maintain and organize all library books efficiently.
                    </p>
                </div>

                <div class="feature-card">
                    <h3><i class="fa-solid fa-user-graduate"></i> Student & Teacher Records</h3>
                    <p>
                        Store and manage student and teacher information.
                    </p>
                </div>

                <div class="feature-card">
                    <h3><i class="fa-solid fa-right-left"></i> Issue & Return</h3>
                    <p>
                        Record every issued and returned book accurately.
                    </p>
                </div>

                <div class="feature-card">
                    <h3><i class="fa-solid fa-chart-column"></i> Reports</h3>
                    <p>
                        Generate reports for books, students and transactions.
                    </p>
                </div>

            </div>

        </div>

    </section> 

    <!-- Footer -->

    <footer class="footer">

        <h3>VIDHYANT LIBRARY</h3>

        <p>Library Management System</p>

        <p>Built with ASP.NET Web Forms | C# | MySQL</p>

        <p>Version 1.0</p>

        <p>&copy; 2026 Vidhyant Library. All Rights Reserved.</p>

    </footer>
    </form>
</body>
</html>
