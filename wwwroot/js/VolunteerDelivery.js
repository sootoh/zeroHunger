var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#volunteer_load').DataTable({
        "ajax": {
            "url": "/api/volunteerdelivery",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "receiver.userName", "width": "5%" },
            { "data": "receiver.userPhone", "width": "5%" },
            {
                "data": "receiver.userAdrs1",
                "render": function (data, type, row) {
                    return data + ' ' + row.receiver.userAdrs2;
                }, "width": "15%"
            },
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
            {
                "data": "deliveryID",
                "render": function (data) {
                    return `
                        <a href="/Deliveries/ViewDeliveryItem?id=${data}" class='btn btn-info text-white' style='cursor:pointer; width:100px;'>
                            View 
                        </a>`;
                }, "width": "10%"
            },
            {
                "data": "deliveryStatus",
                "render": function (data, type, row) {
                    if (data == 0) {
                        return `<td>
                         <a class='btn btn-success text-white' style='cursor:pointer; width:100px;'
                             onclick=Accept('/api/volunteerdelivery/Accept?id='+${row.deliveryID})>
                            Accept 
                        </a></td><br><td>
                        <a class='btn btn-danger text-white' style='cursor:pointer; width:100px;'
                             onclick=Reject('/api/volunteerdelivery/Reject?id='+${row.deliveryID})>
                            Reject
                        </a></td><br><td>
                        <a class='btn btn-warning text-white' style='visibility:hidden;cursor:pointer; width:100px;'
                             onclick=Complete('/api/volunteerdelivery/Complete?id='+${row.deliveryID})>
                            Complete
                        </a>
                        </td>`
                    }
                    else if (data == 1) {
                        return `<td></td>
                                <td></td>
                                <td>
                                <a class='btn btn-warning text-white' style='cursor:pointer; width:120px;'
                                onclick=Complete('/api/volunteerdelivery/Complete?id='+${row.deliveryID})>
                                Complete &nbsp;<i class="bi bi-check-square-fill"></i>
                                </a>
                                </td>`
                    }
                    else if (data == 2) {
                        return `<td></td>
                                <td></td>
                                <td></td>`
                    }
                }, "width": "10%"
            }
        ],
        "language": {
            "emptyTable": "no data found"
        },
        "width": "100%"
    });
}

function Accept(url) {
    $.ajax({
        type: "POST",
        url: url,
        success: function (data) {
            if (data.success) {
                toastr.success(data.message);
                dataTable.ajax.reload();
            }
            else {
                toastr.error(data.message);
                dataTable.ajax.reload();
            }
        }
    });
}
function Reject(url) {
    $.ajax({
        type: "POST",
        url: url,
        success: function (data) {
            if (data.success) {
                toastr.success(data.message);
                dataTable.ajax.reload();
            }
            else {
                toastr.error(data.message);
                dataTable.ajax.reload();
            }
        }
    });
}
function Complete(url) {
    $.ajax({
        type: "POST",
        url: url,
        success: function (data) {
            if (data.success) {
                toastr.success(data.message);
                dataTable.ajax.reload();
            }
            else {
                toastr.error(data.message);
                dataTable.ajax.reload();
            }
        }
    });

}
        