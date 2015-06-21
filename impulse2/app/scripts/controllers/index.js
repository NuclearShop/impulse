'use strict';

app.controller('IndexCtrl', ['$scope', 'Upload', 'Constants', '$http', 'ProjectFactory', function ($scope, Upload, Constants, $http, ProjectFactory) {
      $scope.saveAd = function(update) {
      ProjectFactory.saveAd(update);
  }
}]);