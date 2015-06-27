ImpulseApp.controller('UserRequestController', function ($scope, $http, SpinnerService, ServerQueryService, $filter) {
    SpinnerService.AssignSpinner($scope, 'page-wrapper');

    var container = document.getElementById('page-wrapper');
    $scope.adspinner = new Spinner(spinnerOpts).spin();
    container.appendChild($scope.adspinner.el);
    $scope.userRequests = [];
    ServerQueryService.getAds()
        .then(function (ads) {
            $scope.adspinner.stop();
            $scope.ads = $filter('shortUrlFilter')(ads, 2);
        },
        function (data) {
            $scope.adspinner.stop();
            console.log('AdController getAds error');
        });
    $scope.load = function () {
        var container = document.getElementById('page-wrapper');
        $scope.adspinner = new Spinner(spinnerOpts).spin();
        container.appendChild($scope.adspinner.el);
        ServerQueryService.getUserRequests($scope.selectedAd).then(function (data) {
            $scope.adspinner.stop();
            $scope.userRequests = data;
        })
    }
});