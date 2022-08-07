$(document).ready(function () {

    $('#tb_Author thead tr')
        .clone(true)
        .addClass('filters')
        .appendTo('#tb_Author thead');



    $('#tb_Author').DataTable({
        "ajax": {
            "url": "/Author/ListAuthors",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id" },
            { "data": "idBook" },
            { "data": "firstName" },
            { "data": "lastName" }

        ],
        "language":
        {
            url: "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"

        },
        columnDefs: [
            {
                targets: 0,
                visible: false

            }
        ],
        dom: "Bfrtip",
        buttons: [

            {
                extend: "excelHtml5",
                text: "Export Excel",
                filename: "Report of Authors",
                title: "",
                exportOptions: {
                    columns: [1, 2, 3]
                },
                className: "btn-export-exel"

            },
            {
                extend: "pdfHtml5",
                text: "Export PDF",
                filename: "Report of Authors",
                title: "",
                exportOptions: {
                    columns: [1, 2, 3]
                }

            },
            {
                extend: "print",
                title: "",
                exportOptions: {
                    columns: [1, 2, 3]
                }

            },

            "pageLength"

        ],
        orderCellsTop: true,
        fixedHeader: true,
        initComplete: function () {
            var api = this.api();

            // For each column
            api
                .columns()
                .eq(0)
                .each(function (colIdx) {
                    // Set the header cell to contain the input element
                    var cell = $('.filters th').eq(
                        $(api.column(colIdx).header()).index()
                    );
                    var title = $(cell).text();
                    //$(cell).html('<input type="text" placeholder="' + title + '" />');
                    $(cell).html('<input type="text" placeholder="Search..." />');

                    // On every keypress in this input
                    $(
                        'input',
                        $('.filters th').eq($(api.column(colIdx).header()).index())
                    )
                        .off('keyup change')
                        .on('keyup change', function (e) {
                            e.stopPropagation();

                            // Get the search value
                            $(this).attr('title', $(this).val());
                            var regexr = '({search})'; //$(this).parents('th').find('select').val();

                            var cursorPosition = this.selectionStart;
                            // Search the column for that value
                            api
                                .column(colIdx)
                                .search(
                                    this.value != ''
                                        ? regexr.replace('{search}', '(((' + this.value + ')))')
                                        : '',
                                    this.value != '',
                                    this.value == ''
                                )
                                .draw();

                            $(this)
                                .focus()[0]
                                .setSelectionRange(cursorPosition, cursorPosition);
                        });
                });
        },




    });

});
