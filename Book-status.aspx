<%@ Page Language="C#" AutoEventWireup="true" CodeFile="book-status.aspx.cs" Inherits="book_status" %>
<%@ Register Src="~/MobileNavigation.ascx"
    TagPrefix="uc1"
    TagName="MobileNavigation" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>Book Status | Vidhyant Library</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <link href="StyleSheet1.css" rel="stylesheet" type="text/css" />

    <style>

    /* ================================
       BOOK STATUS SEARCH
    ================================= */

    .status-search-wrapper {
        display: inline-flex;
        align-items: center;
        position: relative;
        margin-left: 20px;
    }

    /* TextBox + Search Icon wrapper */
    .status-search-input {
        position: relative;
        display: inline-block;
    }

    .status-search-box {
        width: 380px;
        height: 38px;

        /* Right side icon ke liye extra space */
        padding: 0 45px 0 15px;

        border: 1px solid #D0D7DE;
        border-radius: 6px;
        outline: none;
        font-size: 14px;

        box-sizing: border-box;
    }

    .status-search-box:focus {
        border-color: #0078D4;
    }


    /* ================================
       SEARCH ICON INSIDE TEXTBOX
    ================================= */

    .status-search-icon {
        position: absolute;
        right: 8px;
        top: 50%;
        transform: translateY(-50%);

        width: 30px;
        height: 30px;

        display: flex;
        align-items: center;
        justify-content: center;

        color: #0078D4 !important;
        background: transparent;
        border: none;

        font-size: 17px;
        text-decoration: none;
        cursor: pointer;

        z-index: 5;
    }

    .status-search-icon:hover {
        color: #005a9e !important;
    }


    /* ================================
       STATUS DROPDOWN
    ================================= */

    .status-dropdown {
        width: 160px;
        height: 38px;
        margin-left: 15px;
        padding: 5px 10px;
        border: 1px solid #D0D7DE;
        border-radius: 6px;
        font-size: 14px;
        outline: none;
    }

    .status-dropdown:focus {
        border-color: #0078D4;
    }


    /* ================================
       SUMMARY CARDS
    ================================= */

    .status-summary {
        display: flex;
        gap: 12px;
        margin: 15px 0;
        flex-wrap: wrap;
    }

    .status-card {
        min-width: 135px;
        padding: 10px 15px;
        background: white;
        border: 1px solid #D0D7DE;
        border-radius: 8px;
        box-shadow: 0 2px 6px rgba(0,0,0,0.08);
    }

    .status-card-title {
        display: block;
        font-size: 12px;
        color: #555;
        margin-bottom: 3px;
    }

    .status-card-value {
        display: block;
        font-size: 21px;
        font-weight: bold;
        color: #0078D4;
    }


    /* ================================
       STATUS BADGES
    ================================= */

    .status-available {
        color: green !important;
        font-weight: bold;
    }

    .status-issued {
        color: red !important;
        font-weight: bold;
    }

    .status-never {
        color: #777 !important;
        font-weight: bold;
    }


    /* ================================
       GRID
    ================================= */

    .grid-container {
        height: 430px;
        overflow-y: auto;
        overflow-x: auto;
        background: white;
        margin-top: 15px;
    }

    #GridView1 {
        width: max-content;
        min-width: 100%;
        border-collapse: collapse;
        background: white;
        border: 1px solid #D0D7DE;
        font-size: 14px;
    }

    #GridView1 th {
        background-color: #0078D4;
        color: white;
        padding: 10px;
        text-align: center;
        white-space: nowrap;
        position: sticky;
        top: 0;
        z-index: 2;
    }

    #GridView1 td {
        padding: 9px 10px;
        border: 1px solid #D0D7DE;
        white-space: nowrap;
    }

    #GridView1 tr:hover td {
        background-color: #f4f8fb;
    }


    /* ================================
       DELETE ICON
    ================================= */

    .deleteIcon {
        color: red !important;
        font-size: 17px;
        text-decoration: none;
        cursor: pointer;
    }

    .deleteIcon:hover {
        color: darkred !important;
    }
    /* =========================================================
   MOBILE RESPONSIVE
   StyleSheet1.css
   Desktop Design Remains Unchanged
========================================================= */

@media screen and (max-width: 600px) {

    /* =====================================================
       GENERAL
    ===================================================== */

    html,
    body {
        width: 100%;
        max-width: 100%;
        overflow-x: hidden;
    }


    /* =====================================================
       HEADER
    ===================================================== */

    header {
        width: 100%;
        height: auto;
        min-height: 30px;
        padding: 10px !important;
        box-sizing: border-box;
        display: flex;
        flex-wrap: wrap;
        align-items: center;
        position: relative;
        /* Dropdown ko cut hone se rokega */
        overflow: visible;
        z-index: 1000;
    }


    /* =====================================================
       LOGO
    ===================================================== */

    #Image1 {
        width: auto;
        height: 40px;
        max-width: 42px;
        position: relative;
        left: auto;
        top: auto;
        flex-shrink: 0;
    }


    /* =====================================================
       LIBRARY HEADING
    ===================================================== */

    .heading {
        position: relative;
        left: auto;
        top: auto;
        margin-left: 7px;
        font-size: 18px;
        line-height: 1.2;
        /* Text ko cut hone se bachayega */
        white-space: nowrap;
        flex: 1;
        min-width: 0;
        overflow: visible;
        z-index: 1001;
    }


    /* =====================================================
       NAVIGATION
    ===================================================== */

    nav {
        width: 100%;
        max-width: 100%;
        position: relative;
        z-index: 2000;
        overflow: visible;
    }


        nav ul {
            width: 100%;
            max-width: 100%;
            display: flex;
            flex-wrap: wrap;
            justify-content: center;
            align-items: center;
            gap: 6px;
            margin: 8px 0 0;
            padding: 0;
            box-sizing: border-box;
            position: relative;
            z-index: 2000;
            overflow: visible;
        }


            nav ul li {
                position: relative;
                max-width: 100%;
                font-size: 12px;
                flex-shrink: 0;
            }


        /* Menu links */
        nav a,
        .dropdown > span {
            white-space: nowrap;
        }

    /* =====================================================
   MOBILE DROPDOWNS
===================================================== */

    .dropdown {
        position: relative;
        display: inline-block;
        z-index: 3000;
    }


    /* Dropdown box */
    .dropdown-content {
        position: absolute;
        top: 100%;
        min-width: 150px;
        max-width: calc(100vw - 20px);
        box-sizing: border-box;
        background-color: white;
        z-index: 99999;
        overflow: visible;
    }


        /* Dropdown links */
        .dropdown-content a,
        .logout-link {
            display: block;
            width: 100%;
            box-sizing: border-box;
            padding: 10px 12px;
            font-size: 12px;
            white-space: normal;
            word-break: normal;
        }


    /* =====================================================
   LEFT SIDE DROPDOWN
===================================================== */

    nav ul li:first-child .dropdown-content {
        left: 0;
        right: auto;
    }


    /* =====================================================
   RIGHT SIDE DROPDOWN
===================================================== */

    nav ul li:last-child .dropdown-content {
        left: auto;
        right: 0;
    }


    /* =====================================================
       MAIN CONTAINER
    ===================================================== */

    .container {
        width: 100%;
        max-width: 100%;
        margin-top: 10px;
        padding: 10px;
        box-sizing: border-box;
    }


    /* =====================================================
       PANEL
    ===================================================== */

    #Panel1 {
        width: calc(100% - 20px);
        min-width: 0;
        max-width: 100%;
        height: auto;
        min-height: 0;
        margin: 25px auto 55px auto;
        box-sizing: border-box;
        overflow: hidden;
    }


    /* =====================================================
       GRID CONTAINER
    ===================================================== */

    .grid-container {
        width: 100%;
        height: auto;
        max-height: 400px;
        overflow-x: auto;
        overflow-y: auto;
        box-sizing: border-box;
        margin-top: 15px;
    }


    /* =====================================================
       GRIDVIEW
    ===================================================== */

    #GridView1 {
        width: max-content;
        min-width: 700px;
        border-collapse: collapse;
        font-size: 13px;
    }


        #GridView1 th {
            padding: 9px 10px;
            font-size: 13px;
            white-space: nowrap;
        }


        #GridView1 td {
            padding: 9px 10px;
            font-size: 13px;
            white-space: normal;
            word-break: break-word;
        }


    /* =====================================================
       DROPDOWNS / SELECT
    ===================================================== */

    .DD {
        width: 100%;
        max-width: 180px;
        box-sizing: border-box;
    }


    /* =====================================================
       LABEL
    ===================================================== */

    .label {
        max-width: 100%;
        padding: 6px 10px;
        box-sizing: border-box;
        white-space: normal;
        word-break: break-word;
    }


    /* =====================================================
   MOBILE WELCOME CARD
===================================================== */

    .welcome-card {
        position: relative !important;
        top: auto !important;
        left: auto !important;
        right: auto !important;
        width: 220px !important;
        max-width: calc(100% - 40px);
        margin: 10px auto 12px auto !important;
        padding: 8px 10px !important;
        box-sizing: border-box;
        z-index: 10 !important;
    }


    /* =====================================================
       WELCOME LABELS
    ===================================================== */

    .welcome-label,
    .welcome-label1,
    .welcome-label2 {
        max-width: 100%;
        box-sizing: border-box;
        word-break: break-word;
    }


    /* =====================================================
       SEARCH
    ===================================================== */

    .search-wrapper {
        width: 100%;
        max-width: 380px;
        box-sizing: border-box;
    }


    .search-box {
        width: 100%;
        max-width: 100%;
        box-sizing: border-box;
        padding: 8px 45px 8px 10px;
    }


    .search-icon {
        right: 12px;
        left: auto;
        transform: translateY(-50%);
    }


    /* =====================================================
       DELETE ICON
    ===================================================== */

    .deleteIcon {
        font-size: 21px;
        white-space: nowrap;
    }


    /* =====================================================
       FOOTER
    ===================================================== */

    .footer {
        width: 100%;
        height: auto;
        min-height: 35px;
        line-height: 1.4;
        padding: 7px 8px;
        font-size: 11px;
        box-sizing: border-box;
        white-space: normal;
        overflow: hidden;
    }
}
/* =========================================================
   BOOK STATUS - MOBILE SEARCH/FILTER FIX
   Desktop completely unchanged
========================================================= */

@media screen and (max-width: 600px) {

    /* SEARCH + STATUS ko mobile par vertical rakho */
    .status-search-wrapper {
        width: 100% !important;
        max-width: 100% !important;
        margin-left: 0 !important;
        display: flex !important;
        flex-direction: column !important;
        align-items: stretch !important;
        gap: 10px !important;
        box-sizing: border-box !important;
    }

    /* Search textbox wrapper */
    .status-search-input {
        width: 100% !important;
        max-width: 100% !important;
        display: block !important;
        box-sizing: border-box !important;
    }

    /* Search textbox */
    .status-search-box {
        width: 100% !important;
        max-width: 100% !important;
        height: 40px !important;
        box-sizing: border-box !important;
        font-size: 13px !important;
        padding: 0 42px 0 12px !important;
    }

    /* Search icon */
    .status-search-icon {
        right: 7px !important;
        width: 30px !important;
        height: 30px !important;
        font-size: 16px !important;
    }

    /* STATUS label */
    .status-search-wrapper > .label {
        margin-left: 0 !important;
        width: auto !important;
        max-width: 100% !important;
        padding: 0 !important;
        box-sizing: border-box !important;
    }

    /* Status dropdown */
    .status-dropdown {
        width: 100% !important;
        max-width: 100% !important;
        height: 40px !important;
        margin-left: 0 !important;
        box-sizing: border-box !important;
        font-size: 13px !important;
    }

}

</style>

</head>


<body>

<form id="form1" runat="server">
    <uc1:MobileNavigation
    ID="MobileNavigation1"
    runat="server" />

    <div id="background-image"></div>


    <!-- =========================================
         HEADER
    ========================================== -->

    <header>

        <span id="logo">
            <asp:Image
                ID="Image1"
                runat="server"
                ImageUrl="~/images/logo.png" />
        </span>

        <span class="heading">
            VIDHYANT LIBRARY
        </span>


        <nav>

            <ul>


                <li>
                    <asp:HyperLink
                        ID="HyperLink1"
                        runat="server"
                        NavigateUrl="~/index.aspx"
                        CssClass="active">

                        ⌂ Home

                    </asp:HyperLink>
                </li>


     

                <li>

                    <div class="dropdown">

                        <span>Details</span>

                        <div class="dropdown-content">

                            <asp:HyperLink
                                ID="HyperLink2"
                                runat="server"
                                NavigateUrl="~/book-details.aspx">

                                Manage Book

                            </asp:HyperLink>


                            <asp:HyperLink
                                ID="HyperLink3"
                                runat="server"
                                NavigateUrl="~/student-details.aspx">

                                Manage Student

                            </asp:HyperLink>


                            <asp:HyperLink
                                ID="HyperLink4"
                                runat="server"
                                NavigateUrl="~/teacher-details.aspx">

                                Manage Teacher 

                            </asp:HyperLink>

                        </div>

                    </div>

                </li>


  

                <li>

                    <div class="dropdown">

                        <span>Issue Book</span>

                        <div class="dropdown-content">

                            <asp:HyperLink
                                ID="HyperLink5"
                                runat="server"
                                NavigateUrl="~/student-issue-book.aspx">

                                Student 

                            </asp:HyperLink>


                            <asp:HyperLink
                                ID="HyperLink6"
                                runat="server"
                                NavigateUrl="~/teacher-issue-book.aspx">

                                Teacher 

                            </asp:HyperLink>

                        </div>

                    </div>

                </li>


    

                <li>

                    <asp:HyperLink
                        ID="HyperLink7"
                        runat="server"
                        NavigateUrl="~/return-book.aspx">

                        Return Book

                    </asp:HyperLink>

                </li>



                <li>

                    <div class="dropdown">

                        <span>Book Tools</span>

                        <div class="dropdown-content">


                            <asp:HyperLink
                                ID="HyperLink8"
                                runat="server"
                                NavigateUrl="~/issued-book-details.aspx">

                                Issued Book

                            </asp:HyperLink>


                            <asp:HyperLink
                                ID="HyperLink10"
                                runat="server"
                                NavigateUrl="~/search-students.aspx">

                                Student Records

                            </asp:HyperLink>


                            <asp:HyperLink
                                ID="HyperLink16"
                                runat="server"
                                NavigateUrl="~/search-teacher.aspx">

                                Teacher Records

                            </asp:HyperLink>


                            <asp:HyperLink
                                ID="HyperLink14"
                                runat="server"
                                NavigateUrl="~/fine-collection.aspx">

                                Late Fee

                            </asp:HyperLink>

                        </div>

                    </div>

                </li>


              

                <li>

                    <div class="dropdown no-arrow">

                        <span class="menu-icon">
                             ☰
                        </span>


                        <div class="dropdown-content">

                            <asp:HyperLink
                                ID="HyperLink18"
                                runat="server"
                                NavigateUrl="~/Switch_user.aspx">

                                Switch User

                            </asp:HyperLink>


                            <asp:HyperLink
                                ID="HyperLink17"
                                runat="server"
                                NavigateUrl="~/create-new-user.aspx">

                                Sign UP

                            </asp:HyperLink>


                            <asp:LinkButton
                                ID="btnLogout"
                                runat="server"
                                CssClass="logout-link"
                                OnClientClick="return confirmLogout();"
                                OnClick="btnLogout_Click">

                                Logout

                            </asp:LinkButton>

                        </div>

                    </div>

                </li>

            </ul>

        </nav>

    </header>


    <!-- =========================================
         WELCOME CARD
    ========================================== -->

    <div class="welcome-card">

        <asp:Label
            ID="lblUserName"
            runat="server"
            CssClass="welcome-label">
        </asp:Label>


        <asp:Label
            ID="lblUserName1"
            runat="server"
            CssClass="welcome-label1">
        </asp:Label>

        <br />


        <asp:Label
            ID="lblUserName2"
            runat="server"
            CssClass="welcome-label2">
        </asp:Label>

    </div>


    <!-- =========================================
         MAIN PANEL
    ========================================== -->

    <asp:Panel
        ID="Panel1"
        runat="server">


        <h2 class="head">
            📚 BOOK STATUS
        </h2>


        <div class="container">


            <!-- =====================================
                 SEARCH + FILTER
            ====================================== -->

            <div>

                <asp:Label
                    ID="Label6"
                    runat="server"
                    Text="SEARCH"
                    CssClass="label"
                    Width="100px">
                </asp:Label>


                <div class="status-search-wrapper">

                    <div class="status-search-input">                    
                    <asp:TextBox
                        ID="TextBoxSearch"
                        runat="server"
                        CssClass="status-search-box"
                        Placeholder="Search by Acc No, Author, Title, Subject..."
                        AutoPostBack="True"
                        OnTextChanged="TextBoxSearch_TextChanged">
                    </asp:TextBox>
                                <asp:LinkButton
            ID="btnSearch"
            runat="server"
            CssClass="status-search-icon"
            OnClick="btnSearch_Click"
            ToolTip="Search">

            🔍

        </asp:LinkButton>
                     </div>

                    <asp:Label
                        ID="Label5"
                        runat="server"
                        Text="STATUS"
                        CssClass="label"
                        Style="margin-left:20px;">
                    </asp:Label>


                    <asp:DropDownList
                        ID="DropDownListStatus"
                        runat="server"
                        CssClass="status-dropdown"
                        AutoPostBack="True"
                        OnSelectedIndexChanged="DropDownListStatus_SelectedIndexChanged">

                        <asp:ListItem
                            Text="All Books"
                            Value="ALL">
                        </asp:ListItem>

                        <asp:ListItem
                            Text="Issued"
                            Value="ISSUED">
                        </asp:ListItem>

                        <asp:ListItem
                            Text="Returned"
                            Value="AVAILABLE">
                        </asp:ListItem>

                        <asp:ListItem
                            Text="Never Issued"
                            Value="NEVER">
                        </asp:ListItem>

                    </asp:DropDownList>

                </div>

            </div>


            <!-- =====================================
                 SUMMARY
            ====================================== -->

            <div class="status-summary">


                <div class="status-card">

                    <asp:Label
                        ID="LabelTotalTitle"
                        runat="server"
                        Text="Total Books"
                        CssClass="status-card-title">
                    </asp:Label>

                    <asp:Label
                        ID="lblTotalBooks"
                        runat="server"
                        Text="0"
                        CssClass="status-card-value">
                    </asp:Label>

                </div>


                <div class="status-card">

                    <asp:Label
                        ID="LabelAvailableTitle"
                        runat="server"
                        Text="Returned / Available"
                        CssClass="status-card-title">
                    </asp:Label>

                    <asp:Label
                        ID="lblAvailableBooks"
                        runat="server"
                        Text="0"
                        CssClass="status-card-value">
                    </asp:Label>

                </div>


                <div class="status-card">

                    <asp:Label
                        ID="LabelIssuedTitle"
                        runat="server"
                        Text="Currently Issued"
                        CssClass="status-card-title">
                    </asp:Label>

                    <asp:Label
                        ID="lblIssuedBooks"
                        runat="server"
                        Text="0"
                        CssClass="status-card-value">
                    </asp:Label>

                </div>


                <div class="status-card">

                    <asp:Label
                        ID="LabelNeverTitle"
                        runat="server"
                        Text="Never Issued"
                        CssClass="status-card-title">
                    </asp:Label>

                    <asp:Label
                        ID="lblNeverIssued"
                        runat="server"
                        Text="0"
                        CssClass="status-card-value">
                    </asp:Label>

                </div>

            </div>


            <!-- =====================================
                 BOOK STATUS GRID
            ====================================== -->

            <div class="grid-container">


                <asp:GridView
                    ID="GridView1"
                    runat="server"
                    AutoGenerateColumns="False"
                    DataKeyNames="acc_no"
                    OnRowCommand="GridView1_RowCommand"
                    OnRowDataBound="GridView1_RowDataBound">


                    <Columns>


                        <asp:BoundField
                            DataField="acc_no"
                            HeaderText="Acc No" />


                        <asp:BoundField
                            DataField="title"
                            HeaderText="Book Title" />


                        <asp:BoundField
                            DataField="author"
                            HeaderText="Author" />


                        <asp:BoundField
                            DataField="subject"
                            HeaderText="Subject" />


                        <asp:BoundField
                            DataField="status"
                            HeaderText="Status" />


                        <asp:BoundField
                            DataField="issued_to"
                            HeaderText="Issued To" />


                        <asp:BoundField
                            DataField="user_type"
                            HeaderText="User Type" />


                        <asp:BoundField
                            DataField="issue_date"
                            HeaderText="Issue Date"
                            DataFormatString="{0:dd-MM-yyyy}" />


                        <asp:BoundField
                            DataField="return_date"
                            HeaderText="Return Date"
                            DataFormatString="{0:dd-MM-yyyy}" />


                      

                        <asp:TemplateField
                            HeaderText="Delete">

                            <ItemTemplate>

                                <asp:LinkButton
                                    ID="btnDelete"
                                    runat="server"
                                    Text="🗑"
                                    CssClass="deleteIcon"
                                    ToolTip="Delete Book"
                                    CommandName="DeleteBook"
                                    CommandArgument='<%# Eval("acc_no") %>'
                                    OnClientClick="return confirm('Are you sure you want to delete this book?');">

                                </asp:LinkButton>

                            </ItemTemplate>

                        </asp:TemplateField>


                    </Columns>

                </asp:GridView>


            </div>


        </div>

    </asp:Panel>


    <!-- =========================================
         FOOTER
    ========================================== -->

    <footer class="footer">

        © 2026 Vidhyant Library |
        Library Management System |
        All Rights Reserved.

    </footer>


    <script src="JavaScript3.js"></script>

</form>

</body>

</html>

