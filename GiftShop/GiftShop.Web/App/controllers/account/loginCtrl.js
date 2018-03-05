(function (app) {
    'use strict';

    app.controller('loginCtrl', loginCtrl);

    loginCtrl.$inject = ['$scope', '$rootScope', 'apiService', '$state', 'AuthenticationService', '$uibModal', '$base64', 'notificationService'];

    function loginCtrl($scope, $rootScope, apiService, $state, AuthenticationService, $uibModal, $base64, notificationService) {
        $scope.login = function () {
            $scope.dataLoading = true;
            $scope.loginError = false;
            AuthenticationService.Login($scope.Username, $scope.Password, function (response) {
                if (response.Status === 'OK') {
                    AuthenticationService.SetCredentials($scope.Username, $scope.Password);
                    notificationService.displaySuccess('Welcome ' + $scope.Username);
                    $state.go('home');
                } else {
                    $scope.message = response.Message;
                    notificationService.displayError($scope.message);
                    $scope.loginError = true;
                }
            });
        };
    }
})(angular.module('GSApp'));
