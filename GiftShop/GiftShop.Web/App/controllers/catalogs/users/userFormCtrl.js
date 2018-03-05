(function (app) {
    'use strict';
    app.controller('userFormCtrl', userFormCtrl);
    userFormCtrl.$inject = ['$scope', '$rootScope', 'apiService', '$uibModal', 'formData', '$uibModalInstance', '$filter', 'notificationService'];

    function userFormCtrl($scope, $rootScope, apiService, $uibModal, formData, $uibModalInstance, $filter, notificationService) {

        $scope.title = formData.ID === -1 ? "New User" : "Edit User";

        $scope.formData = angular.copy(formData);

        $scope.Save = function () {
            apiService.post('/api/Users/save', $scope.formData, null, function (result) {
                if (result.data && result.data.Status === "OK") {
                    notificationService.displaySuccess(result.data.Message);
                    $uibModalInstance.close($scope.formData);
                }
                else if (result.data && result.data.Status === "ERROR") {
                    notificationService.displayError(result.data.Message);
                }
                else {
                    notificationService.displayError('It was not possible to save the user' + $scope.formData.USUARIO1 + 'please try again !!!');
                }
            });
        };

        $scope.Cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };
    }
})(angular.module('GSApp'));