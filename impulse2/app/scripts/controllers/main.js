'use strict';

/**
 * @ngdoc function
 * @name impulseApp.controller:MainCtrl
 * @description
 * # MainCtrl
 * Controller of the impulseApp
 */
 app.controller('MainCtrl', function ($scope, $modal, $log, ProjectFactory, Constants, _) {

    $scope.nodes=[];
    $scope.nodeSelected=false;
    console.log($scope.nodeSelected);
    $scope.nodeSettingTemplate='nodesettings.html';
    $scope.presentationName = 'Презентация 1';
    $scope.mySel = {
      name: ''

    };
    $scope.showInteractiveLines=true;
    $scope.showDefaultLines=true;
    $scope.userConnect=[];
    
  $scope.country = {};
  $scope.countries = [ // Taken from https://gist.github.com/unceus/6501985
    {name: 'Afghanistan', code: 'AF'},
    {name: 'Åland Islands', code: 'AX'},
   
  ];
  $scope.openAddSlideModal = function (size) {

    var modalInstance = $modal.open({
      animation: $scope.animationsEnabled,
      templateUrl: 'views/addnode.html',
      controller: 'AddSlideCtrl',
      size: size,
      resolve: {
        items: function () {
          return $scope.items;
        }
      }
    });

  modalInstance.result.then(function (selectedItem) {
            var node = new Node();
            node.videoUnitId=selectedItem.id;
            node.defaultNext=0;
            node.x = 100;
            node.y = 150;
            node.name = selectedItem.name===undefined?'name':selectedItem.name;
            //node.Id = project.AdStates[n].Id;

            var img=new Image();
            img.src = Constants.rootPath +selectedItem.v.Image;
            img.onload=onLoadImage;

            node.videoDuration=selectedItem.v.Length;
            node.capture = img;
            $scope.nodes.push(node);
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
            setPoster(selectedItem.Poster);
            $scope.presentationName = selectedItem.Name;
    }, function () {
      $log.info('Modal dismissed at: ' + new Date());
    });
  };
  $scope.updateStructure= function(){
   // var data=[];
    var states = ProjectFactory.getProject().AdStates;
    for(var i in states){
      var node = _.findWhere($scope.nodes, {Id: states[i].Id});
      if(node!==undefined){
        states[i].X = node.x;
        states[i].Y = node.y;
        states[i].Name = node.name;
        states[i].DefaultNext = node.defaultNext;
        states[i].VideoUnitId = node.videoUnitId;
      }
      
    }
    ProjectFactory.updateStructure();
  };
  $scope.invalidate=function(){
    invalidate();
  };
    /*"Id":43,
         "EndTime":5,
         "IsFullPlay":false,
         "Name":"StartState",
         "ChainedHtml":null,
         "IsStart":true,
         "IsEnd":false,
         "VideoUnitId":68,
         "X":150,
         "Y":150,
         "DefaultNext":0,
         "DefaultNextTime":0,
         "VideoUnit":{
            "Id":68,
            "DateLoaded":"2015-05-16T20:51:59.49",
            "Name":"birds",
            "GeneratedName":"465899830",
            "UserName":"admin",
            "Length":0,
            "FullPath":"/Videos/admin/465899830/videoplayback.webm",
            "MimeType":"video/webm",
            "Image":"../../images/thumbnail.jpg"
         },
    ProjectFactory.save(data);
    console.log($scope.nodes);
  };*/
  $scope.getNodenameByID=function(id){
    var nodename='Не задан';
    for(var i in $scope.nodes){
        
        if(id===$scope.nodes[i].videoUnitId){
          nodename = $scope.nodes[i].name;
        }
      }
      return nodename;
  };
  $scope.editNode=function(){
    console.log('Откртыть редактор');
    $scope.updateStructure();
    ProjectFactory.setSelectedNode($scope.mySel.videoUnitId);
  };

  $scope.deleteNode=function(){
    $scope.nodes = _.without($scope.nodes, $scope.mySel);
    var states = ProjectFactory.getProject().AdStates;
    states = _.without(states, _.findWhere(states, {Id: $scope.mySel.Id}));
    $scope.mySel='';
    invalidate();
    ProjectFactory.getProject().AdStates = states;
    $scope.updateStructure();
  };


    //$scope.mySel;
    var canvas,canvasWrapper;    
    var ctx;
    var WIDTH;
    var HEIGHT;
    var INTERVAL = 20;  // how often, in milliseconds, we check to see if a redraw is needed
    var WAITINTERVAL = 400;
    var isDrag = false;
    var isCanvasDrag=false;
    var mx, my; // mouse coordinates
    var waitintervalid;
    var project;
    var posterImg;
    var bgImg;
     // when set to true, the canvas will redraw everything
     // invalidate() just sets this to false right now
     // we want to call invalidate() whenever we make a change
    var canvasValid = false;

    // The node (if any) being selected.
    // If in the future we want to select multiple objects, this will get turned into an array
    //var mySel; 

    // The selection color and width. Right now we have a red selection with a small width
    var mySelColor = '#6173b2';
    var mySelWidth = 2;
    var nodeWidth = 112;
    var nodeHeight = 70;
    // we use a fake canvas to draw individual shapes for selection testing
    var ghostcanvas;
    var gctx; // fake canvas context
    var waitCoef=0;
    // since we can drag from anywhere in a node
    // instead of just its x/y corner, we need to save
    // the offset of the mouse when we start dragging.
    var offsetx, offsety;
    var ctxOffsetx=10, ctxOffsety=-10;
    // Padding and border style widths for mouse offsets
    var stylePaddingLeft, stylePaddingTop, styleBorderLeft, styleBorderTop;


      


     
      
    // initialize our canvas, add a ghost canvas, set draw loop
    // then add everything we want to intially exist on the canvas
   function Node() {
      this.x = 0;
      this.y = 0;
      this.w=nodeWidth;
      this.h=nodeHeight;
      this.defaultNext = -1;
      this.videoUnitId = -1;
      this.name = 'Слайд';
      this.capture = '';
      this.videoDuration=0;
    }
    
    function getNodeByVideoId(id){
      var node;
      for(var i in $scope.nodes){
        
        if(id===$scope.nodes[i].videoUnitId){
          node = $scope.nodes[i];
        }
      }
      return node;
    }
    //Clear canvas
    function clear(c) {
      c.clearRect(0, 0, WIDTH, HEIGHT);
    }
    function drawBg(){
    ctx.drawImage(bgImg, 16, 16);
    var pattern = ctx.createPattern(bgImg, 'repeat');
    ctx.fillStyle = pattern; 
    ctx.rect(0, 0, WIDTH, HEIGHT);
    ctx.fill();
    }
    //метод для рисования звена
    function drawNode(node) {
      // We can skip the drawing of elements that have moved off the screen:
      if (node.x+ctxOffsetx > WIDTH || node.y+ctxOffsety > HEIGHT) { 
        return;
      } 
      if (node.x+ctxOffsetx + nodeWidth < 0 || node.y+ctxOffsety + nodeHeight < 0){
        return;
      } 
      //var capture = new Image();
      //capture.src = node.capture;
      ctx.fillStyle = '#ffffff';
      ctx.setLineDash([0]);
      ctx.fillRect(node.x+ctxOffsetx,node.y+ctxOffsety,nodeWidth,nodeHeight);
      ctx.strokeStyle = '#e6e6e6';
      ctx.lineWidth = 1;
      ctx.strokeRect(node.x+ctxOffsetx,node.y+ctxOffsety,nodeWidth,nodeHeight);
      ctx.drawImage(node.capture, node.x+ctxOffsetx+8, node.y+ctxOffsety+8, 96,54);
      ctx.font = 'bold 9pt Open Sans';
      ctx.fillStyle = 'black';
      ctx.fillText(node.name, node.x+ctxOffsetx + 8, node.y+ctxOffsety-5);
    }
    function setPoster(src){
      if(src&&src!=='none'){
        posterImg.src=src;
      }else{
        posterImg.src='../images/poster.jpg';
      }
    }
    function drawPoster(){

      ctx.drawImage(posterImg, 15+ctxOffsetx,HEIGHT/2-25+ctxOffsety,80,50);
      ctx.font = 'bold 9pt Open Sans';
      ctx.fillStyle = 'black';
      ctx.fillText('Начало', 30+ctxOffsetx, HEIGHT/2+40+ctxOffsety);
    }
    function drawGhostNode(gcontext, node){
      if (node.x+ctxOffsetx > WIDTH || node.y+ctxOffsety > HEIGHT) { 
        return;
      } 
      if (node.x+ctxOffsetx + nodeWidth < 0 || node.y+ctxOffsety + nodeHeight < 0){
        return;
      } 
      gcontext.fillStyle = '#ffffff';
      gcontext.fillRect(node.x+ctxOffsetx,node.y+ctxOffsety,nodeWidth,nodeHeight);
    }
    //Метод для рисования линии
    function drawline(x,y,x1,y1){
      ctx.beginPath();
    ctx.moveTo(x,y);
    ctx.lineTo(x1,y1);
    ctx.stroke();
    }
    //Метод для соеденения звеньев
    function connectNodes(node1,node2,type){
      var startpointx=node1.x+ctxOffsetx+node1.w;
      var startpointy=node1.y+ctxOffsety+node1.h/2;
      var endpointx=node2.x+ctxOffsetx;
      var endpointy=node2.y+ctxOffsety+node2.h/2;
      var coef=1;

      if(type==='default'){
      ctx.setLineDash([5]);
      ctx.strokeStyle = '#acacac';
      ctx.lineWidth = 1;
      }else{
      ctx.setLineDash([0]);
      ctx.strokeStyle = '#3498db';
      ctx.lineWidth = 1;
      drawline(endpointx-5,endpointy-2.5,endpointx,endpointy);
      drawline(endpointx,endpointy,endpointx-5,endpointy+2.5);
    }
      if(startpointx<endpointx){
        drawline(startpointx,startpointy,startpointx+(endpointx-startpointx)/2,startpointy);
        drawline(startpointx+(endpointx-startpointx)/2,startpointy,startpointx+(endpointx-startpointx)/2,endpointy);
        drawline(startpointx+(endpointx-startpointx)/2,endpointy,endpointx,endpointy);
        return;
      }
      if(startpointy>endpointy) {coef=-1;}
      else {coef=1;}
      
      if(Math.abs(startpointy-endpointy)>node1.h+20){
        drawline(startpointx,startpointy,startpointx+10,startpointy);
        drawline(startpointx+10,startpointy,startpointx+10,Math.abs(startpointy-endpointy)/2*coef+startpointy);
        drawline(startpointx+10,Math.abs(startpointy-endpointy)/2*coef+startpointy,endpointx-10,Math.abs(startpointy-endpointy)/2*coef+startpointy);
        drawline(endpointx-10,Math.abs(startpointy-endpointy)/2*coef+startpointy,endpointx-10,endpointy);
        drawline(endpointx-10,endpointy,endpointx,endpointy);
        return;
      }else{

        drawline(startpointx,startpointy,startpointx+10,startpointy);
        drawline(startpointx+10,startpointy,startpointx+10,endpointy+coef*(nodeHeight/2+10));
        drawline(startpointx+10,endpointy+coef*(nodeHeight/2+10),endpointx-10,endpointy+coef*(nodeHeight/2+10));
        drawline(endpointx-10,endpointy+coef*(nodeHeight/2+10),endpointx-10,endpointy);
        drawline(endpointx-10,endpointy,endpointx,endpointy);
        return;
      }
      drawline(startpointx,startpointy,endpointx,endpointy);

      
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
    
    /*
    //Initialize a new Box, add it, and invalidate the canvas
    function addRect(x, y, capture, nodename) {
      //var nn = 'Слайд ' + $scope.boxes.length; 
      var img= new Image();
      img.src = capture;
      var rect = new Node();
      rect.x = parseInt(x);
      rect.y = parseInt(y);
      rect.nodename = nodename;
      rect.capture = img;
      $scope.boxes.push(rect);
      invalidate();
    }*/
    //Ожидание загрузки проекта
    function waitingLoadProject(){
      if (!ProjectFactory.isProjectLoaded()){
      ctx.font = 'normal 20pt Open Sans';
      ctx.fillStyle = 'black';
      switch(waitCoef) {
        case 0: 
        clear(ctx);
        ctx.fillText('Идет загрузка проекта.', WIDTH/2-140, HEIGHT/2-10);
        waitCoef++;
        break;
        case 1:
        clear(ctx);
        ctx.fillText('Идет загрузка проекта..',WIDTH/2-140, HEIGHT/2-10);
        waitCoef++;
        break;
        case 2:
        clear(ctx);
        ctx.fillText('Идет загрузка проекта...',WIDTH/2-140, HEIGHT/2-10);
        waitCoef=0;
        break;
      }
      
      } else {
          project = ProjectFactory.getProject();
  
          posterImg=new Image();
          posterImg.onload=onLoadImage;
          setPoster(project.Poster);

          for(var n in project.AdStates){
            var node = new Node();
            node.videoUnitId=project.AdStates[n].VideoUnitId;
            node.defaultNext=project.AdStates[n].DefaultNext;
            node.x = project.AdStates[n].X;
            node.y = project.AdStates[n].Y;
            node.name = project.AdStates[n].Name;
            node.Id = project.AdStates[n].Id;

            var img=new Image();
            img.src = Constants.rootPath +project.AdStates[n].VideoUnit.Image;
            img.onload=onLoadImage;

            node.videoDuration=project.AdStates[n].VideoUnit.Length;
            node.capture = img;
            $scope.nodes.push(node);
          }
          for(var i in project.StateGraph){
            $scope.userConnect.push(project.StateGraph[i]);
          }
          $scope.presentationName = project.Name;
          console.log('Проект загружен, звенья переданы в scope');
          clearInterval(waitintervalid);
          setInterval(drawStructure, INTERVAL);
      }
    }
    function onLoadImage(){
      invalidate();
    }
    //Перерисовка структуры
    function drawStructure() {
      
      if (canvasValid === false) {
        clear(ctx);
        drawBg();
        // Add stuff you want drawn in the background all the time here
        drawPoster();

        // draw all boxes
        var l = $scope.nodes.length;
        for (var i = 0; i < l; i++) {
            drawNode($scope.nodes[i]);
            var defaultNext = getNodeByVideoId($scope.nodes[i].defaultNext);
            if(defaultNext!==undefined&&defaultNext!==null&&$scope.showDefaultLines){
            connectNodes($scope.nodes[i],defaultNext,'default');
            }
            /*if($scope.nodes[i+1]!==null&& $scope.nodes[i+1]!==undefined){
            connectNodes($scope.nodes[i],$scope.nodes[i+1],'user');
            }*/
        }
        if($scope.showInteractiveLines){
        for (var c in $scope.userConnect){
          var node1=getNodeByVideoId(parseInt($scope.userConnect[c].V1));
          var node2=getNodeByVideoId(parseInt($scope.userConnect[c].V2));

          if(node1&&node2){
            connectNodes(node1,node2,'user');
          }
        }
        }
        if($scope.nodes&&project.FirstState!==0){
          var poster=new Node();
          poster.x=15;
          poster.y=HEIGHT/2-25;
          poster.w=80;
          poster.h=50;
          var firstnode=getNodeByVideoId(parseInt(project.FirstState));
          if(poster&&firstnode){
            connectNodes(poster,firstnode,'default');
          }


        }
        //connectNodes($scope.nodes[0],$scope.nodes[2],'default');
        // draw selection
        // right now this is just a stroke along the edge of the selected box
        if ($scope.mySel !== undefined && $scope.mySel !== null) {
          ctx.strokeStyle = mySelColor;
          ctx.lineWidth = mySelWidth;
          ctx.setLineDash([0]);
          ctx.strokeRect($scope.mySel.x+ctxOffsetx,$scope.mySel.y+ctxOffsety,nodeWidth,nodeHeight);
          
        }
        
        // Add stuff you want drawn on top all the time here
        
        
        canvasValid = true;
      }
    }


    //wipes the canvas context


    // While draw is called as often as the INTERVAL variable demands,
    // It only ever does something if the canvas gets invalidated by our code


    // Draws a single shape to a single context
    // draw() will call this with the normal canvas
    // myDown will call this with the ghost canvas

    // Happens when the mouse is moving inside the canvas
    function myMove(e){
      if (isDrag){
        getMouse(e);
        
        $scope.mySel.x = mx - offsetx;
        $scope.mySel.y = my - offsety;   
        
        // something is changing position so we better invalidate the canvas!
        invalidate();
      }
      if(isCanvasDrag){
        getMouse(e);
        ctxOffsetx=mx-offsetx;
        ctxOffsety=my-offsety;
        invalidate();
      }
    }

    // Happens when the mouse is clicked in the canvas
    function myDown(e){
      getMouse(e);
      clear(gctx);
      var l = $scope.nodes.length;
      for (var i = l-1; i >= 0; i--) {
        // draw shape onto ghost context
        drawGhostNode(gctx, $scope.nodes[i]);
        
        // get image data at the mouse x,y pixel
        var imageData = gctx.getImageData(mx, my, 1, 1);
        //var index = (mx + my * imageData.width) * 4;
        
        // if the mouse pixel exists, select and break
        if (imageData.data[3] > 0) {
          $scope.mySel = $scope.nodes[i];
          $scope.nodeSelected=true;
          console.log('Клик по звену '+$scope.mySel.name.toString());
          $scope.$apply();
          offsetx = mx - $scope.mySel.x;
          offsety = my - $scope.mySel.y;
          $scope.mySel.x = mx - offsetx;
          $scope.mySel.y = my - offsety;
          isDrag = true;
          canvas.onmousemove = myMove;
          invalidate();
          clear(gctx);
          return;
        }

        
      }

      $scope.nodeSelected=false;
      console.log('Клик по рабочей области');
      $scope.$apply();
      // havent returned means we have selected nothing
      $scope.mySel = null;
      offsetx=mx-ctxOffsetx;
      offsety=my-ctxOffsety;
      ctxOffsetx=mx-offsetx;
      ctxOffsety=my-offsety;
      isCanvasDrag=true;
      canvas.onmousemove=myMove;

      // clear the ghost canvas for next time
      clear(gctx);
      // invalidate because we might need the selection border to disappear
      invalidate();
    }

    function myUp(){
      isDrag = false;
      isCanvasDrag=false;
      canvas.onmousemove = null;
    }

    // adds a new node
    function myDblClick(e) {
      getMouse(e);
      // for this method width and height determine the starting X and Y, too.
      // so I left them as vars in case someone wanted to make them args for something and copy this code
     // var width = 112;
      //var height = 70;
      //addRect(mx - (width / 2), my - (height / 2), width, height, '#ffffff');
    }
    function resizecanvas(){
      canvas.height = canvasWrapper.offsetHeight;
      canvas.width = canvasWrapper.offsetWidth;
      HEIGHT = canvas.height;
      WIDTH = canvas.width;
      ctx = canvas.getContext('2d');
      ghostcanvas.height = HEIGHT;
      ghostcanvas.width = WIDTH;
      gctx = ghostcanvas.getContext('2d');
      invalidate();
    }

    function init() {
      canvas = document.getElementById('canvas');
      canvasWrapper = document.getElementById('editor-canvas');
      ghostcanvas = document.createElement('canvas');
      
      window.onresize =resizecanvas;
      resizecanvas();
      //fixes a problem where double clicking causes text to get selected on the canvas
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
      
      // make draw() fire every INTERVAL milliseconds
      bgImg = new Image();
      bgImg.src='../images/tiny_grid.png';
      waitintervalid = setInterval(waitingLoadProject, WAITINTERVAL);
      // set our events. Up and down are for dragging,
      // double click is for making new boxes
      canvas.onmousedown = myDown;
      canvas.onmouseup = myUp;
      canvas.ondblclick = myDblClick;
      
      // add custom initialization here:
        //ProjectFactory.getAnswers();
      
      }

    // If you dont want to use <body onLoad='init()'>
    // You could uncomment this init() reference and place the script reference inside the body tag
        init();
  });
