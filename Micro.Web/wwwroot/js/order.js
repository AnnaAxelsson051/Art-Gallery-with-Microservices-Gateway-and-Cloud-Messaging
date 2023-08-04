﻿//Retrieving data from the get all endpoint
var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        order: [[0, 'desc']],
        "ajax": { url: "/order/getall" },
        "columns": [
            { data: 'orderHeaderId', "width":"5%"},
            { data: 'email', "width": "25%" },
            { data: 'email', "width": "20%" },
            { data: 'email', "width": "10%" },
            { data: 'email', "width": "10%" },
            { data: 'email', "width": "10%" },
            {
                data: 'orderHeaderId',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                    <a href="/order/orderDetail?orderId=${data}" class="btn btn-primary mx-2"><i class="bi bi-pencil-square"></i></a>
                    </div>
                },
                "width": "10%"
            }
        ]
    })
}
