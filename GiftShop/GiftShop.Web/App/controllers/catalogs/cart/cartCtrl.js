(function (app) {
    'use strict';
    app.controller('cartCtrl', cartCtrl);
    cartCtrl.$inject = ['$scope', '$rootScope', 'apiService', '$uibModal', 'pageSize', 'notificationService'];

    function cartCtrl($scope, $rootScope, apiService, $uibModal, pageSize, notificationService) {
        $scope.list = [];
   
    }
})(angular.module('GSApp'));