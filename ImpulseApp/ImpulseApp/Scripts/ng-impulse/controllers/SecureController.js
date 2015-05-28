'use strict';
ImpulseApp.controller('LoginPartialController', ['$scope', '$location', 'authService', 'SpinnerService', function ($scope, $location, authService, SpinnerService) {
    $scope.logout = function () {
        authService.logOut();
    }
    authService.fillAuthData();
    $scope.userName = authService.authentication.userName;
    $scope.isAuth = authService.authentication.isAuth;

}]);
ImpulseApp.controller('LoginController', ['$scope', '$location', 'authService', 'SpinnerService', function ($scope, $location, authService, SpinnerService) {
    SpinnerService.AssignSpinner($scope, 'page-wrapper');
    $scope.loginData = {
        userName: "",
        password: ""
    };

    $scope.message = "";

    $scope.login = function () {

        authService.login($scope.loginData).then(function (response) {

            $location.path('/');

        },
         function (err) {
             $scope.message = err.error_description;
         });
    };

}]);

ImpulseApp.controller('RegisterController', ['$scope', '$location', '$timeout', 'authService', 'SpinnerService', function ($scope, $location, $timeout, authService, SpinnerService) {
    SpinnerService.AssignSpinner($scope, 'page-wrapper');
    $scope.savedSuccessfully = false;
    $scope.message = "";

    $scope.registration = {
        userName: "",
        password: "",
        confirmPassword: "",
        email:""
    };

    $scope.signUp = function () {

        authService.saveRegistration($scope.registration).then(function (response) {

            $scope.savedSuccessfully = true;
            $scope.message = "Вы были успешно зарегистрированы";
            startTimer();

        },
         function (response) {
             var errors = [];
             for (var key in response.data.modelState) {
                 for (var i = 0; i < response.data.modelState[key].length; i++) {
                     errors.push(response.data.modelState[key][i]);
                 }
             }
             $scope.message = "Невозможно зарегистрироваться из-за:" + errors.join(' ');
         });
    };

    var startTimer = function () {
        var timer = $timeout(function () {
            $timeout.cancel(timer);
            $location.path('/#/login');
        }, 2000);
    }

}]);