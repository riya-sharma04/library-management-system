function confirmLogout() {
    return confirm("Are you sure you want to logout?");
}


/* =========================================
   MOBILE DROPDOWN
========================================= */

document.addEventListener("DOMContentLoaded", function () {

    // Only run dropdown code on mobile
    if (window.innerWidth <= 767) {

        const dropdowns = document.querySelectorAll(".dropdown");

        dropdowns.forEach(function (dropdown) {

            const trigger = dropdown.querySelector("span");

            if (!trigger) {
                return;
            }

            trigger.addEventListener("click", function (e) {

                e.preventDefault();
                e.stopPropagation();

                // Close all other dropdowns
                dropdowns.forEach(function (otherDropdown) {

                    if (otherDropdown !== dropdown) {
                        otherDropdown.classList.remove("open");
                    }

                });

                // Open / close selected dropdown
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

    }

});