ImpulseApp.controller('HomeController', function ($scope, $http, SpinnerService) {
    SpinnerService.AssignSpinner($scope, 'page-wrapper');
    $scope.sendxml = function () {
        var model = SenderStub.send();
        $http.post('/api/ad/update', model).success(function () {
            location.href = "/#/";
        })
    }
});