'use strict';

angular.module('impulseApp')
  .controller('SlideEditCtrl', function ($scope, ProjectFactory, _, Constants) {

    $scope.userElements=[];
    $scope.node=null;
    $scope.isVideoPlayed=false;
    $scope.video='';
    $scope.curBarPosition=0;
    $scope.interactiveLayer=null;
    $scope.selectedElement=null;
    $scope.isElementSelected=false;
    $scope.selectedStyles={
      x:100,
      y:100,
      width:100,
      height:100,
      starttime:1.5,
      endtime:10.5,
      text:''
    };
    $scope.tabCoef=0;
    $scope.tab=function(id){
      $scope.tabCoef=id;
    };
    $scope.playVideo=function(){
        if($scope.isVideoPlayed){
            $scope.video.pause();
        }else{
            $scope.video.play();
        }
        $scope.isVideoPlayed=!$scope.isVideoPlayed;
       $scope.isElementSelected=false;

    };
    $scope.getLeft=function(id){
        if($scope.video===''||!$scope.userElements[id]||!$scope.video.duration){ return 0;}
        var durationBarWidth=800;
        var startcroptime=$scope.userElements[id].TimeAppear;
        var endcroptime = $scope.userElements[id].TimeDisappear;

        var sposition=startcroptime*durationBarWidth/$scope.video.duration;
        console.log('duration '+ $scope.duration+' startscopetime '+startcroptime+' endscopetime '+endcroptime);
        return sposition;

    };
    $scope.getWidth=function(id){
         if($scope.video===''||!$scope.userElements[id]||!$scope.video.duration){ return 800;}
        var durationBarWidth=800;
        var startcroptime=$scope.userElements[id].TimeAppear;
        var endcroptime = $scope.userElements[id].TimeDisappear;

        var sposition=startcroptime*durationBarWidth/$scope.video.duration;
        var eposition=endcroptime*durationBarWidth/$scope.video.duration-sposition;
        console.log('duration '+ $scope.duration+' startscopetime '+startcroptime+' endscopetime '+endcroptime);
        return eposition;

    };



    $scope.interactiveClick=function(){
    };
     $scope.elementClick = function(id) {
      $scope.selectedElement=$scope.userElements[id];
      $scope.isElementSelected=true;
      console.log($scope.selectedElement);
      //var elem = _.findWhere($scope.userElements, {tempId: id});

    };
    $scope.addButton=function(){
    	var id = $scope.userElements.length;
	var userelement={
    	'HtmlId':'',
    	'HtmlClass':'',
    	'HtmlType':'',
    	'UseDefaultStyle':true,
    	'HtmlStyle':'',
    	'Text':'Кнопка 1',
    	'X':10,
    	'Y':5,
    	'Width':150,
    	'Height':40,
    	'Action':'next-slide',
    	'TimeAppear':0,
    	'TimeDisappear':5,
    	'CurrentId':'',
    	'NextId':'',
    	'NextTime':0,
    	'FormName':'',
    	'HtmlTags':[],
    	'tempId': id
		};

		//var elemwrap='<div id="element'+id+'" data-id="'+id+'" class="elem-wrap element'+id+'"><div class="element" style="width:800px;left:0px;">'+userelement.Text+'</div></div>';
		//angular.element('.elements-wrapper').append(elemwrap);
		//angular.element('#interactive').append('<div data-id="'+id+'" class="mpls-action-button element'+id+'" >{{userelement.Text}}</div>');
		$scope.userElements.push(userelement);
    console.log('Id созданного элемента'+userelement.tempId + $scope.userElements);
    //var elem =angular.element('.element'+id);
    console.log($scope.userElements.length);
    //elem.on("click", function (e){
      //if($scope.isElementSelected){
        //angular.element('.element').removeClass('selected');
       // $scope.elementClick(elem.attr('data-id'));
      
   //console.log('ID элемента '+ elem.attr('data-id'));
   // });
		console.log(userelement);
    };

   

    /*
    "Id":39,
               "HtmlId":"",
               "HtmlClass":"mpls-action-button",
               "HtmlType":"div",
               "UseDefaultStyle":true,
               "HtmlStyle":"",
               "Text":"Перейти к видео 3 на позицию 3c",
               "X":10,
               "Y":5,
               "Width":"10",
               "Height":"5",
               "Action":"next-slide",
               "TimeAppear":-1,
               "TimeDisappear":-1,
               "CurrentId":68,
               "NextId":66,
               "NextTime":3,
               "FormName":"",
               "HtmlTags":[

               ]
    function redrawCropPosition(){
        var durationBarWidth=538;
        var startcroptime =$scope.startSec+$scope.startMSec/100;
        var endcroptime = $scope.endSec+ $scope.endMSec/100;

        var sposition=startcroptime*durationBarWidth/$scope.duration;
        var eposition=endcroptime*durationBarWidth/$scope.duration-sposition;
        console.log('duration '+ $scope.duration+' startscopetime '+startcroptime+' endscopetime '+endcroptime);
        $scope.cropLeft=sposition;
        $scope.cropWidth=eposition;

    }*/
    function init(){
    
    $scope.node = ProjectFactory.getSelectedNode();
     if($scope.node.VideoUnit.FullPath.indexOf(Constants.rootPath)<0) {
    $scope.node.VideoUnit.FullPath = Constants.rootPath + $scope.node.VideoUnit.FullPath;
}	
    console.log($scope.node);
     $scope.video = document.getElementById('main-video');
     $scope.interactiveLayer=document.getElementById('interactive');
		$scope.video.addEventListener('timeupdate', function() {
                var durationBarWidth=800;
                $scope.duration = $scope.video.duration;
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
    }
    init();
});
