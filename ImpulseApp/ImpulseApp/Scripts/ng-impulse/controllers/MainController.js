ImpulseApp.controller('MainController', ['$scope', '$location', 'authService', 'SpinnerService', function ($scope, $location, authService, SpinnerService) {
    authService.fillAuthData();
    $scope.userName = authService.authentication.userName;
    $scope.isAuth = authService.authentication.isAuth;
    $scope.token = authService.authentication.token;
}]);