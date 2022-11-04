

$(document).ready(function () {
    debugger;
    var id = getParameterByName('id');
    id=id == null ? 0 : id;
    if (id!=0) {
        getCarDetailById();
    }
    bindCarBrands();
    bindCarModels();
    function bindCarBrands() {
        var options = "";
        var list = ["Captur", "Clio", "Clio Grandtour", "Espace", "Express", "Fluence", "Agila", "Ampera", "Antara", "Astra", "Astra cabrio"];

        list.forEach(function (val, index) {
            options = options + "<option value=" + val + ">" + val + "</option>";
        })
        $("#brandName").append(options);

    }

    function bindCarModels() {
        var options = "";

        var list = ["Seat", "Renault", "Peugeot", "Dacia", "Citroën", "Opel", "Alfa Romeo"];

        list.forEach(function (val, index) {
            options = options + "<option value=" + val + ">" + val + "</option>";
        })
        $("#modelName").append(options);
    }

    function getCarDetailById(){
        $(".loader").show();
        $.ajax({
            url: "/Home/GetCarDetailById?id=" + parseInt(id),
            type: "GET",
            success: function (response) {
                $(".loader").hide();
                if (response.isSuccess) {
                    $("#carName").val(response.data.carName);
                    $("#modelName").val(response.data.modelName);
                    $("#brandName").val(response.data.brandName);
                    $("input[name='IsNew']").val(response.data.IsNew ? "Yes" : "No");
                    $("#price").val(response.data.price);
                } else {
                    window.location.href = "/";
                }
                
            },
            error: function (error) {

            }

        })
    }
    $("#btnSave").click(function () {
        var errorCount = 0;
        
        var carName = $("#carName").val();
        var modelName = $("#modelName").val();
        var brandName = $("#brandName").val();
        var isNew = $("input[name='IsNew']:checked").val();
        var price = $("#price").val();
        if (carName == "") {
            errorCount++;
            $("#errorMsgCarName").html("please enter car name");

        }
        if (carName != "") {
            if (checkSpecialCharacter(carName)) {
                errorCount++;
                $("#errorMsgCarName").html("Special character not allowed");
            }
            else {
                $("#errorMsgCarName").html("");
            }
        }

        if (isNew == "") {
            errorCount++;
            $("#errorMsgIsNew").html("please select is new yes or no")
        }
        if (modelName == "") {
            errorCount++;
            $("#errorMsgModelName").html("please select model name")
        }
        if (brandName == "") {
            errorCount++;
            $("#errorMsgBrandName").html("please select brand name")
        }
        if (price == "") {
            errorCount++;
            $("#errorMsgPrice").html("please enter price")
        }

        if (errorCount == 0) {
            $(".loader").show();
            var data = {
                carName: carName,
                price: price,
                brandName: brandName,
                modelName: modelName,
                isNew: isNew == "Yes" ? true : false
            }
            debugger;
            if (id == 0) {
              
                $.ajax({
                    url: "/Home/AddCarDetail",
                    type: "POST",
                    data: JSON.stringify(data),
                    dataType: "json",
                    contentType: "application/json",
                    success: function (response) {
                        $(".loader").hide();
                        $("#inventory-pop-modal").modal('show');
                        $("#isHdnSucess").val(response.isSuccess);
                        if (response.isSuccess) {
                            $("#popupMsg").html(response.message).css("color", "green");
                        }
                        else {
                            $("#popupMsg").html(response.message).css("color", "red");
                        }
                    },
                    error: function (error) {
                        $("#inventory-pop-modal").modal('show');
                        $("#isHdnSucess").val(false);
                        $("#popupMsg").html(response.message).css("color", "red");
                    }
                })
            }
            else {
                data.id = id;
                $.ajax({
                    url: "/Home/UpdateCarDetail",
                    type: "PUT",
                    data: JSON.stringify(data),
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (response) {
                        $(".loader").hide();
                        if (response.isSuccess) { 
                            $("#inventory-pop-modal").modal('show');
                            $("#isHdnSucess").val(response.isSuccess);
                            if (response.isSuccess) {
                                $("#popupMsg").html(response.message).css("color", "green");
                            }
                            else {
                                $("#popupMsg").html(response.message).css("color", "red");
                            }
                        }
                    },
                    error: function (error) {

                    }
                })
            }

        }

    })

    function checkSpecialCharacter(string) {
        const specialChars = /[`!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?~]/;


        if (specialChars.test(string)) {
            return true;
        } else {
            return false;
        }
    }
    function getParameterByName(name, url = window.location.href) {
        name = name.replace(/[\[\]]/g, '\\$&');
        var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
            results = regex.exec(url);
        if (!results) return null;
        if (!results[2]) return '';
        return decodeURIComponent(results[2].replace(/\+/g, ' '));
    }
    $("#carName").change(function () {
        debugger;
        var carName = $(this).val();
        if (checkSpecialCharacter(carName)) {
            $("#errorMsgCarName").html("Special character not allowed");
        }
        else {
            $("#errorMsgCarName").html("");
        }
    })

    $('#price').keypress(function (e) {
        debugger;
        var self = $(this);
        self.val(self.val().replace(/\D/g, ""));
        if ((e.which < 48 || e.which > 57)) {
            e.preventDefault();
        }
         
          //if (String.fromCharCode(charCode).match(/[^0-9]/g))
          //  return false;

    });
    $('#price').change(function () {
        if ($(this).val() == "") {
            $("#errorMsgPrice").html("please enter price name")
        }
        else {
            $("#errorMsgPrice").html("");
        }
        })
    $("#closeIcon").click(function () {
       
        var isHidden = $("#isHdnSucess").val();
        if (isHidden) {
            window.location.href = "/";
        }
    });

    $("#brandName").change(function () {

        if ($(this).val() != "") {
            $("#errorMsgBrandName").html("")
        }
        else {
            $("#errorMsgBrandName").html("please select brand name")
        }
    })
    $("#modelName").change(function () {

        if ($(this).val() != "") {
            $("#errorMsgModelName").html("")
        }
        else {
            $("#errorMsgModelName").html("please select model name")
        }
    })


})