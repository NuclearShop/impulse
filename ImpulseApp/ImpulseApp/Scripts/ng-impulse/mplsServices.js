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
        getAbReviewById: getAbReviewById
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
});