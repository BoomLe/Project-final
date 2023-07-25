var dataTable;

$(document).ready(function () {
    loadDataTable();
});
function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        ajax: { url: "/Attandances/Index" },
        "columns": [
           
            { data: "datatime", width: "25%" },
            { data: "username", width: "15%" },
        ]
    });
}

$(document).ready(function () {
    $('.btn[data-toggle="collapse"]').on('click', function () {
        $(this).toggleClass('collapsed');
    });
});