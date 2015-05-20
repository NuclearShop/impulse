/// <reference path="../mpls.intellisense.js" />
ImpulseApp.directive('adInfoblock', function () {
    return {
        templateUrl: 'Scripts/ng-impulse/html/adInfoBlock.html',
        restrict: 'A',
        controller: function ($scope, $modal, ServerQueryService, ExportValues) {
            $scope.watch = function (url) {
                isClose = false;
                $scope.$modalInstance = $modal.open({
                    backdrop: false,
                    scope: $scope,
                    templateUrl: '/ad/' + url,
                    windowTemplateUrl: 'splash/index.html'
                });

            };
            $scope.closeModal = function () {
                $scope.$modalInstance.dismiss('cancel');
            };
            $scope.isShowExport = false;
            $scope.exportValues = ExportValues.dropdown;
            $scope.exportChoosen = $scope.exportValues[0];
            $scope.ad.isopen = false;
            $scope.isopenexport = false;
            $scope.ad.choosen = _.findWhere($scope.ad.versions, { IsActive: true });
            $scope.image = $scope.ad.choosen.AdStates[0].VideoUnit.Image;
            $scope.switchChoosen = function (id) {
                _.each($scope.ad.versions, function (version) {
                    if (version.Id === id) {
                        version.IsActive = true;
                    } else {
                        version.IsActive = false;
                    }

                })
                $scope.ad.choosen = _.findWhere($scope.ad.versions, { IsActive: true });
                $scope.image = $scope.ad.choosen.AdStates[0].VideoUnit.Image;
                $scope.saveButton = "fa-save";

            };
            $scope.exportAd = function () {
                $scope.exportString = '';
                $scope.isShowExport = !$scope.isShowExport;
                switch ($scope.exportChoosen.id) {
                    case 0: $scope.exportString = '<iframe src="http://localhost:56596/ad/'+$scope.ad.choosen.ShortUrlKey+'"></iframe>';
                        break;
                    case 1: $scope.exportString = '<iframe src="http://localhost:56596/ad/' + $scope.ad.choosen.ShortUrlKey + '"></iframe>';
                        break;
                }
            }
            $scope.switchExportChosen = function (id) {
                _.each($scope.exportValues, function (exportVal) {
                    if (exportVal.id === id) {
                        $scope.exportChoosen = exportVal;
                    }
                })
            };

            $scope.saveButton = "fa-save";
            $scope.saveChoosen = function () {
                $scope.saveButton = "fa-clock-o";
                ServerQueryService.updateActive($scope.ad.choosen.Id).then(function (data) {
                    $scope.saveButton = "fa-check";
                });
            }
            $scope.remove = function () {
                console.log('confirmed');
            }
            $scope.toggleDropdown = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                $scope.status.isopen = !$scope.status.isopen;
            };
        }
    }
});

ImpulseApp.filter('shortUrlFilter', function () {
    return function (items, groupCount) {
        /// <param name="items" type="Array" elementType="server.SimpleAdModelDTO">Description</param>
        var results = [];
        if (items) {
            results = _.chain(items)
                .groupBy('ShortUrlKey')
            .map(function (value, key) {
                return {
                    key: key,
                    versions: value
                }
            }).value();

            return results;
        } else {
            return [];
        }
    }
});

ImpulseApp.directive('ngConfirmClick', ['dialogs',
  function (dialogs) {
      return {
          priority: -1,
          restrict: 'A',
          link: function (scope, element, attrs) {
              element.bind('click', function (e) {
                  var message = attrs.ngConfirmClick;
                  dlg = dialogs.confirm('Пожалуйста, подтвердите действие', message);
                  dlg.result.then(function (btn) {
                     
                  }, function (btn) {
                      e.stopImmediatePropagation();
                      e.preventDefault();
                  });
              });
          }
      }
  }
]);