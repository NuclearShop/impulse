var chartColor = new Array();
var removedDD = new Array();
var AdIds = new Array();

function reload() {
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
                        "<div id='" + chart.name + "'>" + chart.name + "<button class='dltBtn' id='dltBtn" + i + "' data-val='" + i + "'>Удалить</button></div>"
                    );

                $("#dltBtn" + i).click(function () {
                    id = $(this).data("val");
                    chartColor = _.without(chartColor, chartColor[id].toString());
                    $("#Ads").append("<option value='" + AdIds[id] + "'>" + removedDD[AdIds[id].toString()] + "</option>");
                    AdIds = _.without(AdIds, AdIds[id].toString())
                    reload(AdIds);
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
                $("#" + chart.name).css({
                    "border-style": "solid",
                    "border-color": chartColor[i] + ",1)"
                })
                i++;
            });
            var ch = new Chart(ctx).Line(data, {
                bezierCurve: true,

            });
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
$(document).ready(function () {

    $("#addAd").click(function () {
        AdIds[AdIds.length] = $("#Ads option:selected").val();
        var selector = "#Ads option[value='" + AdIds[AdIds.length - 1] + "']";
        removedDD[AdIds[AdIds.length - 1].toString()] = $(selector).html();
        $(selector).remove();
        AdIds = _.uniq(AdIds, false);
        reload();
    });

    $("#minificate").click(function () {
        reload();
    });
})