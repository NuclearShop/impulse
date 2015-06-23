'use strict';

app.controller('SettingsCtrl', ['$scope', 'Upload', 'Constants', '$modalInstance', '$http', 'ProjectFactory', function ($scope, Upload, Constants, $modalInstance, $http, ProjectFactory) {
    $scope.$watch('files', function () {
        $scope.upload($scope.files);
    });
    $scope.isPosterLoaded=false;
    $scope.posterEnabled=false;
    $scope.projectName='';
    $scope.posterSrc='';
    $scope.States=[];
    $scope.FirstState='none';
    $scope.closeModal = function() {
            $modalInstance.dismiss();
    };
    $scope.saveAndClose = function(){
        var settings = [];
        settings.Name = $scope.projectName;
        settings.FirstState=$scope.FirstState.id;
        if($scope.posterEnabled&&$scope.isPosterLoaded){
        settings.Poster = $scope.posterSrc;

        }else{
            settings.Poster='none';
        }
        console.log(settings);
        ProjectFactory.saveSettings(settings);
        $modalInstance.close(settings);
    };
    
    $scope.upload = function (files) {
        if (files && files.length) {
            for (var i = 0; i < files.length; i++) {
                var file = files[i];
                Upload.upload({
                    url: Constants.rootPath+'/api/upload/pic',
                    file: file
                }).progress(onProgressLoad)
                .success(onSuccessVideoLoad);
            }
        }

    };
    function init(){
        var project= ProjectFactory.getProject();
        for(var i in project.AdStates){
            var state={
                id:project.AdStates[i].VideoUnitId,
                name:project.AdStates[i].Name
            };
            if(project.FirstState===project.AdStates[i].VideoUnitId){
                $scope.FirstState={
                 id:project.AdStates[i].VideoUnitId,
                name:project.AdStates[i].Name
                };
            }
            $scope.States.push(state);
        }
        $scope.projectName=project.Name;
        if(project.Poster==='none'){
            $scope.posterEnabled=false;
            $scope.isPosterLoaded=false;
        }else{
            $scope.posterEnabled=true;
            $scope.isPosterLoaded=true;
            $scope.posterSrc=project.Poster;
        }
    }
    function onProgressLoad(evt){
        var progressPercentage = parseInt(100.0 * evt.loaded / evt.total);
        console.log('progress: ' + progressPercentage + '% ' + evt.config.file.name);
    }
    function onSuccessVideoLoad(data, status, headers, config){
         
         $scope.isPosterLoaded=true; 
         $scope.posterSrc=Constants.rootPath+data;   
    }

    init();
}]);