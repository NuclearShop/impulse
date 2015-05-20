var ImpulseApp = angular.module('ImpulseApp', ['ngRoute', 'ui.bootstrap', 'dialogs.main', 'pascalprecht.translate']);

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

var configFunction = function ($routeProvider, $translateProvider) {
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
        });
    /*$translateProvider.translations('en-US', {
        DIALOGS_ERROR: "Error",
        DIALOGS_ERROR_MSG: "An unknown error has occurred.",
        DIALOGS_CLOSE: "Close",
        DIALOGS_PLEASE_WAIT: "Please Wait",
        DIALOGS_PLEASE_WAIT_ELIPS: "Please Wait...",
        DIALOGS_PLEASE_WAIT_MSG: "Waiting on operation to complete.",
        DIALOGS_PERCENT_COMPLETE: "% Complete",
        DIALOGS_NOTIFICATION: "Notification",
        DIALOGS_NOTIFICATION_MSG: "Unknown application notification.",
        DIALOGS_CONFIRMATION: "Confirmation",
        DIALOGS_CONFIRMATION_MSG: "Confirmation required.",
        DIALOGS_OK: "OK",
        DIALOGS_YES: "Да",
        DIALOGS_NO: "Нет"
    });*/
};
configFunction.$inject = ['$routeProvider', '$translateProvider'];

ImpulseApp.config(configFunction);