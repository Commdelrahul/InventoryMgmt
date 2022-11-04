$(document).ready(function () {
   
    $("#login").click(function () {
        var email = $("#email").val();
        var password = $("#password").val();
        $("#loginErrorMsg").html("");
        var errorCount = 0;
        if (email != "") {
            if (!validateEmail(email)) {
                errorCount++;
                $("#emailErrorMsg").html("You have entered an invalid email address!")
            }

        }
        if (email == "") {
            errorCount++;
            $("#emailErrorMsg").html("Please enter email");
        }

        if (password == "") {
            errorCount++;
            $("#passwordErrorMsg").html("Please password email");
        }
        if (password != "") {
            var errors = validatePassword(password);
            if (errors.length > 0) {
                errorCount++;
                var msg = errors.join(',');
                $("#passwordErrorMsg").html(msg);

            }
        }

        if (errorCount == 0) {
            $(".loader").show();
            var data = {
                email: email,
                password: password
            };
            $.ajax({
                async: false,
                url: "/Account/Login",
                type: "Post",
                data: JSON.stringify(data),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    $(".loader").hide();
                    console.log(response);
                    if (response.ErrorMessage != null) {
                        $("#loginErrorMsg").html(response.errorMessage);
                    }
                    else {
                        window.location.href = "/";
                    }
                },
                error: function (error) {

                }



            })
        }



    });

    function validateEmail(email) {
        if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(email)) {
            return (true)
        }

        return (false)
    }
    function validatePassword(password) {

        errors = [];
        if (password.length < 8) {
            errors.push("Your password must be at least 8 characters");
        }
        if (password.search(/[a-z]/i) < 0) {
            errors.push("Your password must contain at least one letter.");
        }
        if (password.search(/[0-9]/) < 0) {
            errors.push("Your password must contain at least one digit.");
        }
        //if (errors.length > 0) {

        //    return errors;
        //}
        return errors;
    }

    $("#email").change(function () {
        var email = $(this).val();
        $("#loginErrorMsg").html("");

        if (email != "") {
            if (!validateEmail(email)) {
                $("#emailErrorMsg").html("You have entered an invalid email address!");
            }
            else {
                $("#emailErrorMsg").html("");
            }
        }
        else {
            $("#emailErrorMsg").html("Please enter email");
        }

    })
    $("#password").change(function () {
        $("#loginErrorMsg").html("");

        var password = $(this).val();
        if (password != "") {
            var errors = validatePassword(password);
            if (errors.length > 0) {
                $("#passwordErrorMsg").html(errors.join(','));
            }
            else {
                $("#passwordErrorMsg").html("");
            }
        }
        else {
            $("#passwordErrorMsg").html("Please enter password");
        }
    })
});