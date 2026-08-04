<%@ Control Language="C#" AutoEventWireup="true"
    CodeFile="MobileNavigation.ascx.cs"
    Inherits="MobileNavigation" %>
<link href="MobileNavigation.css"
      rel="stylesheet"
      type="text/css" />

<!-- =====================================================
     MOBILE NAVIGATION
     Desktop par hidden rahega
===================================================== -->

<div class="mobile-navigation">

    <!-- TOP RIGHT ICONS -->

    <div class="mobile-navigation-icons">

        <!-- Main Library Drawer -->
        <button type="button"
                class="mobile-drawer-open"
                aria-label="Open Library Menu">
            ☰
        </button>


        <!-- Existing Menu -->
        <button type="button"
                class="mobile-account-open"
                aria-label="Open Account Menu">
            ⚙️
        </button>

    </div>


    <!-- =================================================
         ACCOUNT MENU
         Switch User / Sign Up / Logout
    ================================================= -->

    <div class="mobile-account-menu">

        <asp:HyperLink ID="lnkSwitchUser"
            runat="server"
            NavigateUrl="~/Switch_user.aspx">
            Switch User
        </asp:HyperLink>

        <asp:HyperLink ID="lnkSignUp"
            runat="server"
            NavigateUrl="~/create-new-user.aspx">
            Sign Up
        </asp:HyperLink>

        <asp:LinkButton ID="btnMobileLogout"
            runat="server"
            CssClass="mobile-logout"
            OnClientClick="return confirmLogout();"
            OnClick="btnMobileLogout_Click">
            Logout
        </asp:LinkButton>

    </div>


    <!-- =================================================
         DARK OVERLAY
    ================================================= -->

    <div class="mobile-drawer-overlay"></div>


    <!-- =================================================
         RIGHT SIDE DRAWER
    ================================================= -->

    <aside class="mobile-library-drawer">

        <!-- Drawer Header -->

        <div class="mobile-drawer-header">

            <span>Library Menu</span>

            <button type="button"
                    class="mobile-drawer-close"
                    aria-label="Close Library Menu">
                ×
            </button>

        </div>


        <!-- Drawer Content -->

        <div class="mobile-drawer-content">


            <!-- =================================================
                 DETAILS
            ================================================= -->

            <div class="mobile-menu-group">

                <button type="button"
                        class="mobile-main-menu">

                    <span>
                        <span class="mobile-menu-icon">▣</span>
                        Details
                    </span>

                    <span class="mobile-menu-arrow">⌄</span>

                </button>


                <div class="mobile-submenu">

                    <asp:HyperLink ID="lnkManageBook"
                        runat="server"
                        NavigateUrl="~/book-details.aspx">
                        <span>▣</span>
                        Manage Book
                    </asp:HyperLink>

                    <asp:HyperLink ID="lnkManageStudent"
                        runat="server"
                        NavigateUrl="~/student-details.aspx">
                        <span>♙</span>
                        Manage Student
                    </asp:HyperLink>

                    <asp:HyperLink ID="lnkManageTeacher"
                        runat="server"
                        NavigateUrl="~/teacher-details.aspx">
                        <span>♟</span>
                        Manage Teacher
                    </asp:HyperLink>

                </div>

            </div>


            <!-- =================================================
                 ISSUE BOOK
            ================================================= -->

            <div class="mobile-menu-group">

                <button type="button"
                        class="mobile-main-menu">

                    <span>
                        <span class="mobile-menu-icon">▤</span>
                        Issue Book
                    </span>

                    <span class="mobile-menu-arrow">⌄</span>

                </button>


                <div class="mobile-submenu">

                    <asp:HyperLink ID="lnkStudentIssue"
                        runat="server"
                        NavigateUrl="~/student-issue-book.aspx">
                        <span>♙</span>
                        Student
                    </asp:HyperLink>

                    <asp:HyperLink ID="lnkTeacherIssue"
                        runat="server"
                        NavigateUrl="~/teacher-issue-book.aspx">
                        <span>♟</span>
                        Teacher
                    </asp:HyperLink>

                </div>

            </div>


            <!-- =================================================
                 RETURN BOOK
            ================================================= -->

            <div id="grpReturnBook"
     runat="server"
     class="mobile-menu-group mobile-direct-menu">

                <asp:HyperLink ID="lnkReturnBook"
                    runat="server"
                    NavigateUrl="~/return-book.aspx"
                    CssClass="mobile-main-menu mobile-direct-link">

                    <span>
                        <span class="mobile-menu-icon">↔</span>
                        Return Book
                    </span>

                </asp:HyperLink>

            </div>


            <!-- =================================================
                 BOOK TOOLS
            ================================================= -->

            <div class="mobile-menu-group">

                <button type="button"
                        class="mobile-main-menu">

                    <span>
                        <span class="mobile-menu-icon">⚙</span>
                        Book Tools
                    </span>

                    <span class="mobile-menu-arrow">⌄</span>

                </button>


                <div class="mobile-submenu">

                    <asp:HyperLink ID="lnkIssuedRecords"
                        runat="server"
                        NavigateUrl="~/issued-book-details.aspx">
                        <span>▤</span>
                        Issued Book Records
                    </asp:HyperLink>

                    <asp:HyperLink ID="lnkBookRecords"
                        runat="server"
                        NavigateUrl="~/search-books.aspx">
                        <span>▣</span>
                        Book Records
                    </asp:HyperLink>

                    <asp:HyperLink ID="lnkStudentRecords"
                        runat="server"
                        NavigateUrl="~/search-students.aspx">
                        <span>♙</span>
                        Student Records
                    </asp:HyperLink>

                    <asp:HyperLink ID="lnkTeacherRecords"
                        runat="server"
                        NavigateUrl="~/search-teacher.aspx">
                        <span>♟</span>
                        Teacher Records
                    </asp:HyperLink>

                    <asp:HyperLink ID="lnkLateFee"
                        runat="server"
                        NavigateUrl="~/fine-collection.aspx">
                        <span>₹</span>
                        Late Fee
                    </asp:HyperLink>

                    <asp:HyperLink ID="lnkBookStatus"
                        runat="server"
                        NavigateUrl="~/Book-status.aspx">
                        <span>▥</span>
                        Book Status
                    </asp:HyperLink>

                </div>

            </div>

        </div>

    </aside>

</div>
<script type="text/javascript">

document.addEventListener("DOMContentLoaded", function() {

    var openButton =
        document.querySelector(".mobile-drawer-open");

    var closeButton =
        document.querySelector(".mobile-drawer-close");

    var drawer =
        document.querySelector(".mobile-library-drawer");

    var overlay =
        document.querySelector(".mobile-drawer-overlay");

    var accountButton =
        document.querySelector(".mobile-account-open");

    var accountMenu =
        document.querySelector(".mobile-account-menu");


    /* =========================================
       OPEN LIBRARY DRAWER
    ========================================= */

    if (openButton && drawer && overlay) {

        openButton.onclick = function() {

            drawer.classList.add("active");

            overlay.classList.add("active");

            document.body.classList.add(
                "mobile-navigation-open"
            );

        };

    }


    /* =========================================
       CLOSE DRAWER
    ========================================= */

    if (closeButton && drawer && overlay) {

        closeButton.onclick = function() {

            drawer.classList.remove("active");

            overlay.classList.remove("active");

            document.body.classList.remove(
                "mobile-navigation-open"
            );

        };

    }


    /* =========================================
       OVERLAY CLICK
    ========================================= */

    if (overlay && drawer) {

        overlay.onclick = function() {

            drawer.classList.remove("active");

            overlay.classList.remove("active");

            document.body.classList.remove(
                "mobile-navigation-open"
            );

        };

    }


    /* =========================================
       MAIN MENU ACCORDION
    ========================================= */

    var menuGroups =
        document.querySelectorAll(
            ".mobile-menu-group"
        );


    for (var i = 0; i < menuGroups.length; i++) {

        (function(group) {

            var menu =
                group.querySelector(
                    ".mobile-main-menu"
                );

            var submenu =
                group.querySelector(
                    ".mobile-submenu"
                );


            if (!menu || !submenu) {
                return;
            }


            menu.onclick = function(event) {

                event.preventDefault();

                event.stopPropagation();


                var alreadyOpen =
                    group.classList.contains("open");


                /* Close other menus */

                for (
                    var j = 0;
                    j < menuGroups.length;
                    j++
                ) {

                    menuGroups[j].classList.remove(
                        "open"
                    );

                }


                /* Open current menu */

                if (!alreadyOpen) {

                    group.classList.add("open");

                }

            };

        })(menuGroups[i]);

    }


    /* =========================================
       ACCOUNT MENU
    ========================================= */

    if (accountButton && accountMenu) {

        accountButton.onclick = function(event) {

            event.preventDefault();

            event.stopPropagation();

            accountMenu.classList.toggle("open");

        };

    }


    /* =========================================
       ESCAPE KEY
    ========================================= */

    document.addEventListener(
        "keydown",
        function(event) {

            if (event.key === "Escape") {

                if (drawer) {
                    drawer.classList.remove("active");
                }

                if (overlay) {
                    overlay.classList.remove("active");
                }

                if (accountMenu) {
                    accountMenu.classList.remove("open");
                }

                document.body.classList.remove(
                    "mobile-navigation-open"
                );

            }

        }
    );

});

</script>