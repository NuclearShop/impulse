var chartColor = new Array();
var removedDD = new Array();
var AdIds = new Array();

var HighChartHelper = function () {
    
}

HighChartHelper.chart = undefined;

HighChartHelper.dropdownTemp = undefined;

HighChartHelper.dispose = function () {
    if (HighChartHelper.chart !== undefined) {
        HighChartHelper.chart.destroy();
        chartColor = new Array();
        if (HighChartHelper.dropdownTemp !== undefined) {
            $("#Ads").html(HighChartHelper.dropdownTemp);
        }
        removedDD = new Array();
        AdIds = new Array();
    }
}

HighChartHelper.reload = function (chartType, chartText, legend, url, clickCallback) {
    var container = document.getElementById('canvasContainer');
    var spinner = new Spinner(spinnerOpts).spin();
    container.appendChild(spinner.el);
    var label = new Array();

    var clickCount = new Array();
    var startDate = $('#start-date-stats').val();
    var endDate = $('#end-date-stats').val();
    $.ajax({
        url: url,
        data: {
            AdIds: AdIds,
            sDate: startDate,
            eDate: endDate
        },
        type: 'POST',
        success: function (incoming) {
            spinner.stop();
            var data = {
                categories: incoming.charts[0].labels,
                series: []
            };
            _.each(incoming.charts, function (chart) {
                var numericData = [];
                _.each(chart.data, function (value) {
                    numericData[numericData.length] = parseInt(value);
                })
                if (chartType === 'funnel') {
                    var i = 0;
                    var funnalData = [];
                    _.each(numericData, function (value) {
                        funnalData[funnalData.length] = new Array(chart.labels[i], value);
                        i++;
                    })
                    data.series[data.series.length] =
                    {
                        name: legend,
                        data: funnalData,
                        events: {
                            click: function (event) {
                                clickCallback(event.point.name, AdIds[0])
                        }
                    }
                    }
                } else {
                    data.series[data.series.length] =
                    {
                        name: chart.name,
                        data: numericData
                    }
                }

            });

            var opts = {
                chart: {
                    type: chartType,
                    zoomType: 'x',
                    marginRight: 100,
                    renderTo: 'canvasContainer'
                },
                title: {
                    text: chartText
                },
                xAxis: {
                    categories: data.categories
                },
                yAxis: {
                    title: {
                        text: legend
                    }
                },
                series: data.series
            }
            HighChartHelper.chart = new Highcharts.Chart(opts);
        }
    });
}

function loadStatistics(date, list) {
    var statDetails = $('#statDetails');
    $.ajax({
        url: statDetails.data('url'),
        type: 'POST',
        context: statDetails,
        data: {
            Date: date,
            AdIds: list
        },
        success: function (result) {
            statDetails.html(result);
        }
    });
}

HighChartHelper.init = function (chartType, chartText, legend, url, clickCallback) {
    $("#dispose-graph").click(HighChartHelper.dispose);
    HighChartHelper.dropdownTemp = $("#Ads").html();
    $("#addAd").click(function () {
        if (chartType === 'funnel') {
            AdIds = [];
        }
        AdIds[AdIds.length] = $("#Ads option:selected").val();
        var selector = "#Ads option[value='" + AdIds[AdIds.length - 1] + "']";
        if (chartType !== 'funnel') {
            removedDD[AdIds[AdIds.length - 1].toString()] = $(selector).html();
            $(selector).remove();
        }

        AdIds = _.uniq(AdIds, false);
        if ($("#Ads option").size() == 0) {
            $("#addAd").hide();
            $("#Ads").hide();
        }
        HighChartHelper.reload(chartType, chartText, legend, url, clickCallback);
    });
}