var chartColor = new Array();
var removedDD = new Array();
var AdIds = new Array();

function reload(chartType) {
    $("#canvasContainer").html("");
    $("#canvasContainer").html("<canvas id='graph1' width='800' height='400'></canvas>");
    var ctx = $("#graph1").get(0).getContext("2d");



    var label = new Array();

    var clickCount = new Array();
    var minificate = $('#minificate').is(':checked');
    $.ajax({
        url: $("#canvasContainer").data('url'),
        data: {
            AdIds: AdIds,
            isMinificate: minificate
        },
        type: 'POST',
        success: function (incoming) {
            var data = {
                labels: incoming.charts[0].labels,
                datasets: []
            };

            var strR = "";
            $("#statLegend").html("");
            var i = 0;
            _.each(incoming.charts, function (chart) {
                if (chartColor[i] == null) {
                    chartColor[chartColor.length] = "rgba(" + _.random(0, 255) + "," + _.random(0, 255) + "," + _.random(0, 255);
                }
                data.datasets[data.datasets.length] =
                    {
                        label: "charts",
                        fillColor: chartColor[i] + ",0.2)",
                        strokeColor: chartColor[i] + ",1)",
                        pointColor: chartColor[i] + ",1)",
                        pointStrokeColor: "#fff",
                        pointHighlightFill: "#fff",
                        pointHighlightStroke: chartColor[i] + ",1)",
                        data: chart.data
                    }
                $("#statLegend").append(
                        "<div class='row' id='" + chart.name.toString().substr(8) + "'>" + chart.name + "<button class='dltBtn btn btn-warning btn-xs pull-right' id='dltBtn" + i + "' data-val='" + i + "'><i class='fa fa-times'></i></button></div>"
                    );

                $("#dltBtn" + i).click(function () {
                    id = $(this).data("val");
                    chartColor = _.without(chartColor, chartColor[id].toString());
                    $("#Ads").append("<option value='" + AdIds[id] + "'>" + removedDD[AdIds[id].toString()] + "</option>");
                    AdIds = _.without(AdIds, AdIds[id].toString());
                    $("#Ads").show();
                    $("#addAd").show();
                    reload(chartType);
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
                    "background-color": chartColor[i] + ",0.5)"
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
                loadStatistics(date, AdIds);
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

var init = function (chartType) {
    $("#addAd").click(function () {
        AdIds[AdIds.length] = $("#Ads option:selected").val();
        var selector = "#Ads option[value='" + AdIds[AdIds.length - 1] + "']";
        removedDD[AdIds[AdIds.length - 1].toString()] = $(selector).html();
        $(selector).remove();
        AdIds = _.uniq(AdIds, false);
        if ($("#Ads option").size() == 0) {
            $("#addAd").hide();
            $("#Ads").hide();
        }
        reload(chartType);
    });

    $("#minificate").click(function () {
        reload(chartType);
    });
}
