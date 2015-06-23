'use strict';

app.controller('PreviewCtrl', ['$scope', 'Upload', 'Constants', '$modalInstance', '$http', 'ProjectFactory', function ($scope, Upload, Constants, $modalInstance, $http, ProjectFactory) {
   
    $scope.closeModal = function() {
            $modalInstance.dismiss();
    };
    $scope.load = function(){
        getVideoInfo(ProjectFactory.getProject(), Constants.rootPath)
    }
    //
}]);