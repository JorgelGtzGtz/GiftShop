(function (app) {
    'use strict';
    app.factory('apiService', apiService);
    apiService.$inject = ['$http', '$rootScope', '$timeout', '$state', 'notificationService'];

    function apiService($http, $rootScope, $timeout, $state, notificationService) {

        var service = {
            get: get,
            post: post,
            getErrorMessage: getErrorMessage,
            showError: showError
        };

        function get(url, config, success, fail) {

            $rootScope.GSPromise = $http.get("." + url, config)
                    .then(function (response) {
                        success(response);
                    }, function (error) {
                        showError(error);

                        if (fail)
                            fail(error);
                    });
            return $rootScope.GSPromise;
        }
        function post(url, data, config, success, fail) {

            $rootScope.GSPromise = $http.post("." + url, data, config)
                .then(function (response) {
                    success(response);
                }, function (error) {
                    showError(error.data);

                    if (fail)
                        fail(error.data);
                });

            return $rootScope.GSPromise;
        }
        function getErrorMessage(response) {
            var message = '';

            if (response !== undefined && response.InnerException !== undefined) {
                message = response.InnerException.ExceptionMessage;
            }
            else if (response && response.ExceptionMessage) {
                message = response.ExceptionMessage;
            }
            else if (response !== undefined && response.ErrorMessage) {
                message = response.ErrorMessage;
            }
            else if (response !== undefined && response.Message) {
                message = response.Message;
            }
            else if (response !== undefined && response.error) {
                message = response.error;
            }
            else if (response !== undefined && response.Errors) {
                message = response.Errors;
            }
            else if (response && !angular.isArray(response)) {
                message = response;
            }
            else if (response && angular.isArray(response)) {
                message = response.join('<br/>');
            }
            else {
                message = response;
            }

            return message;
        }
        function showError(response) {
            var message = getErrorMessage(response);
            notificationService.displayError(message);
        }
        return service;
    }

})(angular.module('common.core'));