var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#Delivery_load').DataTable({
        "ajax": {
            "url": "/api/delivery",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "volunteer.userName", "width": "5%" },
            { "data": "deliveryTime", "width": "15%" },
            {
                "data": "deliveryStatus",
                "render": function (data) {
                    var status;
                    if (data == 0) {
                        status = "pending";
                    }
                    else if (data == 1) {
                        status = "incomplete";
                    }
                    else if (data == 2) {
                        status = "complete";
                    }
                    else if (data == 3) {
                        status = "rejected";
                    }
                    return status;
                }, "width": "2%"
            },
            { "data": "receiver.userName", "width": "5%" },
            { "data": "receiver.userPhone", "width": "5%" },
            {
                "data": "receiver.userAdrs1",
                "render": function (data, type, row) {
                    return data + ' ' + row.receiver.userAdrs2;
            }, "width": "15%"
            },
            {
                "data": "deliveryID",
                "render": function (data) {
                    return `<td>
                        <a href="/Deliveries/DeliveryItem?id=${data}" class='btn btn-info text-white' style='cursor:pointer; width:100px;'>
                            Update &nbsp;<i class="bi bi-pencil-square"></i>
                        </a>
                        </td>`;
                }, "width": "10%"
            },
            {
                "data": "deliveryID",
                "render": function (data) {
                    return `<td>
                        <a href="/Deliveries/Edit?id=${data}" class="btn btn-success mx-2 text-white" style="cursor:pointer;width:100px">
                        Edit &nbsp;<i class="bi bi-pencil-square"></i>
                        </a>
                        </td>`;
                }, "width": "10%"
            },
            {
                "data": "deliveryID",
                "render": function (data) {
                    return `<td>
                        <a class="btn btn-danger mx-2 text-white" style="cursor:pointer;width:100px"
                           onclick=Delete('/api/delivery?id='+${data})>
                        Delete &nbsp;<i class="bi bi-trash-fill"></i>
                        </a>
                        </td>`;
                }, "width": "10%"
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
















