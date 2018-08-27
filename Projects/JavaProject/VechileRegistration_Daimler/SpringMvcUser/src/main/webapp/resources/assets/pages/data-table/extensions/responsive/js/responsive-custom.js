$(document).ready(function() {
    /*$('#res-config').DataTable({
        responsive: true
    });*/
    var newcs = $('#new-cons').DataTable({
										 
             "ordering": true, 
            //Set column definition initialisation properties.
            "columnDefs": [
                {
                    "targets": [0], //first, fourth & seventh column
                    "orderable": false //set not orderable
                }
            ]
										 
										 });

    new $.fn.dataTable.Responsive(newcs);

   /* $('#show-hide-res').DataTable({
        responsive: {
            details: {
                display: $.fn.dataTable.Responsive.display.childRowImmediate,
                type: ''
            }
        }
    });*/
});
