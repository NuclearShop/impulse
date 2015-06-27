ImpulseApp.controller('TableController', function ($scope, $routeParams, uiGridConstants, SpinnerService, ServerQueryService, i18nService) {

    SpinnerService.AssignSpinner($scope, 'page-wrapper');
    i18nService.setCurrentLang('ru');
    $scope.showMap = false;
    var numericFilters = [
                            {
                                condition: function (searchTerm, cellValue) {
                                    return parseInt(cellValue) > parseInt(searchTerm);
                                },
                                placeholder: '>'
                            },
                            {
                                condition: function (searchTerm, cellValue) {
                                    return parseInt(cellValue) < parseInt(searchTerm);
                                },
                                placeholder: '<'
                            }
    ];
    $scope.gridOptions = {
        paginationPageSizes: [25, 50, 75],
        showColumnFooter: true,
        paginationPageSize: 25,
        enableSorting: true,
        enableFiltering: true,
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;
        },
        enableGridMenu: true,
        enableSelectAll: true,
        exporterCsvFilename: 'mpls.export.csv',
        exporterPdfDefaultStyle: { fontSize: 9 },
        exporterPdfTableStyle: { margin: [30, 30, 30, 30] },
        exporterPdfTableHeaderStyle: { fontSize: 10, bold: true, italics: true, color: 'red' },
        exporterPdfHeader: { text: "My Header", style: 'headerStyle' },
        exporterPdfFooter: function (currentPage, pageCount) {
            return { text: currentPage.toString() + ' из ' + pageCount.toString(), style: 'footerStyle' };
        },
        exporterPdfCustomFormatter: function (docDefinition) {
            docDefinition.styles.headerStyle = { fontSize: 22, bold: true };
            docDefinition.styles.footerStyle = { fontSize: 10, bold: true };
            return docDefinition;
        },
        exporterPdfOrientation: 'portrait',
        exporterPdfPageSize: 'LETTER',
        exporterPdfMaxGridWidth: 500,
        exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
    }
    var url = '';
    switch ($routeParams.type) {
        case 'click': url = '/Statistic/TableClickStatistics';
            $scope.header = "Статистика по кликам";
            break;
        case 'visitors': url = '/api/reports/visitors';
            $scope.header = "Уникальные посетители";
            $scope.gridOptions.columnDefs = [
                  {
                      field: 'Date', displayName: 'Дата', sortingAlgorithm: function (a, b) {
                          var nulls = $scope.gridApi.core.sortHandleNulls(a, b);
                          if (nulls !== null) {
                              return nulls;
                          } else {
                              datea = new Date(a.replace(/(\d{2}).(\d{2}).(\d{4})/, "$2/$1/$3"));
                              dateb = new Date(b.replace(/(\d{2}).(\d{2}).(\d{4})/, "$2/$1/$3"));
                              if (a.indexOf('От') >= 0) {
                                  datea = new Date(a.replace(/От (\d{2}).(\d{2}).(\d{4}) До (\d{2}).(\d{2}).(\d{4})/, "$2/$1/$3"));
                                  dateb = new Date(b.replace(/От (\d{2}).(\d{2}).(\d{4}) До (\d{2}).(\d{2}).(\d{4})/, "$2/$1/$3"));
                              }
                              if (datea === dateb) {
                                  return 0;
                              } else if (datea > dateb) {
                                  return 1;
                              }
                              return -1;
                          }
                      },
                      aggregationType: uiGridConstants.aggregationTypes.count
                  },
                  {
                      field: 'UniqueVisitors', displayName: 'Уникальные посетители',
                      aggregationType: uiGridConstants.aggregationTypes.max
                  },
                  {
                      field: 'AverageTime', displayName: 'Среднее время',
                      aggregationType: uiGridConstants.aggregationTypes.avg, filters: numericFilters
                  },
                  {
                      field: 'MaxTime', displayName: 'Максимальное время',
                      aggregationType: uiGridConstants.aggregationTypes.max, filters: numericFilters
                  }
            ];
            $scope.gridOptions.exporterPdfHeader.text = $scope.header;
            break;
        case 'locale': url = '/api/reports/locale';
            $scope.showMap = true;
            $scope.header = "Лояльность по странам";
            $scope.gridOptions.columnDefs = [
                  {
                      field: 'Locale', displayName: 'Локаль',
                      aggregationType: uiGridConstants.aggregationTypes.count
                  },
                  {
                      field: 'PopularPresentation', displayName: 'Самая популярная презентация'
                  },
                  {
                      field: 'ViewsByWeek', displayName: 'Просмотров за неделю',
                      aggregationType: uiGridConstants.aggregationTypes.avg, filters: numericFilters
                  },
                  {
                      field: 'ViewsByMonth', displayName: 'Просмотров за месяц',
                      aggregationType: uiGridConstants.aggregationTypes.avg, filters: numericFilters
                  }
            ];
            $scope.gridOptions.exporterPdfHeader.text = $scope.header;
            break;
    }
    var container = document.getElementById('tablediv');
    $scope.data = [];
    $scope.isShowAdSelector = $routeParams.type === 'click';
    $scope.loadTable = function () {
        var AdId = $("#Ads option:selected").val();
        var dateStart = $("#DateStart").val();
        var dateEnd = $("#DateEnd").val();
        var container = document.getElementById('page-wrapper');
        $scope.adspinner = new Spinner(spinnerOpts).spin();
        container.appendChild($scope.adspinner.el);
        if ($routeParams.type === 'click') {
            $.ajax({
                url: url,
                type: 'POST',
                data: {
                    AdId: AdId,
                    StartDate: dateStart,
                    EndDate: dateEnd
                },
                success: function (result) {
                    $scope.adspinner.stop();
                    success(result);
                }
            });
        } else {
            ServerQueryService.getReport(url + "?StartDate="
                + new Date($scope.dateStart).toDateString()
                + "&EndDate=" + new Date($scope.dateEnd).toDateString()+
                "&daysStep="+$scope.daysStep)
                .then(function (result) {
                    $scope.adspinner.stop();
                    $scope.gridOptions.data = result;

                },
                function (data) {
                    $scope.adspinner.stop();
                    console.log('AdController getAds error');
                });
        }


    };
    function success(result) {
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