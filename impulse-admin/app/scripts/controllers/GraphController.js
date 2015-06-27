ImpulseApp.controller('GraphController', function ($scope, $routeParams, SpinnerService, ServerQueryService, authService) {

    SpinnerService.AssignSpinner($scope, 'page-wrapper');
    var url = '';
    $scope.token = authService.authentication.token;
    $scope.width = 10;
    $scope.showAds = true;
    switch ($routeParams.type) {
        case 'click': url = '/Statistic/ClickStatistics';
            $scope.title = 'Статистика по кликам';
            $scope.type = 'line';
            HighChartHelper.init($scope.token, $scope.type, 'График', 'Количество кликов', url);
            break;
        case 'browser': url = '/Statistic/BrowserStatistics';
            $scope.title = 'Статистика по браузерам';
            $scope.type = 'Radar';
            ChartJsHelper.init($scope.token, $scope.type, url);
            break;
        case 'funnel': url = '/Statistic/FunnelStatistics';
            $scope.width = 6;
            $scope.title = 'Воронка переходов';
            $scope.type = 'funnel';
            HighChartHelper.init($scope.token, $scope.type, 'График', 'Переходы', url, function (name, id) {
                ServerQueryService.getAdById(id).then(function (ad) {
                    $scope.state = _.findWhere(ad.AdStates, { Name: name });

                },
                function (data) {
                    console.log('AdController getAds error');
                });
            });
            break;
        case 'locale': url = '/Statistic/LocaleStatistics';
            $scope.showAds = false;
            $scope.title = 'Лояльность';
            $scope.type = 'Map';
            HighChartHelper.init($scope.token, $scope.type, 'График', 'Лояльность', url);
            break;
    }

    $scope.$on('$routeChangeStart', function () {
        HighChartHelper.dispose();
    });

    $scope.$watch('type', function () {
        //переписать
        //HighChartHelper.reload($scope.type, 'График', 'Количество кликов', url);
    })
});