var ImpulseApp = angular.module('ImpulseApp', ['ngRoute', 'ui.bootstrap', 'ngDialog', 'pascalprecht.translate', 'LocalStorageModule', 'ui.grid', 'ui.grid.pagination', 'ui.grid.exporter']);

ImpulseApp.run([
  '$templateCache',
  function ($templateCache) {
      $templateCache.put('splash/index.html',
        '<section class="splash" ng-class="{\'splash-open\': animate}" ng-style="{\'z-index\': 1000, display: \'block\'}">' +
        ' <div class="splash-inner" ng-transclude></div>' +
        '</section>'
      );
  }
]);

var configFunction = function ($routeProvider, $translateProvider, $httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
    $routeProvider.
        when('/', {
            templateUrl: 'UserFront/Index',
            controller: 'HomeController'
        })
        .when('/graphics/:type', {
            templateUrl: 'UserFront/StatisticsClicks',
            controller: 'GraphController'
        })
        .when('/report/:type', {
            templateUrl: 'UserFront/StatisticsTableClick',
            controller: 'TableController'
        })
        .when('/create', {
            templateUrl: 'UserFront/CreateTestStub',
            controller: 'HomeController'
        })
        .when('/generate', {
            templateUrl: 'Additional/GenerateAds',
            controller: 'HomeController'
        })
        .when('/generateab', {
            templateUrl: 'Additional/GenerateAbs',
            controller: 'HomeController'
        })
        .when('/media', {
            templateUrl: 'UserFront/AdList',
            controller: 'AdController'
        })
        .when('/ab/get/:id', {
            templateUrl: 'Test/AbInfo',
            controller: 'AbTestInfoController'
        })
        .when('/login', {
            templateUrl: 'Scripts/ng-impulse/html/login.html',
            controller: 'LoginController'
        })
        .when('/register', {
            templateUrl: 'Scripts/ng-impulse/html/register.html',
            controller: 'RegisterController'
        })
        .when('/ab', {
            templateUrl: 'UserFront/AbTest',
            controller: 'AbTestController',
            resolve: {
                ctrlOptions: function () {
                    return {
                        getAllAb: false,
                    };
                }
            }
        })
        .when('/ab/all', {
            templateUrl: 'UserFront/AbTestList',
            controller: 'AbTestListController',
            resolve: {
                ctrlOptions: function () {
                    return {
                        getAllAb: true,
                    };
                }
            }
        })
        .when('/ab/:id', {
            templateUrl: 'UserFront/AbTest',
            controller: 'AbTestController',
            resolve: {
                ctrlOptions: function () {
                    return {
                        getAllAb: false,
                    };
                }
            }
        })
        .when('/admin', {
            templateUrl: 'Scripts/ng-impulse/html/admin-main.html',
            controller: 'AdminController',
        })
        .when('/requests', {
            templateUrl: 'Scripts/ng-impulse/html/user-requests.html',
            controller: 'UserRequestController'
        });
};
configFunction.$inject = ['$routeProvider', '$translateProvider','$httpProvider'];

ImpulseApp.config(configFunction);