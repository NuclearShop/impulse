///<reference path="~/Scripts/ng-impulse/mplsServices.js" />
///<reference path="~/Scripts/ng-impulse/mplsApp.js" />
///<reference path="~/Scripts/ng-impulse/mpls.intellisense.js"/>

ImpulseApp.controller('AbTestController', function ($scope, $routeParams, SpinnerService, ServerQueryService, $modal, $filter, ctrlOptions) {

    SpinnerService.AssignSpinner($scope, 'page-wrapper');
    var container = document.getElementById('page-wrapper');
    $scope.adspinner = new Spinner(spinnerOpts).spin();
    container.appendChild($scope.adspinner.el);
    $scope.today = new Date();
    var getAdsDefinition = function () {
        ServerQueryService.getAds()
        .then(function (ads) {
            /// <param name="ads" type="Array" elementType="SimpleAdModelDTO">Description</param>
            $scope.adspinner.stop();
            $scope.ads = $filter('shortUrlFilter')(ads, 2);
            if ($scope.adKey1 === '' || !$scope.adKey1) {
                $scope.adKey1 = $scope.ads[0].key;
                $scope.versionId1 = $scope.ads[0].versions[0].Id;
            }
            if ($scope.adKey2 === '' || !$scope.adKey2) {
                $scope.adKey2 = $scope.ads[0].key;
                $scope.versionId2 = $scope.ads[0].versions[0].Id;
            }
        },
        function (data) {
            $scope.adspinner.stop();
            console.log('AdController getAds error');
        });
    }

    if (ctrlOptions && ctrlOptions.getAllAb) {
        $scope.abList = [];
        ServerQueryService.getAllTests()
        .then(function (abList) {
            /// <param name="ab" type="ABTest">Description</param>
            $scope.abList = abList;
            $scope.adspinner.stop();
        },
        function (data) {
            $scope.adspinner.stop();
            console.log('AdController getAds error');
        });
    }

    if ($routeParams.id) {
        ServerQueryService.getAbTestById($routeParams.id)
        .then(function (ab) {
            /// <param name="ab" type="ABTest">Description</param>
            getAdsDefinition();
            $scope.adspinner.stop();
            $scope.abTest = ab;
            $scope.adKey1 = ab.AdA.ShortUrlKey;
            $scope.adKey2 = ab.AdB.ShortUrlKey;
            //$scope.abTest.Url = _.random(10,100) + "" + _.random(10,100) + ""+_.random(10,100)+""+_.random(10,100);
        },
        function (data) {
            $scope.adspinner.stop();
            console.log('AdController getAds error');
        });
    } else {
        $scope.ads = undefined;
        $scope.adKey1 = '';
        $scope.adKey2 = '';
        $scope.version1 = [];
        $scope.version2 = [];
        $scope.abTest = new ABTest();
        $scope.abTest.ActiveAd = '1';
        $scope.abTest.ChangeCount = 1;
        $scope.abTest.ChangeHours = 1;
        $scope.abTest.DateStart = new Date();
        getAdsDefinition();
    }

    $scope.submit = function () {
        ServerQueryService.postAbTest($scope.abTest)
        .then(function (ads) {
            /// <param name="ads" type="Array" elementType="SimpleAdModelDTO">Description</param>
            $scope.adspinner.stop();
            location.href = "/#/ab/all"
        },
        function (data) {
            $scope.adspinner.stop();
            console.log('AdController getAds error');
        });
    }

    $scope.$watch('adKey1', function (val) {
        if ($scope.ads !== undefined) {
            $scope.ad1 = _.findWhere($scope.ads, { key: val });
            $scope.abTest.AdAId = $scope.ad1.versions[0].Id;
            $scope.version1 = _.findWhere($scope.ad1.versions, { Id: $scope.abTest.AdAId });
            $scope.abTest.Url = $scope.adKey1 + "" + $scope.adKey2 + "" + $scope.abTest.AdAId + "" + $scope.abTest.AdBId;
        }

    });
    $scope.$watch('adKey2', function (val) {
        if ($scope.ads !== undefined) {
            $scope.ad2 = _.findWhere($scope.ads, { key: val })
            $scope.abTest.AdBId = $scope.ad2.versions[0].Id;
            $scope.version2 = _.findWhere($scope.ad2.versions, { Id: $scope.abTest.AdBId });
            $scope.abTest.Url = $scope.adKey1 + "" + $scope.adKey2 + "" + $scope.abTest.AdAId + "" + $scope.abTest.AdBId;
        }
    });
    $scope.$watchGroup(['abTest.AdAId', 'abTest.AdBId'], function () {
        $scope.abform.ada.$setValidity("same", $scope.abTest.AdAId !== $scope.abTest.AdBId);
        $scope.abform.adb.$setValidity("same", $scope.abTest.AdAId !== $scope.abTest.AdBId);
        $scope.abTest.Url = $scope.adKey1 + "" + $scope.adKey2 + "" + $scope.abTest.AdAId + "" + $scope.abTest.AdBId;
    })
    $scope.$watchGroup([
        'abTest.ChangeHours',
        'abTest.ChangeCount',
        'abTest.DateStart'], function (newValues, oldValues, scope) {
            var days = Math.floor($scope.abTest.ChangeHours * $scope.abTest.ChangeCount * 2 / 24);
            var hours = ($scope.abTest.ChangeHours * $scope.abTest.ChangeCount * 2) % 24;
            var dateEndDays = new Date($scope.abTest.DateStart);
            dateEndDays.setHours(0);
            dateEndDays.setMinutes(0);
            dateEndDays.setSeconds(0);
            dateEndDays.setMilliseconds(0);
            dateEndDays.setDate(dateEndDays.getDate() + days);
            dateEndDays.setHours(dateEndDays.getHours() + hours);
            $scope.abTest.DateEnd = dateEndDays;
            $scope.dateEnd = dateEndDays.toLocaleString() === 'Invalid Date' ? 'Невозможно рассчитать дату' : dateEndDays.toLocaleString();
        });



});

ImpulseApp.controller('AbTestListController', function ($scope, $routeParams, SpinnerService, ServerQueryService, $modal, $filter, ctrlOptions) {

    SpinnerService.AssignSpinner($scope, 'page-wrapper');
    var container = document.getElementById('page-wrapper');
    $scope.adspinner = new Spinner(spinnerOpts).spin();
    container.appendChild($scope.adspinner.el);
    $scope.today = new Date();
    $scope.abList = [];
    ServerQueryService.getAllTests()
    .then(function (abList) {
        /// <param name="ab" type="ABTest">Description</param>
        $scope.abList = abList;
        _.forEach($scope.abList, function (ab) {
            ab.StartDateString = new Date(ab.DateStart).toLocaleDateString();
            ab.EndDateString = new Date(ab.DateEnd).toLocaleDateString();
            var full = Math.abs(new Date(ab.DateEnd) - new Date(ab.DateStart));
            var nowDate = new Date();
            var now = Math.abs(nowDate - new Date(ab.DateStart));
            if (nowDate < new Date(ab.DateStart))
                ab.progressValue = 0;
            else if (nowDate > new Date(ab.DateEnd))
                ab.progressValue = 100;
            else
                ab.progressValue = ((now / full) * 100).toFixed(0);
        })
        $scope.adspinner.stop();
    },
    function (data) {
        $scope.adspinner.stop();
        console.log('AdController getAds error');
    });
});

ImpulseApp.controller('AbTestInfoController', function ($scope, $routeParams, SpinnerService, ServerQueryService, $modal, $filter) {

    SpinnerService.AssignSpinner($scope, 'page-wrapper');
    var container = document.getElementById('page-wrapper');
    $scope.adspinner = new Spinner(spinnerOpts).spin();
    container.appendChild($scope.adspinner.el);
    $scope.today = new Date();
    $scope.abReview = [];
    var id = $routeParams.id;
    ServerQueryService.getAbReviewById(id)
    .then(function (ab) {
        $scope.adspinner.stop();
        /// <param name="ab" type="ABTest">Description</param>
        ab.StartDateString = new Date(ab.AbTest.DateStart).toLocaleDateString();
        ab.EndDateString = new Date(ab.AbTest.DateEnd).toLocaleDateString();
        var full = Math.abs(new Date(ab.AbTest.DateEnd) - new Date(ab.AbTest.DateStart));
        var nowDate = new Date();
        var now = Math.abs(nowDate - new Date(ab.AbTest.DateStart));
        if (nowDate < new Date(ab.AbTest.DateStart))
            ab.progressValue = 0;
        else if (nowDate > new Date(ab.AbTest.DateEnd))
            ab.progressValue = 100;
        else
            ab.progressValue = ((now / full) * 100).toFixed(0);
        $scope.abReview = ab;

        $('#click-container').highcharts({
            chart: {
                plotBackgroundColor: null,
                plotBorderWidth: 0,
                plotShadow: false
            },
            title: {
                text: 'Относительный<br>показатель<br>взаимодействия',
                align: 'center',
                verticalAlign: 'middle',
                y: 50
            },
            tooltip: {
                pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
            },
            plotOptions: {
                pie: {
                    dataLabels: {
                        enabled: true,
                        distance: -50,
                        style: {
                            fontWeight: 'bold',
                            color: 'white',
                            textShadow: '0px 1px 2px black'
                        }
                    },
                    startAngle: -90,
                    endAngle: 90,
                    center: ['50%', '75%']
                }
            },
            series: [{
                type: 'pie',
                name: 'Clicks',
                innerSize: '50%',
                data: [
                    ['Презентация A', $scope.abReview.OverallClicksA],
                    ['Презентация B', $scope.abReview.OverallClicksB]
                ]
            }]
        });

        $('#click-container-all').highcharts({
            chart: {
                type: 'area'
            },
            title: {
                text: 'Показатель кликов по периодам'
            },
            xAxis: {
                categories: _.pluck($scope.abReview.ClickChart, 'Iteration'),
                title: {
                    text: null
                }
            },
            yAxis: {
                min: 0,
                title: {
                    text: 'Количество кликов',
                    align: 'high'
                },
                labels: {
                    overflow: 'justify'
                }
            },
            tooltip: {
                valueSuffix: ' кликов'
            },
            plotOptions: {
                bar: {
                    dataLabels: {
                        enabled: true
                    }
                }
            },
            legend: {
                layout: 'vertical',
                align: 'right',
                verticalAlign: 'top',
                x: -40,
                y: 100,
                floating: true,
                borderWidth: 1,
                backgroundColor: ((Highcharts.theme && Highcharts.theme.legendBackgroundColor) || '#FFFFFF'),
                shadow: true
            },
            credits: {
                enabled: false
            },
            series: [{
                name: 'Реклама A',
                data: _.pluck($scope.abReview.ClickChart, 'AdAClicks')
            }, {
                name: 'Реклама B',
                data: _.pluck($scope.abReview.ClickChart, 'AdBClicks')
            }]
        });

        $('#click-container-absolute').highcharts({
            chart: {
                type: 'bar'
            },
            title: {
                text: 'Абсолютный показатель кликов'
            },
            xAxis: {
                categories: ['Презентация A', 'Презентация B'],
                title: {
                    text: null
                }
            },
            yAxis: {
                min: 0,
                title: {
                    text: 'Количество кликов',
                    align: 'high'
                },
                labels: {
                    overflow: 'justify'
                }
            },
            tooltip: {
                valueSuffix: ' кликов'
            },
            plotOptions: {
                bar: {
                    dataLabels: {
                        enabled: true
                    }
                }
            },
            legend: {
                layout: 'vertical',
                align: 'right',
                verticalAlign: 'top',
                x: -40,
                y: 100,
                floating: true,
                borderWidth: 1,
                backgroundColor: ((Highcharts.theme && Highcharts.theme.legendBackgroundColor) || '#FFFFFF'),
                shadow: true
            },
            credits: {
                enabled: false
            },
            series: [{
                name: 'Клики',
                data: [$scope.abReview.OverallClicksA, $scope.abReview.OverallClicksB]
            }]
        });
        
    },
    function (data) {
        $scope.adspinner.stop();
        console.log('AdController getAds error');
    });
});