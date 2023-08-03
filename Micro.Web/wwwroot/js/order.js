//Retrieving data from the get all endpoint
var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: "/order/getall" },
        "columns": [
            { data: 'orderHeaderId', "width":"5%"},
            { data: 'email', "width": "25%" },
            { data: 'email', "width": "20%" },
            { data: 'email', "width": "10%" },
            { data: 'email', "width": "10%" },
            { data: 'email', "width": "10%" },
        ]
    })
}
