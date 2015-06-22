'use strict';

app.controller('PreviewCtrl', ['$scope', 'Upload', 'Constants', '$modalInstance', '$http', 'ProjectFactory', function ($scope, Upload, Constants, $modalInstance, $http, ProjectFactory) {
   
    $scope.closeModal = function() {
            $modalInstance.dismiss();
    };
    $scope.saveAndClose = function(){
       /* var settings = [];
        settings.Name = $scope.projectName;
        if($scope.posterEnabled&&$scope.isPosterLoaded){
        settings.Poster = $scope.posterSrc;

        }else{
            settings.Poster='none';
        }
        ProjectFactory.saveSettings(settings);
        $modalInstance.close(settings);*/
    };
    
    
    function init(){
        /*var project= ProjectFactory.getProject();
        $scope.projectName=project.Name;
        if(project.Poster==='none'){
            $scope.posterEnabled=false;
            $scope.isPosterLoaded=false;
        }else{
            $scope.posterEnabled=true;
            $scope.isPosterLoaded=true;
            $scope.posterSrc=project.Poster;
        }*/
    }
   
    init();
}]);