(function (app) {
    'use strict';

    app.controller('homeCtrl', homeCtrl);

    homeCtrl.$inject = ['$scope', '$rootScope', 'apiService', '$state'];

    function homeCtrl($scope, $rootScope, apiService, $state) {

        function loadDropdowns() {
            apiService.get('/api/OptionBase/prodlist', null, function (response) {
                $scope.prodlist = response.data;
            });
        }
        loadDropdowns();

        $scope.AddToCart = function (prod) {
            $rootScope.ShopContent.push(prod);
            $rootScope.ShopTotalProducts = $rootScope.ShopContent.length;
        }

    }
})(angular.module('GSApp'));
