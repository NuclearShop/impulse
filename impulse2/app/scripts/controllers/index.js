'use strict';

app.controller('IndexCtrl', ['$scope', 'Upload', 'Constants', '$http', 'ProjectFactory','$modal', function ($scope, Upload, Constants, $http, ProjectFactory, $modal) {

	var project;
	var WAITINTERVAL = 400;
	var waitintervalid;

$scope.presentationName='Презентация';
  $scope.saveAd = function(update) {
      ProjectFactory.saveAd(update);
  };
  $scope.openPreviewModal=function(size){
    var modalInstance = $modal.open({
      animation: $scope.animationsEnabled,
      templateUrl: 'views/preview.html',
      controller: 'PreviewCtrl',
      size: size,
      resolve: {
        items: function () {
          return $scope.items;
        }
      }
    });

  modalInstance.result.then(function (selectedItem) {
    }, function () {
      $log.info('Modal dismissed at: ' + new Date());
    });
  };

$scope.openSettingsModal=function(size){
    var modalInstance = $modal.open({
      animation: $scope.animationsEnabled,
      templateUrl: 'views/settings.html',
      controller: 'SettingsCtrl',
      size: size,
      resolve: {
        items: function () {
          return $scope.items;
        }
      }
    });

  modalInstance.result.then(function (selectedItem) {
            //setPoster(selectedItem.Poster);
            $scope.presentationName = selectedItem.Name;
            project.FirstState=selectedItem.FirstState;
            project.Name=selectedItem.Name;
    }, function () {
      $log.info('Modal dismissed at: ' + new Date());
    });
  };
  function waitingLoadProject(){
      if (!ProjectFactory.isProjectLoaded()){
      
      
      } else {
      	project=ProjectFactory.getProject();
  		console.log(project);
	  	$scope.presentationName=project.Name;
          clearInterval(waitintervalid);
          $scope.$apply();
      }
    }
  function init(){
  	 waitintervalid = setInterval(waitingLoadProject, WAITINTERVAL);
  	  }
  init();
}]);