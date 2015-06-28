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
            templateUrl: 'views/index.html',
            controller: 'HomeController'
        })
        .when('/graphics/:type', {
            templateUrl: 'views/reports/graph-stats.html',
            controller: 'GraphController'
        })
        .when('/report/:type', {
            templateUrl: 'views/reports/table-stats.html',
            controller: 'TableController'
        })
        .when('/create', {
            templateUrl: 'views/additional/create-stub.html',
            controller: 'HomeController'
        })
        .when('/generate', {
            templateUrl: 'views/additional/generate-stats-ads.html',
            controller: 'HomeController'
        })
        .when('/generateab', {
            templateUrl: 'views/additional/generate-stats-abs.html',
            controller: 'HomeController'
        })
        .when('/media', {
            templateUrl: 'views/ad-list.html',
            controller: 'AdController'
        })
        .when('/ab/get/:id', {
            templateUrl: 'views/ab-info.html',
            controller: 'AbTestInfoController'
        })
        .when('/login', {
            templateUrl: 'views/login.html',
            controller: 'LoginController'
        })
        .when('/register', {
            templateUrl: 'views/register.html',
            controller: 'RegisterController'
        })
        .when('/ab', {
            templateUrl: 'views/ab.html',
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
            templateUrl: 'views/ab-list.html',
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
            templateUrl: 'views/ab.html',
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
            templateUrl: 'views/admin-main.html',
            controller: 'AdminController',
        })
        .when('/requests', {
            templateUrl: 'views/user-requests.html',
            controller: 'UserRequestController'
        });
};
configFunction.$inject = ['$routeProvider', '$translateProvider','$httpProvider'];

ImpulseApp.config(configFunction);