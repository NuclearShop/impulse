var ChartJsHelper = function () { }
ChartJsHelper.chartColor = new Array();
ChartJsHelper.removedDD = new Array();
ChartJsHelper.AdIds = new Array();
ChartJsHelper.url = '';


ChartJsHelper.reload = function (token, chartType, url) {
    $("#canvasContainer").html("");
    var container = document.getElementById('canvasContainer');
    var spinner = new Spinner(spinnerOpts).spin();
    $("#canvasContainer").html("<canvas id='graph1' width='800' height='400'></canvas>");
    var ctx = $("#graph1").get(0).getContext("2d");


    container.appendChild(spinner.el);
    var label = new Array();

    var clickCount = new Array();
    var minificate = $('#minificate').is(':checked');
    var startDate = $('#start-date-stats').val();
    var endDate = $('#end-date-stats').val();
    $.ajax({
        url: url,
        headers: {
            "Authorization": "Bearer " + token
        },
        data: {
            AdIds: ChartJsHelper.AdIds,
            isMinificate: minificate,
            sDate: startDate,
            eDate: endDate
        },
        type: 'POST',
        success: function (incoming) {
            spinner.stop();
            var data = {
                labels: incoming.charts[0].labels,
                datasets: []
            };

            var strR = "";
            $("#statLegend").html("");
            var i = 0;
            _.each(incoming.charts, function (chart) {
                if (ChartJsHelper.chartColor[i] == null) {
                    ChartJsHelper.chartColor[ChartJsHelper.chartColor.length] = "rgba(" + _.random(0, 255) + "," + _.random(0, 255) + "," + _.random(0, 255);
                }
                data.datasets[data.datasets.length] =
                    {
                        label: "charts",
                        fillColor: ChartJsHelper.chartColor[i] + ",0.2)",
                        strokeColor: ChartJsHelper.chartColor[i] + ",1)",
                        pointColor: ChartJsHelper.chartColor[i] + ",1)",
                        pointStrokeColor: "#fff",
                        pointHighlightFill: "#fff",
                        pointHighlightStroke: ChartJsHelper.chartColor[i] + ",1)",
                        data: chart.data
                    }
                $("#statLegend").append(
                        "<div class='row' id='" + chart.name.toString().substr(8) + "'>" + chart.name + "<button class='dltBtn btn btn-warning btn-xs pull-right' id='dltBtn" + i + "' data-val='" + i + "'><i class='fa fa-times'></i></button></div>"
                    );

                $("#dltBtn" + i).click(function () {
                    id = $(this).data("val");
                    ChartJsHelper.chartColor = _.without(ChartJsHelper.chartColor, ChartJsHelper.chartColor[id].toString());
                    $("#Ads").append("<option value='" + ChartJsHelper.AdIds[id] + "'>" + ChartJsHelper.removedDD[ChartJsHelper.AdIds[id].toString()] + "</option>");
                    ChartJsHelper.AdIds = _.without(ChartJsHelper.AdIds, ChartJsHelper.AdIds[id].toString());
                    $("#Ads").show();
                    $("#addAd").show();
                    ChartJsHelper.reload(chartType, ChartJsHelper.url);
                });
                if ($('.dltBtn').length === 1) {
                    $('.dltBtn').hide();

                    $('#minificated').show();
                }
                else {
                    $('.dltBtn').show();
                    $('#minificated').hide();
                    $('#minificate').prop("checked", false);
                }
                $("#" + chart.name.toString().substr(8)).css({
                    "border-radius": "5px",
                    "background-color": ChartJsHelper.chartColor[i] + ",0.5)"
                })
                i++;
            });
            var ch;
            if (chartType === "Line") {
                ch = new Chart(ctx).Line(data, {
                    bezierCurve: true,

                });
            } else if (chartType === "Radar") {
                ch = new Chart(ctx).Radar(data);
            }
            else if (chartType === "Bar") {
                ch = new Chart(ctx).Bar(data);
            }
            $("#graph1").click(function (evt) {
                var activePoints = ch.getPointsAtEvent(evt);
                var date = activePoints[0].label;
                loadStatistics(date, ChartJsHelper.AdIds);
            });
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

ChartJsHelper.init = function (token, chartType, url) {
    ChartJsHelper.url = url;
    $("#addAd").click(function () {
        ChartJsHelper.AdIds[ChartJsHelper.AdIds.length] = $("#Ads option:selected").val();
        var selector = "#Ads option[value='" + ChartJsHelper.AdIds[ChartJsHelper.AdIds.length - 1] + "']";
        ChartJsHelper.removedDD[ChartJsHelper.AdIds[ChartJsHelper.AdIds.length - 1].toString()] = $(selector).html();
        $(selector).remove();
        ChartJsHelper.AdIds = _.uniq(ChartJsHelper.AdIds, false);
        if ($("#Ads option").size() == 0) {
            $("#addAd").hide();
            $("#Ads").hide();
        }
        ChartJsHelper.reload(token, chartType, url);
    });

    $("#minificate").click(function () {
        ChartJsHelper.reload(chartType, url);
    });
}
