ImpulseApp.controller('HomeController', function ($scope, SpinnerService) {
    SpinnerService.AssignSpinner($scope, 'page-wrapper');
    $scope.sendxml = function () {
        SenderStub.send();
    }
});