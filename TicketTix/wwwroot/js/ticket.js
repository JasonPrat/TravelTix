
var dataTable;
$(document).ready(function () {
    loadDataTable();
});
function loadDataTable() {
    dataTable = $('#ticket').DataTable({
        "ajax": {
            "url": "/Admin/Ticket/GetAll"
        },
        "columns": [
            { "data": "airline.name", "width": "20%" },
            { "data": "departure",
                "render":  function(data) {
                    var date = new Date(data);
                    var month = date.getMonth() + 1;
                    return (month.toString().length > 1 ? month : "0" + month) + "/" + date.getDate() + "/" + date.getFullYear() + " "
                        + (date.getHours().toString().length > 1 ? date.getHours() : "0" + date.getHours()) + ":" +
                        (date.getMinutes().toString().length > 1 ? date.getMinutes() : "0" + date.getMinutes());
                }, "width": "20%" },
            { "data": "arrival",
                "render": function (data) {
                    var date = new Date(data);
                    var month = date.getMonth() + 1;
                    return (month.toString().length > 1 ? month : "0" + month) + "/" + date.getDate() + "/" + date.getFullYear() + " "
                        + (date.getHours().toString().length > 1 ? date.getHours() : "0" + date.getHours()) + ":" +
                        (date.getMinutes().toString().length > 1 ? date.getMinutes() : "0" + date.getMinutes());
                }
                , "width": "20%" },
            { "data": "price", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="w-60 btn-group " role="group">
                       <a href="/Admin/Ticket/Upsert?id=${data}"
                        class="btn btn-primary mx-2>
                            <i class="bi bi-pencil"></i> Edit</a>
                       <a onClick=Delete('/Admin/Ticket/Delete/${data}')
                        class="btn btn-danger mx-2">
                        
                             Delete</a>
                         </div>
                    `
                },
                "width": "20%"
            }
        ]
    });

}
function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}