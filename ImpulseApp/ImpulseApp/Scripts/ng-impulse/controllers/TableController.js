ImpulseApp.controller('TableController', function ($scope, $routeParams, SpinnerService) {

    SpinnerService.AssignSpinner($scope, 'page-wrapper');
    var url = '';
    switch ($routeParams.type) {
        case 'click': url = '/Statistic/TableClickStatistics';
            break;
    }
    var container = document.getElementById('tablediv');
    

    $scope.loadTable = function () {
        var AdId = $("#Ads option:selected").val();
        var dateStart = $("#DateStart").val();
        var dateEnd = $("#DateEnd").val();
        $.ajax({
            url: url,
            type: 'POST',
            data: {
                AdId: AdId,
                StartDate: dateStart,
                EndDate: dateEnd
            },
            success: function (result) {
                $("#tablediv").html(result);
                $('#ClickStat').DataTable({
                    "paging": true, "language": {
                        "processing": "Подождите...",
                        "search": "Поиск:",
                        "lengthMenu": "Показать _MENU_ записей",
                        "info": "Записи с _START_ до _END_ из _TOTAL_ записей",
                        "infoEmpty": "Записи с 0 до 0 из 0 записей",
                        "infoFiltered": "(отфильтровано из _MAX_ записей)",
                        "infoPostFix": "",
                        "loadingRecords": "Загрузка записей...",
                        "zeroRecords": "Записи отсутствуют.",
                        "emptyTable:": "В таблице отсутствуют данные",
                        "paginate": {
                            "first": "Первая",
                            "previous": "Предыдущая",
                            "next": "Следующая",
                            "last": "Последняя"
                        },
                        "aria": {
                            "sortAscending": ": активировать для сортировки столбца по возрастанию",
                            "sortDescending": ": активировать для сортировки столбца по убыванию"
                        }
                    },
                    "dom": 'CT<"clear">lfrtip',
                    "tableTools": {
                        "sSwfPath": "/Content/bower_componenets/datatables-plugins/copy_csv_xls_pdf.swf",
                        "aButtons": [
                            "xls"

                        ]
                    },
                    "colVis": {
                        "buttonText": "Изменить колонки"
                    },
                    responsive: true,
                });
            }
        });
    };

    $scope.generateReport = function (isAll) {
        //var isAll = $(this).data('val');
        var params = {
            pageTitle: '@ViewBag.Title',
            htmlText: escape($('<table class="table table-striped table-bordered table-hover dataTable no-footer">' + $('#ClickStat').html() + '</table>'))//.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;")
        };
        var tt = TableTools.fnGetInstance("ClickStat");
        $.ajax({
            url: '/PdfExport/ViewPdf',
            type: 'POST',
            data: params,
            success: function (response, status, request) {
                
                var disp = request.getResponseHeader('Content-Disposition');
                if (disp && disp.search('attachment') != -1) {
                    $("#export-form").remove();
                    var form = $('<form id="export-form" method="POST" action="/PdfExport/ViewPdf">');
                    $.each(params, function (k, v) {
                        if (k === 'htmlText') {
                            form.append('<input id = "export" type="hidden" name="' + k +
                                '" value="' + v + '" />');
                        } else {
                            form.append('<input type="hidden" name="' + k +
                                '" value="' + v + '" />');
                        }

                    });
                    $('body').append(form);

                    tt.fnPrint(true, { bShowAll: isAll });
                    $("#export").val(escape('<table class="table table-striped table-bordered table-hover dataTable no-footer">' + $('#ClickStat').html() + '</table>'));
                    tt.fnPrint(false);
                    form.submit();
                }
            }
        });
    }

});