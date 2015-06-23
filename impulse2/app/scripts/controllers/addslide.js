'use strict';

app.controller('AddSlideCtrl', ['$scope', 'Upload', 'Constants', '$modalInstance', '$http', 'ProjectFactory', function ($scope, Upload, Constants, $modalInstance, $http, ProjectFactory) {
    $scope.$watch('files', function () {
        $scope.upload($scope.files);
    });
    $scope.isVideoLoaded=false;
    $scope.video =undefined;
    $scope.duration='--:--';
    $scope.startSec=0;
    $scope.startMSec=0;
    $scope.endSec=10;
    $scope.endMSec=60;
    $scope.cropLeft=0;
    $scope.cropWidth=100;
    $scope.isVideoPlayed=false;
    $scope.curBarPosition=0;
    $scope.videoId = undefined;
    $scope.VideoUnit = {};

    $scope.closeModal = function(){
            $modalInstance.dismiss();
    };
    $scope.saveAndClose = function(){
        var vid = [];
        vid.name = $scope.name;
        vid.id = $scope.videoId;
        $http.get(Constants.rootPath+'/api/video/'+vid.id).success(function(data){
            vid.v = data;
            var proj = ProjectFactory.getProject();

            $modalInstance.close(vid);
        });
    };
    $scope.playVideo=function(){
        if($scope.isVideoPlayed){
            $scope.video.pause();
        }else{
            $scope.video.play();
        }
        $scope.isVideoPlayed=!$scope.isVideoPlayed;
    };
    $scope.incStartSeconds=function(){
            if($scope.startSec>0){
                $scope.startSec-=1;
                redrawCropPosition();
            }

    };
    $scope.incEndSeconds=function(){

            var startcroptime = $scope.startSec+$scope.startMSec/100;
            var endcroptime = $scope.endSec-1+$scope.endMSec/100;
            
            if(isIntervalValid(startcroptime,endcroptime)){
                $scope.endSec-=1;
                redrawCropPosition();
            }
    };
    $scope.descStartSeconds=function(){
            if($scope.startSec>=59){
                $scope.startSec=0;
                return;
            }

            var startcroptime = $scope.startSec+1+$scope.startMSec/100;
            var endcroptime = $scope.endSec+$scope.endMSec/100;
            
            if(isIntervalValid(startcroptime,endcroptime)){
                $scope.startSec++;
                redrawCropPosition();
            }
            
        
    };
    $scope.descEndSeconds=function(){
            if($scope.endSec>=59){
                $scope.endSec=0;
                return;
            }
            var startcroptime = $scope.startSec+$scope.startMSec/100;
            var endcroptime = $scope.endSec+1+$scope.endMSec/100;
            
            if(isIntervalValid(startcroptime,endcroptime)){
                $scope.endSec++;
                redrawCropPosition();
            }
            
    };
    $scope.incStartMSeconds=function(){
             if($scope.startMSec<10){
                $scope.startMSec=0;
                return;
            }
            $scope.startMSec-=10;

            redrawCropPosition();

    };
    $scope.incEndMSeconds=function(){
            if($scope.endMSec<10){
                $scope.endMSec=0;
                return;
            }
            var startcroptime = $scope.startSec+$scope.startMSec/100;
            var endcroptime = $scope.endSec+($scope.endMSec-10)/100;
            
            if(isIntervalValid(startcroptime,endcroptime)){
                $scope.endMSec-=10;
                redrawCropPosition();
            }
    };
    $scope.descStartMSeconds=function(){
            if($scope.startMSec>=90){
                $scope.startMSec=90;
                return;
            }
            var startcroptime = $scope.startSec+($scope.startMSec+10)/100;
            var endcroptime = $scope.endSec+$scope.endMSec/100;
            
            if(isIntervalValid(startcroptime,endcroptime)){
                $scope.startMSec+=10;
                redrawCropPosition();
            }
        
    };
    $scope.descEndMSeconds=function(){
            if($scope.endMSec>=90){
                $scope.endMSec=90;
                return;
            }
            var startcroptime = $scope.startSec+$scope.startMSec/100;
            var endcroptime = $scope.endSec+($scope.endMSec+10)/100;
            
            if(isIntervalValid(startcroptime,endcroptime)){
                $scope.endMSec+=10;
                redrawCropPosition();
            }
    };
    function redrawCropPosition(){
        var durationBarWidth=538;
        var startcroptime =$scope.startSec+$scope.startMSec/100;
        var endcroptime = $scope.endSec+ $scope.endMSec/100;

        var sposition=startcroptime*durationBarWidth/$scope.duration;
        var eposition=endcroptime*durationBarWidth/$scope.duration-sposition;
        console.log('duration '+ $scope.duration+' startscopetime '+startcroptime+' endscopetime '+endcroptime);
        $scope.cropLeft=sposition;
        $scope.cropWidth=eposition;

    }
    function isIntervalValid(startcroptime,endcroptime){
        if(endcroptime-startcroptime<=2){
            return false;
        }
        if($scope.duration<endcroptime){
            return false;
        }
        return true;

    }
    $scope.upload = function (files) {
        if (files && files.length) {
            for (var i = 0; i < files.length; i++) {
                var file = files[i];
                Upload.upload({
                    url: Constants.rootPath+'/api/upload/video',
                    file: file
                }).progress(onProgressLoad)
                .success(onSuccessVideoLoad);
            }
        }

    };
    function onProgressLoad(evt){
        var progressPercentage = parseInt(100.0 * evt.loaded / evt.total);
        console.log('progress: ' + progressPercentage + '% ' + evt.config.file.name);
    }
    function onSuccessVideoLoad(data, status, headers, config){
         
        $scope.video =document.getElementById('addSlideVideo');
        $scope.video.load();
        $scope.video.addEventListener('loadedmetadata', function() {
        $scope.duration = parseFloat($scope.video.duration);
        $scope.isVideoLoaded=true;
        $scope.endSec=parseInt($scope.duration);
        $scope.endMSec=parseInt(($scope.duration-$scope.endSec)*10)*10;
         redrawCropPosition();

        
        $scope.$apply();
        console.log('file ' + config.file.name + 'uploaded. Response: ' + data+'Duration'+$scope.video.duration.toString()); 
        });
         $scope.video.addEventListener('timeupdate', function() {
                var durationBarWidth=538;
                var curbarposition=$scope.video.currentTime*durationBarWidth/$scope.duration;
                $scope.curBarPosition = curbarposition;
                console.log($scope.video.currentTime);
                /*
                var thecanvas = document.createElement('canvas');
                var context = thecanvas.getContext('2d');
                context.drawImage($scope.video, 0, 0, $scope.video.videoWidth, $scope.video.videoHeight);
                var dataURL = thecanvas.toDataURL('image/png');
                dataURL = dataURL.replace('data:image/png;base64,', '');*/
                
                $scope.$apply();
        });
         
         var video = [];
        var realVid = document.getElementById('addSlideVideo');
        realVid.load();
        var dataURL = '';
        var sended = false;
                        window.setTimeout(realVid.onloadeddata = function () {
                            video = Popcorn("#addSlideVideo");
                            dataURL = video.capture({
                                at: realVid.duration > 4?4:Math.floor(realVid.duration / 2)
                            }).media.poster;
                            var image = dataURL;
                            data.Length = realVid.duration;
                            data.Name = $("#name").val();
                            data.Image = image.replace('data:image/png;base64,', '');
                            $scope.VideoUnit = data;
                            if (dataURL !== '' && !sended) {
                                sended = true;
                                $http.post(Constants.rootPath+'/api/upload/video/complete', data).success(
                                    function (data) {
                                        $scope.videoId = data;
                                        $scope.VideoUnit.Id = data;
                                        console.log("id: " + data);
                                    }
                                )
                                /*$.ajax({
                                    url: Constants.rootPath+'/api/upload/video/complete',
                                    type: 'POST',
                                    data: data,
                                    success: function (data) {
                                        $scope.videoId = data;
                                        console.log("id: " + data);
                                    }
                                });*/
                                realVid.onloadeddata = [];
                            } 
                        }, 1000);
         

    }
    
}]);