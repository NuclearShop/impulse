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
    $scope.offsetVideoWrapper=0;
    $scope.selectedStyles={
      x:100,
      y:100,
      width:100,
      height:100,
      starttime:1.5,
      endtime:10.5,
      text:''
    };

    $scope.defaultNext = {};
  $scope.defaultNexts = [];
    $scope.actionType = {};
    $scope.actionTypes = [
    {type:'next-slide',name:'Перейти к слайду'},
    {type:'link',name:'Перейти по ссылке'}
    ];

    var canvas;    
    var ctx;
    var WIDTH;
    var HEIGHT;
    var INTERVAL = 20;  // how often, in milliseconds, we check to see if a redraw is needed
    var isDrag = false;
    var mx, my; // mouse coordinates
    var canvasValid = false;
    var ghostcanvas;
    var gctx; // fake canvas context
    var offsetx, offsety;
    var stylePaddingLeft, stylePaddingTop, styleBorderLeft, styleBorderTop;
    var mySelColor = '#fff';
    var mySelWidth = 2;
    var playbackLineWidth = 800;
    var project = null;

    $scope.getNameOfActionType=function(str){
      if(str==='next-slide'){
        return 'Перейти к слайду';
      }
      if(str==='link'){
        return 'Перейти по ссылке';
      }
      return 'Не задан';
    };
    $scope.getNodenameByID=function(id){
    var nodename='Не задан';
    for(var i in project.AdStates){
        
        if(id===project.AdStates[i].VideoUnitId){
          nodename = project.AdStates[i].Name;
        }
      }
      return nodename;
  };

    $scope.getOpacity=function(opacity){
      console.log(opacity);
      var newopacity=parseInt(opacity)/100;
      console.log(newopacity);
      return newopacity;
    };
    $scope.tabCoef=1;
    $scope.tab=function(id){
      $scope.tabCoef=id;
      console.log('click on '+ id);
    };
    $scope.playVideo=function(){
        if($scope.isVideoPlayed){
            $scope.video.pause();
        }else{
            $scope.video.play();
            unselectElement();

        }
        $scope.isVideoPlayed=!$scope.isVideoPlayed;
       $scope.isElementSelected=false;

    };
    $scope.stopvideo=function(){
      $scope.isVideoPlayed=false;
      $scope.video.pause();
    };
    $scope.getLeft=function(id){
        if($scope.video===''||!$scope.userElements[id]||!$scope.video.duration){ return 0;}
        var durationBarWidth=playbackLineWidth;
        var startcroptime=$scope.userElements[id].TimeAppear;
        var endcroptime = $scope.userElements[id].TimeDisappear;

        var sposition=startcroptime*durationBarWidth/$scope.video.duration;
        return sposition;

    };
    $scope.getWidth=function(id){
         if($scope.video===''||!$scope.userElements[id]||!$scope.video.duration){ return playbackLineWidth;}
        var durationBarWidth=playbackLineWidth;
        var startcroptime=$scope.userElements[id].TimeAppear;
        var endcroptime = $scope.userElements[id].TimeDisappear;

        var sposition=startcroptime*durationBarWidth/$scope.video.duration;
        var eposition=endcroptime*durationBarWidth/$scope.video.duration-sposition;
        return eposition;

    };
    $scope.isShow=function(id){
      if($scope.curBarPosition>=$scope.getLeft(id)&&$scope.curBarPosition<=($scope.getWidth(id)+$scope.getLeft(id))){
      return true;
      } else {
        //if($scope.selectedElement&&$scope.selectedElement.tempId===id){unselectElement();}
        return false;
      }
    };
    $scope.changeTimeAppear=function(){
      $scope.video.currentTime=$scope.selectedElement.TimeAppear;
    };

    $scope.changeTimeDisappear=function(){
      if($scope.selectedElement.TimeDisappear>$scope.video.duration) {$scope.selectedElement.TimeDisappear=$scope.video.duration;}
      $scope.video.currentTime=$scope.selectedElement.TimeDisappear;
    };
    
     $scope.elementClick = function(id) {
      $scope.selectedElement=$scope.userElements[id];
      $scope.isElementSelected=true;
      $scope.stopvideo();
      console.log('invalidate elementclick');
      $scope.video.currentTime=$scope.selectedElement.TimeAppear;
      //var elem = _.findWhere($scope.userElements, {tempId: id});
      invalidate();

    };
    $scope.removeElement=function(){
      console.log($scope.userElements);
      $scope.userElements = _.without($scope.userElements, $scope.selectedElement);
      console.log($scope.userElements);
      unselectElement();
    };
    $scope.gotoAndStop=function(event){
      console.log(event.x+' '+event.y+' '+event.offsetX+' '+event.offsetY);
      if($scope.video){
        $scope.stopvideo();
        $scope.curBarPosition=event.offsetX;
      $scope.video.currentTime=$scope.curBarPosition*$scope.video.duration/playbackLineWidth;
    }
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
    	'Width':200,
    	'Height':60,
    	'Action':'next-slide',
    	'TimeAppear':0,
    	'TimeDisappear':5,
    	'CurrentId':'',
    	'NextId':0,
    	'NextTime':0,
    	'FormName':'',
      'Styles':[],
    	'HtmlTags':[],
    	'Id': id,
      'ActionTemp':'',
      'ActionType':'none'
		};
    userelement.Styles={
      'border-radius':0,
      'background-color':'#fff',
      'border-width':0,
      'border-color':'#008',
      'opacity':100,
      'color':'#000',
      'font-size':14,
      'padding-top':20,
      'font-weight':'normal'
    };
    userelement.TimeDisappear=$scope.video.duration;
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
    function clear(c) {
      c.clearRect(0, 0, WIDTH, HEIGHT);
    }

     function invalidate() {
      canvasValid = false;
    }

    // Sets mx,my to the mouse position relative to the canvas
    // unfortunately this can be tricky, we have to worry about padding and borders
    //Установка позиции мыши относительно canvas
    function getMouse(e) {
          var element = canvas, offsetX = 0, offsetY = 0;

          if (element.offsetParent) {
            do {
              offsetX += element.offsetLeft;
              offsetY += element.offsetTop;
            } while ((element = element.offsetParent));
          }

          // Add padding and border style widths to offset
          offsetX += stylePaddingLeft;
          offsetY += stylePaddingTop;

          offsetX += styleBorderLeft;
          offsetY += styleBorderTop;

          mx = e.pageX - offsetX;
          my = e.pageY - offsetY;
    }
    function drawline(x,y,x1,y1){
      ctx.beginPath();
    ctx.moveTo(x,y);
    ctx.lineTo(x1,y1);
    ctx.stroke();
    }

    function drawCanvas(){
      if(canvasValid===false){
        clear(ctx);
        if ($scope.selectedElement !== undefined && $scope.selectedElement !== null) {
          ctx.strokeStyle = mySelColor;
          ctx.lineWidth =0.25;
          ctx.setLineDash([10]);
          drawline($scope.selectedElement.X,0,$scope.selectedElement.X,HEIGHT);
          drawline($scope.selectedElement.X+$scope.selectedElement.Width,0,$scope.selectedElement.X+$scope.selectedElement.Width,HEIGHT);
          drawline(0,$scope.selectedElement.Y,WIDTH,$scope.selectedElement.Y);
          drawline(0,$scope.selectedElement.Y+$scope.selectedElement.Height,WIDTH,$scope.selectedElement.Y+$scope.selectedElement.Height);
          ctx.lineWidth = mySelWidth;
          ctx.setLineDash([0]);
          ctx.strokeRect($scope.selectedElement.X,$scope.selectedElement.Y,$scope.selectedElement.Width,$scope.selectedElement.Height);

        }
        
        canvasValid=true;      
      }
    }
    function drawGhostNode(gctx, elem){
      if (elem.X> WIDTH || elem.Y> HEIGHT) { 
        return;
      } 
      if (elem.X+elem.Width < 0 || elem.Y + elem.Height < 0){
        return;
      } 
      gctx.fillStyle = '#ffffff';
      gctx.fillRect(elem.X,elem.Y,elem.Width,elem.Height);
    }
    function myMove(e){
      if (isDrag){
        getMouse(e);
        
        $scope.selectedElement.X = mx - offsetx;
        $scope.selectedElement.Y = my - offsety;   
        _.defer(function(){$scope.$apply();});
        // something is changing position so we better invalidate the canvas!
        invalidate();
      console.log('invalidate mousemove');

      }
    }

    function myDown(e){
      getMouse(e);
      clear(gctx);
      var l = $scope.userElements.length;
      for (var i = l-1; i >= 0; i--) {
        // draw shape onto ghost context
        if($scope.isShow($scope.userElements[i].tempId)){
          drawGhostNode(gctx, $scope.userElements[i]);
          console.log('ghostNode '+i);
        }
        
        // get image data at the mouse x,y pixel
        var imageData = gctx.getImageData(mx, my, 1, 1);
        //var index = (mx + my * imageData.width) * 4;
        
        // if the mouse pixel exists, select and break
        if (imageData.data[3] > 0) {
          $scope.selectedElement=$scope.userElements[i];
          $scope.isElementSelected=true;
          $scope.stopvideo();
          console.log('Клик по звену '+$scope.userElements[i].tempId);
          _.defer(function(){$scope.$apply();});
          offsetx = mx - $scope.selectedElement.X;
          offsety = my - $scope.selectedElement.Y;
          $scope.selectedElement.X = mx - offsetx;
          $scope.selectedElement.Y = my - offsety;
          isDrag = true;
          canvas.onmousemove = myMove;
          clear(gctx);
          invalidate();
          return;
        }

        
      }

      $scope.isElementSelected=false;
      console.log('Клик по интерактивному слою');
      _.defer(function(){$scope.$apply();});
      // havent returned means we have selected nothing
      $scope.selectedElement = null;
      // clear the ghost canvas for next time
      clear(gctx);
      // invalidate because we might need the selection border to disappear
      invalidate();
      console.log('invalidate md');

    }

    function myUp(){
      isDrag = false;
      canvas.onmousemove = null;
    }
    function unselectElement(){
      isDrag=false;
      canvas.onmousemove=null;
      $scope.tabCoef=1;
      $scope.selectedElement=null;
      $scope.isElementSelected=false;
      invalidate();
    }
    function loadElements(){
     // for(var i in $scope.node.UserElements){
        angular.copy($scope.node.UserElements,$scope.userElements);
        //$scope.userElements.push($scope.node.UserElements[i]);
        //$scope.userElements[i].Styles=JSON.parse($scope.node.UserElements[i].HtmlStyle);
        console.log($scope.userElements);
        parseStyles();
        //$scope.userElements[i].Styles=JSON.parse($scope.node.UserElements[i].HtmlStyle);
        //console.log($scope.userElements[i]);
        //console.log($scope.node.UserElements[i]);
      //}
    }
    function parseStyles(){
      for(var i in $scope.userElements){
        $scope.userElements[i].Width=parseInt($scope.userElements[i].Width);
        
        $scope.userElements[i].Height=parseInt($scope.userElements[i].Height);

        $scope.userElements[i].X=parseInt($scope.userElements[i].X);

        $scope.userElements[i].Y=parseInt($scope.userElements[i].Y);
        var styles = {};
        try {
          styles = JSON.parse($scope.userElements[i].HtmlStyle);
        }
        catch(err){}
        
        if(styles['border-radius']){styles['border-radius']=parseInt(deletePxPrefix(styles['border-radius']));
        }
        if(styles['padding-top']){styles['padding-top']=parseInt(deletePxPrefix(styles['padding-top']));
        }
        if(styles['font-size']){styles['font-size']=parseInt(deletePxPrefix(styles['font-size']));
        }
        if(styles['border-width']){styles['border-width']=parseInt(deletePxPrefix(styles['border-width']));
        }
        if(styles['opacity']){styles['opacity']=parseFloat(styles['opacity'])*100;
        }
        if(styles['box-shadow']){
          var s2=styles['box-shadow'].split('px 0 0 ');
          s2[0]=s2[0].replace('0 ','');
          styles['shadow-width']=parseInt(s2[0]);
          styles['shadow-color']=s2[1];
        }
        /*$scope.userElements[i].Styles.BorderRadius=deletePxPrefix(styles['border-radius']);
        $scope.userElements[i].Styles['padding-top']=deletePxPrefix(styles['padding-top']);
        $scope.userElements[i].Styles['font-size']=deletePxPrefix(styles['font-size']);
        $scope.userElements[i].Styles['opacity']=styles['opacity'];
        $scope.userElements[i].Styles['border-color']=styles['border-color'];
        $scope.userElements[i].Styles['border-width']=styles['border-width'];
        $scope.userElements[i].Styles['background-color']=styles['background-color'];
        $scope.userElements[i].Styles['color']=styles['color'];
        */
        $scope.userElements[i].Styles=styles;
        console.log($scope.userElements[i].Styles);
      }
      console.log('Стили разбиты');
      console.log($scope.userElements);
    }
    function deletePxPrefix(str){
      str=str.replace('px','');
      return str;
    }
    function createInlineStyle(styles1){
      var styles=angular.copy(styles1);
      if(styles['border-radius']){
        styles['border-radius']=styles['border-radius']+'px';
      }
      if(styles['padding-top']){
        styles['padding-top']=styles['padding-top']+'px';
      }
      if(styles['font-size']){
        styles['font-size']=styles['font-size']+'px';
      }
      if(styles['border-width']){
        styles['border-width']=styles['border-width']+'px';
        styles['border-color']=styles['border-color'];
        styles['border-style']='solid';
      }
      if(styles['opacity']){styles['opacity']=parseFloat(styles['opacity'])/100;
        }

      if(styles['shadow-width']){
        styles['box-shadow']='0 '+styles['shadow-width']+'px 0 0 '+styles['shadow-color'];
        delete styles['shadow-width'];
        delete styles['shadow-color'];      
      }
        styles=JSON.stringify(styles);
        console.log(styles);
        return styles;
      /*
      if(styles['border-radius']!==0){css+='border-radius:'+styles['border-radius']+'px;';}
      if(styles['padding-top']!==0){css+='padding-top:'+styles['padding-top']+'px;';}
      if(styles['font-size']!==0){css+='font-size:'+styles['padding-top']+'px;';}
      if(styles['opacity']!==1){css+='opacity:'+parseInt(styles['opacity'])/100+';';}
      if(styles['border-width']!==0){css+='border:'+styles['border-width']+'px solid'+styles['border-color']+';';}
      css+='background-color:'+styles['background-color']+';';
      css+='color:'+styles['color']+';';
      */
    }
    $scope.saveElements = function (){
        for(var i in $scope.userElements){
         var style = createInlineStyle($scope.userElements[i].Styles); 
         console.log(style);
         $scope.userElements[i].HtmlStyle=style;
         $scope.userElements[i].HtmlType='div';
         $scope.userElements[i].HtmlClass='mpls-action-button';
        }
        $scope.node.UserElements =$scope.userElements;
        console.log(project.StateGraph);
        var StateGraph=_.where(project.StateGraph,{V1:$scope.node.VideoUnitId});
         console.log(StateGraph);
         for(var j in StateGraph){
          project.StateGraph=_.without(project.StateGraph,StateGraph[j]);
         }
        console.log(project.StateGraph);

        var nextslideBtn=_.where($scope.userElements,{Action:'next-slide'});
        for(var j in nextslideBtn){
          if(nextslideBtn[j].NextId!==0&&nextslideBtn[j]!==''){
            var newStateNode={
              V1:parseInt($scope.node.VideoUnitId),
              V2:parseInt(nextslideBtn[j].NextId),
              T:0
            };
            project.StateGraph.push(newStateNode);
          }
        }
        console.log(project.StateGraph);

        console.log($scope.node.UserElements);
        console.log($scope.userElements);
      
    };
    $scope.getNameById=function (id){
      if(id>0){
      var node =_.findWhere(project.AdStates, {VideoUnitId: id});
      return node.Name;

      }
      return '';

    };

    function init(){
    
    var timeline= angular.element('elements-wrapper');
    //timeline.mCustomScrolbar();
    var videoWrapper = document.getElementById('video-wrapper');
    $scope.offsetVideoWrapper=videoWrapper.offsetHeight/2-225;
    console.log(videoWrapper.offsetHeight);
    $scope.node = ProjectFactory.getSelectedNode();
    project = ProjectFactory.getProject();


    for(var i in project.AdStates){
      
      if($scope.node.VideoUnitId!==project.AdStates[i].VideoUnitId){
      $scope.defaultNexts.push(project.AdStates[i].VideoUnitId); 
      }
    }
    if($scope.node.DefaultNext!==0){
      $scope.defaultNext.selected={
        name:$scope.getNodenameByID($scope.node.DefaultNext),
        id:$scope.node.DefaultNext
      };
    }
    console.log($scope.defaultNexts);
    console.log($scope.node.DefaultNext);

    if($scope.node.VideoUnit.FullPath.indexOf(Constants.rootPath)<0) {
      $scope.node.VideoUnit.FullPath = Constants.rootPath + $scope.node.VideoUnit.FullPath;
    }	
    $scope.video = document.getElementById('main-video');

    $scope.interactiveLayer=document.getElementById('interactive');

    canvas = document.getElementById('slide-canvas');
    ghostcanvas = document.createElement('canvas');
    playbackLineWidth=document.getElementById('playback-line').offsetWidth;
    canvas.height = $scope.interactiveLayer.offsetHeight;
    canvas.width = $scope.interactiveLayer.offsetWidth;
    HEIGHT = canvas.height;
    WIDTH = canvas.width;
    ctx = canvas.getContext('2d');
    ghostcanvas.height = HEIGHT;
    ghostcanvas.width = WIDTH;
    gctx = ghostcanvas.getContext('2d');

    canvas.onselectstart = (function () {
       return false; 
      }());
      
      // fixes mouse co-ordinate problems when there's a border or padding
      // see getMouse for more detail
      if (document.defaultView && document.defaultView.getComputedStyle) {
        stylePaddingLeft = parseInt(document.defaultView.getComputedStyle(canvas, null).paddingLeft, 10)      || 0;
        stylePaddingTop  = parseInt(document.defaultView.getComputedStyle(canvas, null).paddingTop, 10)       || 0;
        styleBorderLeft  = parseInt(document.defaultView.getComputedStyle(canvas, null).borderLeftWidth, 10)  || 0;
        styleBorderTop   = parseInt(document.defaultView.getComputedStyle(canvas, null).borderTopWidth, 10)   || 0;
      }
      

    canvas.onmousedown = myDown;
    canvas.onmouseup = myUp;

    ctx.fillStyle = '#ffffff';
    ctx.fillRect(100,100,300,200);
    setInterval(drawCanvas, INTERVAL);

    loadElements();
    $scope.video.play();
    $scope.video.pause();
    _.defer(function(){$scope.$apply();});
    $scope.video.addEventListener('loadedmetadata', function() {
                var durationBarWidth=playbackLineWidth;
                $scope.duration = $scope.video.duration;
                var curbarposition=$scope.video.currentTime*durationBarWidth/$scope.duration;
                $scope.curBarPosition = curbarposition;
                /*
                var thecanvas = document.createElement('canvas');
                var context = thecanvas.getContext('2d');
                context.drawImage($scope.video, 0, 0, $scope.video.videoWidth, $scope.video.videoHeight);
                var dataURL = thecanvas.toDataURL('image/png');
                dataURL = dataURL.replace('data:image/png;base64,', '');*/
                
                $scope.$apply();
        }); 
		$scope.video.addEventListener('timeupdate', function() {
                var durationBarWidth=playbackLineWidth;
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
