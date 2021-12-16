var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/api/dryfooddonation",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "dryFoodName", "width": "20%" },
            { "data": "dryFoodQuantity", "width": "20%" },
            { "data": "dryFoodPickDate", "width": "20%" },
            { "data": "dryFoodRemark", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                        
                        <a class="btn btn-success btn-sm" href="DryFoodDonationF/EditDryFoodDonation?id=${data}">Edit</a>
                        </a>
                        &nbsp;
                        
                        <a class="btn btn-danger btn-sm" onclick="return confirm('Are you sure to delete?')" href="DryFoodDonationF?id=${data}&handler=Delete">Delete</a>
                        
                        </div>`;
                }, "width": "50%"
            }
        ],
        "language": {
            "emptyTable": "no data found"
        },
        "width": "100vw"
    });
}


