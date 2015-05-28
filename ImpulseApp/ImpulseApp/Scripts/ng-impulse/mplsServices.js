'use strict';
ImpulseApp.service("SpinnerService", function ($rootScope) {
    this.AssignSpinner = function (scope, container) {
        var container = document.getElementById(container);
        scope.$on('$routeChangeStart', function () {
            $rootScope.spinner = new Spinner(spinnerOpts).spin();
            container.appendChild($rootScope.spinner.el);
        });
        scope.$on('$routeChangeSuccess', function () {
            if ($rootScope.spinner !== undefined) {
                $rootScope.spinner.stop();
            }
        });
    };
});
ImpulseApp.value('ExportValues', {
    dropdown: [
        {
            id: 0,
            value: 'Google Analytics'
        },
        {
            id: 1,
            value: 'Facebook'
        }
    ]
});
ImpulseApp.service("ServerQueryService", function ($http, $q) {
    var service = {
        getAds: getAds,
        getAd: getAd,
        getAdById: getAdById,
        updateActive: updateActive,
        postAbTest: postAbTest,
        getAbTestByUrl: getAbTestByUrl,
        getAllTests: getAllTests,
        getAbTestById: getAbTestById,
        getAbReviewById: getAbReviewById,
        deleteAdByUrl: deleteAdByUrl,
        deleteAdById: deleteAdById,
        deleteAbById: deleteAbById
    }
    return service;

    function getAbReviewById(id) {
        var def = $q.defer();

        $http.get("/api/ab/review/"+id)
            .success(function (data) {
                def.resolve(data);
            })
            .error(function () {
                def.reject("Completely Failed");
            });
        return def.promise;
    }
    function getAllTests() {
        var def = $q.defer();

        $http.get("/api/ab/all")
            .success(function (data) {
                def.resolve(data);
            })
            .error(function () {
                def.reject("Completely Failed");
            });
        return def.promise;
    }

    function getAbTestByUrl(url) {
        var def = $q.defer();

        $http.get("/api/ab", {url: url})
            .success(function (data) {
                def.resolve(data);
            })
            .error(function () {
                def.reject("Completely Failed");
            });
        return def.promise;
    }
    function getAbTestById(id) {
        var def = $q.defer();

        $http.get("/api/ab/"+id)
            .success(function (data) {
                def.resolve(data);
            })
            .error(function () {
                def.reject("Completely Failed");
            });
        return def.promise;
    }
    function postAbTest(abtest) {
        var def = $q.defer();

        $http.post("/api/ab/create", abtest)
            .success(function (data) {
                def.resolve(data);
            })
            .error(function () {
                def.reject("Completely Failed");
            });
        return def.promise;
    }

    function getAds() {
        var def = $q.defer();

        $http.get("/api/ad/all")
            .success(function (data) {
                def.resolve(data);
            })
            .error(function () {
                def.reject("Completely Failed");
            });
        return def.promise;
    }

    function getAd(url) {
        var def = $q.defer();
        $http.get("/api/ad", { url: url })
            .success(function (data) {
                def.resolve(data);
            })
            .error(function () {
                def.reject("Completely Failed");
            });
        return def.promise;
    }

    function getAdById(id) {
        var def = $q.defer();
        $http.get("/api/ad/"+id)
            .success(function (data) {
                def.resolve(data);
            })
            .error(function () {
                def.reject("Completely Failed");
            });
        return def.promise;
    }



    function updateActive(id) {
        var def = $q.defer();

        $http.post("/api/ad/setactive/"+id)
            .success(function (data) {
                def.resolve(data);
            })
            .error(function () {
                def.reject("Completely Failed");
            });
        return def.promise;
    }

    function deleteAdByUrl(url) {
        var def = $q.defer();

        $http.delete("/api/ad/remove/url/"+url)
            .success(function (data) {
                def.resolve(data);
            })
            .error(function () {
                def.reject("Completely Failed");
            });
        return def.promise;
    }

    function deleteAdById(id) {
        var def = $q.defer();

        $http.delete("/api/ad/remove/" + id)
            .success(function (data) {
                def.resolve(data);
            })
            .error(function () {
                def.reject("Completely Failed");
            });
        return def.promise;
    }

    function deleteAbById(id) {
        var def = $q.defer();

        $http.delete("/api/ab/" + id)
            .success(function (data) {
                def.resolve(data);
            })
            .error(function () {
                def.reject("Completely Failed");
            });
        return def.promise;
    }
});


ImpulseApp.factory('authService', ['$http', '$q', 'localStorageService', function ($http, $q, localStorageService) {

    var authServiceFactory = {};

    var _authentication = {
        isAuth: false,
        userName: ""
    };

    var _saveRegistration = function (registration) {

        _logOut();

        return $http.post('api/account/register', registration).then(function (response) {
            return response;
        });

    };

    var _login = function (loginData) {

        var data = {
            grant_type: 'password',
            username: loginData.userName,
            password: loginData.password
        };


        var deferred = $q.defer();

        $http.post('token', $.param(data), { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {

            localStorageService.set('authorizationData', { token: response.access_token, userName: loginData.userName });

            _authentication.isAuth = true;
            _authentication.userName = loginData.userName;

            deferred.resolve(response);

        }).error(function (err, status) {
            _logOut();
            deferred.reject(err);
        });

        return deferred.promise;

    };

    var _logOut = function () {

        localStorageService.remove('authorizationData');

        _authentication.isAuth = false;
        _authentication.userName = "";

    };

    var _fillAuthData = function () {

        var authData = localStorageService.get('authorizationData');
        if (authData) {
            _authentication.isAuth = true;
            _authentication.userName = authData.userName;
            _authentication.token = authData.token;
        }

    }

    authServiceFactory.saveRegistration = _saveRegistration;
    authServiceFactory.login = _login;
    authServiceFactory.logOut = _logOut;
    authServiceFactory.fillAuthData = _fillAuthData;
    authServiceFactory.authentication = _authentication;

    return authServiceFactory;
}]);

ImpulseApp.factory('authInterceptorService', ['$q', '$location', 'localStorageService',  function ($q, $location, localStorageService) {
    var authInterceptorServiceFactory = {};

    var _request = function (config) {

        config.headers = config.headers || {};

        var authData = localStorageService.get('authorizationData');
        if (authData) {
            config.headers.Authorization = 'Bearer ' + authData.token;
        }

        return config;
    }

    var _responseError = function (rejection) {
        if (rejection.status === 401) {
            $location.path('/login');
        }
        return $q.reject(rejection);
    }

    authInterceptorServiceFactory.request = _request;
    authInterceptorServiceFactory.responseError = _responseError;

    return authInterceptorServiceFactory;
}]);