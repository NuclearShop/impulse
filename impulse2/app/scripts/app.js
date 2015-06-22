'use strict';

/**
 * @ngdoc overview
 * @name impulseApp
 * @description
 * # impulseApp
 *
 * Main module of the application.
 */



var app = angular.module('impulseApp', [
    'ngAnimate',
    'ngAria',
    'ngCookies',
    'ngMessages',
    'ngResource',
    'ngRoute',
    'ngSanitize',
    'ngTouch',
    'ui.bootstrap',
    'ngFileUpload',
    'underscore',
    'LocalStorageModule',
    'ui.select',
    'color.picker'
  ]);
app.constant('Constants', {
  rootPath: 'http://localhost:56596',
});
app.factory('ProjectFactory', function($http, Constants) {
  var projectLoaded = false;
  var project=null;
  var selectedNodeID=null;

  function init(id){
    if(id!==undefined && id!=='') {
      $http.get(Constants.rootPath+'/api/ad/'+id).success(function(data) {
                  project = data;
                  projectLoaded = true;
              });
  } else {
    $http.get('structure2.json').success(function(data) {
                  project = data;
                  projectLoaded = true;
                });
  }
  }
  function setSelectedNode(nodeid){
    selectedNodeID=nodeid;
    console.log('Selected node установлен -'+nodeid +' - '+ selectedNodeID);
  }
  function updateStructure(nodes){
    //project.AdStates = [];
    //project.AdStates = nodes;
  }
  function saveSettings(settings) {
    project.Poster=settings.Poster;
    project.Name=settings.Name;
  }
  function getSelectedNode(){
    var selectedNode=null;
    for(var i in project.AdStates){
      console.log(project.AdStates[i].VideoUnitId+' '+selectedNodeID);
      if(selectedNodeID===project.AdStates[i].VideoUnitId){
        selectedNode = project.AdStates[i];
      }
    }
    return selectedNode;
  }
  function save(data){
    $http.post(Constants.rootPath+'/api/video', data).success(function(data) {
      console.log('Отправлено успешно'+data);
            });
  }
  function getProject(){
    if(project!==null){
      
      return project;
    }
  }
  function isProjectLoaded(){
    return projectLoaded;
  }
  function saveAd(update) {
  	$http.post(Constants.rootPath+'/api/ad/create', project).success(function(data) {
      console.log('Отправлено успешно'+data);
            });
  }
  return {
    variable: 'This is public',
    init:init,
    getProject:getProject,
    isProjectLoaded:isProjectLoaded,
    save:save,
    getSelectedNode:getSelectedNode,
    setSelectedNode:setSelectedNode,
    updateStructure:updateStructure, 
    saveSettings:saveSettings,
    saveAd: saveAd
  };
});

app.run(function(ProjectFactory, $location, $http){
  var id = $location.search()['id'];
  var token = $location.search()['token'];
    $http.defaults.headers.common.Authorization = 'Bearer ' + token;
    ProjectFactory.init(id);
    console.log('Инициализация проекта. Загрузка JSON');
});
app.config(function ($routeProvider) {
    $routeProvider
      .when('/', {
        templateUrl: 'views/main.html',
        controller: 'MainCtrl'
      })
      .when('/slide', {
        templateUrl: 'views/slide-editor.html',
        controller: 'SlideEditCtrl'
      })
      .otherwise({
        redirectTo: '/'

      });
  });
app.filter('trusted', ['$sce', function ($sce) {
    return function(url) {
        return $sce.trustAsResourceUrl(url);
    };
}]);

app.factory('authInterceptorService', ['$q', '$location', 'localStorageService', 'Constants',  function ($q, $location, localStorageService,Constants) {
    var authInterceptorServiceFactory = {};


    var _responseError = function (rejection) {
        if (rejection.status === 401) {
            $location.path(Constants.rootPath+ '/#/login');
        }
        return $q.reject(rejection);
    };
    authInterceptorServiceFactory.responseError = _responseError;

    return authInterceptorServiceFactory;
}]);