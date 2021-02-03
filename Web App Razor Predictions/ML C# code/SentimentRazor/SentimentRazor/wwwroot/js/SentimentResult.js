var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "api/sentiment",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "text", "width": "20%" },
            { "data": "sentimentPrediction", "width": "20%" },
            { "data": "confidenceScore", "width": "20%" },
            { "data": "actualSentiment", "width": "20%" }, //cammel case so that the first letter is lower case no matter what

            {
                "data": "id",
                "render": function (data) { //this data here has the id
                    return `
                        <div class="text-center">

   
                        <a class='btn btn-danger text-white' style='cursor:pointer; width:70px;'
                            onclick=Delete('/api/sentiment?id='+${data})>
                            Delete
                        </a>
                        </div>`;
                }, "width": "40%"
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
        buttons: true, // this is so you can cancel or hit ok in the pop up when delete
        dangerMode: true
    }).then((willDelete) => { // this is the delete message 
        if (willDelete) {
            $.ajax({
                type: "DELETE", //what is this type of ajax call?
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


//<div class="text-center">
//    <a href="/SavePredictionList/Upsert?id=${data}" class='btn btn-success text-white' style='cursor:pointer; width:70px;'>
//        Edit
//                        </a>
//                        &nbsp;