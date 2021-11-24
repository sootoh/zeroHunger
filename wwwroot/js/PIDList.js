var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/api/productinneed",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "product_id", "width": "5%" },
            { "data": "product_name", "width": "10%" },
            { "data": "product_description", "width": "10%" },
            { "data": "amount", "width": "5%" },
           
            { "data": "visibility", "width": "5%" },
            { "data": "image", "width": "5%" },
            {
                "data": "product_id",
                "render": function (data) {
                    return `<div class="text-center">
                        <a href="/ProductInNeedList/Update?id=${data}" class='btn btn-success text-white' style='cursor:pointer; width:70px;'>
                            Edit
                        </a>
                        &nbsp;
                        <a class='btn btn-danger text-white' style='cursor:pointer; width:70px;'
                            onclick=Delete('/api/productinneed?id='+${data})>
                            Delete
                        </a>
                        </div>`;
                }, "width": "50%"
            }
        ],
        "language": {
            "emptyTable": "no data found"
        },
        "width": "100%"
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
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}
