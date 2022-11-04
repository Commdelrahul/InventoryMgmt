$(document).ready(function () {
 
    bindGrid();
    $("#closeIcon").click(function () {
        debugger;
        var isHidden = $("#isHdnSucess").val();
        if (isHidden) {
            bindGrid();
        }
    });
});
function bindGrid() {
    $("#customerDatatable").DataTable({

        ajax: {
            url: "Home/GetCars",
            type: "POST",
        },
        processing: true,
        serverSide: true,
        filter: true,
        bDestroy: true,
        "columns": [
            { "data": "id", "name": "Id", "autoWidth": true },
            { "data": "carName", "name": "Car Name", "autoWidth": true },
            { "data": "modelName", "name": "Model Name", "autoWidth": true },
            { "data": "brandName", "name": "Brand Name", "autoWidth": true },
            { "data": "isNew", "name": "Is New", "autoWidth": true },
            { "data": "createdOn", "name": "Created Date", "autoWidth": true },
            { "data": "updatedOn", "name": "UpdatedOn Date", "autoWidth": true },
            {
                "render": function (data, type, row) {
                    debugger;
                    return "<a href='javascript:void(0)' class='btn btn-danger' onclick=deleteCar('" + row.id + "'); >Delete</a> <a href='javascript:void(0)' class='btn btn-primary' onclick=editCar('" + row.id + "'); >Edit</a>";
                }
            },
        ]
    });

}

function deleteCar(id) {
    debugger
    if (confirm("Are you sure, you want to delete this record?")) {
        $.ajax({
            url: "/Home/DeleteCarDetail?id=" + parseInt(id),
            method: "DELETE",
            success: function (response) {
                
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
                alert("Some thing went wrong!");
            }

        })
    }
}

function editCar(id) {
    debugger;
    window.location.href = "/Home/AddCarDetail?id=" + id;
}



