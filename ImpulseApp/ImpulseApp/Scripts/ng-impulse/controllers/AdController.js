///<reference path="~/Scripts/ng-impulse/mplsServices.js" />
///<reference path="~/Scripts/ng-impulse/mplsApp.js" />
///<reference path="~/Scripts/ng-impulse/mpls.intellisense.js"/>

ImpulseApp.controller('AdController', function ($scope, $routeParams, SpinnerService, ServerQueryService, $modal, $filter) {

    SpinnerService.AssignSpinner($scope, 'page-wrapper');
    var container = document.getElementById('page-wrapper');
    $scope.adspinner = new Spinner(spinnerOpts).spin();
    container.appendChild($scope.adspinner.el);
    $scope.ads = []
    $scope.filterAds = [];
    ServerQueryService.getAds()
        .then(function (ads) {
            /// <param name="ads" type="Array" elementType="server.SimpleAdModelDTO">Description</param>
            $scope.adspinner.stop();
            $scope.ads = $filter('shortUrlFilter')(ads, 2);
        },
        function (data) {
            $scope.adspinner.stop();
            console.log('AdController getAds error');
        });
    
});
