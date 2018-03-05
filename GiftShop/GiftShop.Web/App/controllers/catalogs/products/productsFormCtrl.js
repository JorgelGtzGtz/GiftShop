(function (app) {
    'use strict';
    app.controller('productsFormCtrl', productsFormCtrl);
    productsFormCtrl.$inject = ['$scope', '$rootScope', 'apiService', '$uibModal', 'formData', '$uibModalInstance', '$filter', 'notificationService'];

    function productsFormCtrl($scope, $rootScope, apiService, $uibModal, formData, $uibModalInstance, $filter, notificationService) {

        $scope.title = formData.ID === -1 ? "New Product" : "Edit Product";

        $scope.formData = angular.copy(formData);
        $scope.catlist = [];

        $scope.Save = function () {
            $scope.formData.CategoryID = $scope.formData.Cat.ID;

            apiService.post('/api/Product/save', $scope.formData, null, function (result) {
                if (result.data && result.data.Status === "OK") {
                    notificationService.displaySuccess(result.data.Message);
                    $uibModalInstance.close($scope.formData);
                }
                else if (result.data && result.data.Status === "ERROR") {
                    notificationService.displayError(result.data.Message);
                }
                else {
                    notificationService.displayError('It was not possible to save the Product' + $scope.formData.USUARIO1 + 'please try again !!!');
                }
            });
        };

        $scope.Cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };

        function loadDropdowns() {
            apiService.get('/api/OptionBase/categorylist', null, function (response) {
                $scope.catlist = response.data;
                
                $scope.formData.Cat = $filter('filter')($scope.catlist, function (cat) { return cat.ID === $scope.formData.CategoryID })[0];

                if (!$scope.formData.Cat) {
                    $scope.formData.Cat = $scope.catlist[0];
                }
            });
        }

        loadDropdowns();
    }
})(angular.module('GSApp'));