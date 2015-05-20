ImpulseApp.controller('ModalController', function ($scope, $modalInstance) {
    $scope.closeModal = function () {
        $modalInstance.dismiss('cancel');
    };
});