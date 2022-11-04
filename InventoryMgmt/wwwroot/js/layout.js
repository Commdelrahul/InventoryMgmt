$(document).ready(function () {
 
    $("#btnLogout").click(function () {

        $.ajax({
            url:"/Account/Logout",
            type: "POST",
            success: function (response) {
                window.location.href="/Account/Login"
            },
            error: function (error) {

            }
        })
    });
});