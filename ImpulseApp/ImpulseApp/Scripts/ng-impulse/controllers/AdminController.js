ImpulseApp.controller('AdminController', function ($scope, $routeParams, SpinnerService, ServerQueryService, $filter) {

    SpinnerService.AssignSpinner($scope, 'page-wrapper');
    var container = document.getElementById('page-wrapper');
    $scope.adspinner = new Spinner(spinnerOpts).spin();
    container.appendChild($scope.adspinner.el);
    $scope.reviews = [];
    $scope.filteredReviews = [];
    $scope.filterAds = [];
    $scope.currentPage = 1;
    $scope.pageSize = 2;
    $scope.totalItems = $scope.filteredReviews.length;
    $scope.pageChanged = function () {
        console.log($scope.currentPage);
    };
    $scope.$watch('showOnlyReviewed', function (val) {
        if (val === true) {
            $scope.filteredReviews = _.filter($scope.reviews, function (review) {
                return review.review.Id === 0;
            });
        } else {
            $scope.filteredReviews = $scope.reviews;
        }
        $scope.currentPage = 0;
        $scope.totalItems = $scope.filteredReviews.length;
        
    })
    ServerQueryService.getAllAds()
        .then(function (reviews) {
            /// <param name="ads" type="Array" elementType="server.SimpleAdModelDTO">Description</param>

            $scope.adspinner.stop();
            $scope.reviews = reviews;
            $scope.filteredReviews = $scope.reviews;
            $scope.totalItems = $scope.reviews.length;

        },
        function (data) {
            $scope.adspinner.stop();
            console.log('AdController getAds error');
        });
});