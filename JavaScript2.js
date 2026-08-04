$(function ()
{
    // Match all relevant visible fields
    var $fields = $(':input:visible').filter(
        'input:text, input[type="email"], input[type="tel"], input[type="date"], ' +
        'input[type="number"], input[type="search"],input[type="password"], textarea'
    );

    // On load, focus the *first empty* field (skip autofilled)
    var firstEmpty = $fields.filter(function ()
    {
        return !$(this).val().trim();
    }).first();
    if (firstEmpty.length)
    {
        firstEmpty.focus();
    }

    // Handle Enter key – jump to next empty field
    $('body').on('keydown', $fields.selector, function (e)
    {
        if (e.which === 13) {
            e.preventDefault();
            var idx = $fields.index(this);
            for (var i = idx + 1; i < $fields.length; i++)
            {
                if (!$fields.eq(i).val().trim()) {
                    return $fields.eq(i).focus();
                }
            }
            // Agar next empty na mile, cursor wahi rahega
        }
    });
});

// Show / Hide Password
function togglePassword() {
    var txt = document.getElementById("TextBox2");
    var eye = document.getElementById("eyeIcon");

    if (txt.type === "password") {
        txt.type = "text";
        eye.className = "fa-solid fa-eye-slash";
    }
    else {
        txt.type = "password";
        eye.className = "fa-solid fa-eye";
    }
}