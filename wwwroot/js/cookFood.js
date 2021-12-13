var foodTable;

$(document).ready(function () {
    loadFoodTable();
});

function loadFoodTable() {
    foodTable = $('#food_load').DataTable({
        "ajax": {
            "url": "/api/cookfood",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            
            /*{ "data": "cookID", "width": "5%" },*/
            { "data": "cookName", "width": "8%" },
            { "data": "cookQuantity", "width": "5%" },
            
            //{ "data": "cookLongtitude", "width": "5%" },
            //{ "data": "cookLatitude", "width": "5%" },
            
            { "data": "openDate", "width": "8%" },
            { "data": "closeDate", "width": "8%" },
           
            { "data": "remainQuantity", "width": "5%" },

            { "data": "reservation", "width": "5%" },
            { "data": "shopName", "width": "10%" },
            {
                "data": "cookID",
                "render": function (data) {
                    return `<div class="text-center">
                        <a href="/CookFoodPage/EditCookFood?id=${data}" class='btn btn-success text-white' style='cursor:pointer; width:90px; margin-bottom:5px;'>
                 Edit <i class="bi bi-pencil-square"></i>
                        </a>
                        <a class='btn btn-info text-white' style='cursor:pointer; width:90px;margin-bottom:5px;'
                             onclick=End('/api/cookfood?id='+${data})>
                            End <i class="bi bi-x-octagon"></i>
                        </a>
                        <a class='btn btn-danger text-white' style='cursor:pointer; width:90px;margin-bottom:5px;'
                             onclick=Delete('/api/cookfood?id='+${data})>
               Delete <i class="bi bi-trash-fill"></i>
                        </a>
                        </div>`;
                }, "width": "30%"
            }
        ],
        "language": {
            "emptyTable": "no data found"
        },
        "width": "100%"
    });
}

function End(url) {
    swal({
        title: "Are you sure?",
        text: "Once ended, the donation will not be displayed on map.",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willEnd) => {
        if (willEnd) {
            $.ajax({
                type: "POST",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        foodTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}

function Delete(url) {
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        foodTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}
