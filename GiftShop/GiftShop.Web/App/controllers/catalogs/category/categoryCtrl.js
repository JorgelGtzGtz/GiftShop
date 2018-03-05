(function (app) {
    'use strict';
    app.controller('categoryCtrl', categoryCtrl);
    categoryCtrl.$inject = ['$scope', '$rootScope', 'apiService', '$uibModal',  'pageSize', 'notificationService'];

    function categoryCtrl($scope, $rootScope, apiService, $uibModal, pageSize, notificationService) {
        $scope.list = [];
        $scope.pagerOptions = {
            page: 0,
            pagesCount: 0,
            pageChanged: $scope.Load
        };
        $scope.Criteria = {
            username: '',
            isLocked: false,
            isAdmin: false
        };

        $scope.Load = function (page) {
            page = page || 0;
            $scope.list = [];

            var config = {
                params: {
                    page: page,
                    pageSize: pageSize,
                    Description: $scope.Criteria.Description === '' || $scope.Criteria.Description === undefined ? null : $scope.Criteria.Description
                    
                }
            };

            apiService.get('/api/Category/list', config, function (response) {
                $scope.list = response.data.Items;

                $scope.pagerOptions = {
                    page: response.data.Page,
                    pagesCount: response.data.TotalPages,
                    totalCount: response.data.TotalCount,
                    pageChanged: $scope.Load
                };
            });
        };

        $scope.New = function () {
            showModal({ ID: -1 }, true);
        };

        $scope.Edit = function (row) {
            showModal(row, true);
        };

        $scope.Delete = function (row) {
            swal({
                title: "¿Are you sure?",
                text: "¡You will not be able to recover this information after removing!",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, delete it!",
                cancelButtonText: "No, cancel!",
                closeOnConfirm: false,
                closeOnCancel: false
            }, function (isConfirm) {
                if (isConfirm) {
                    apiService.post('/api/Category/delete', row, null, function (result) {
                        if (result.data && result.data.Status === "OK") {
                            notificationService.displaySuccess(result.data.Message);
                            swal("Removed!", "The Category has been deleted.", "success");
                            $scope.Load($scope.pagerOptions.page);
                        }
                        else if (result.data && result.data.Status === "ERROR") {
                            notificationService.displayError(result.data.Message);
                            swal("Canceled", "The Category could not be deleted.", "error");
                        }
                        else {
                            notificationService.displayError('It was not possible to delete the Category' + row.USUARIO1 + 'please try again !!!');
                        }
                    });
                    
                } else {
                    swal("Canceled", "Your information is safe", "error");
                }
            });
        }

        function showModal(user) {
            user = user || { ID: -1, IsLocked: false, IsAdmin: false};

            var modalInstance = $uibModal.open({
                templateUrl: "./App/views/catalogs/category/form.html",
                controller: 'categoryFormCtrl',
                size: 'lg',
                resolve: {
                    formData: function () { return user; }
                }
            });

            modalInstance.result.then(
                function (selectedItem) {
                    $scope.Load($scope.pagerOptions.page);
                },
                function (x) {
                }
            );
        }
        $scope.Load($scope.pagerOptions.page);
   
    }
})(angular.module('GSApp'));