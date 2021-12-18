var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/api/delivery",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            /*"dataSrc": function (data) {
                for (var i = 0, ien = json.data.length; i < ien; i++) {
                    json.data[i][0] = '<a href="/message/' + json.data[i][0] + '>View message</a>';
                },*/
            { "data": "volunteer.UserName", "width": "5%" },
            { "data": "deliveryTime", "width": "10%" },
            { "data": "deliveryStatus", "width": "2%" },
            { "data": "Receiver.userName", "width": "5%" },
            { "data": "Receiver.userPhone", "width": "5%" },
            { "data": "Receiver.userAdrs1 Receiver.userAdrs2", "width": "15%" },
            {
                "data": "deliveryid",
                "render": function (data) {
                    return `<td>
                        <a href="/Deliveries/DeliveryItem?id=${data}" class='btn btn-info text-white' style='cursor:pointer; width:70px;'>
                            Update &nbsp;<i class="bi bi-pencil-square"></i>
                        </a>
                        </td>`;
                }, "width": "10%"
            },
            {
                "data": "deliveryid",
                "render": function (data) {
                    return `<td>
                        <a href="/Deliveries/Edit?id=${data}" class="btn btn-success mx-2 text-white" style="cursor:pointer;width:80px">
                        Edit &nbsp;<i class="bi bi-pencil-square"></i>
                        </a>
                        </td>`;
                }, "width": "10%"
            },
            {
                "data": "deliveryid",
                "render": function (data) {
                    return `<td>
                        <a href="/api/delivery?id=${data}" class="btn btn-danger mx-2 text-white" style="cursor:pointer;width:100px">
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
















/*
{
                "data": "Receiver",
                "render": function () {
                    return `data.userAdrs1 + data.userAdrs2`;
                }, "width": "50%"
            }
            */