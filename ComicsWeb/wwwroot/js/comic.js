var dataTable;

$(document).ready(function(){
    loadDataTable();
});

function loadDataTable(){
    dataTable = $('#tableData').DataTable({
        "ajax": {
            url: "/admin/comic/getall",
        },
        "columns": [
            { data: 'title', "width": "25%" },
            {data: 'inStock', "width": "15%" },
            { data: 'price', "width": "15%" },
            { data: 'category.name', "width": "10%" },
            { 
                data: 'id', 
                "render": function(data) {
                    return `
                        <div class="w-75 btn-group d-flex gap-2" role="group">
                        <a href="/admin/comic/upsert?id=${data}" class="btn btn-primary flex-fill">
                            <i class="bi bi-pencil-square"></i> Edit
                        </a>
                        <a onClick=Delete("/admin/comic/delete?id=${data}") class="btn btn-danger flex-fill">
                            <i class="bi bi-trash-fill"></i> Delete
                        </a>
                        </div>
                    `
                },
                "width": "35%" 
            },
            
        ]
    });
}


function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {
                    dataTable.ajax.reload();
                }
            });
        }
    });
}

