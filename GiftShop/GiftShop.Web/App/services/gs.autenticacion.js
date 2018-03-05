(function (app) {
    'use strict';

    var idUser = '';
    var UserName = '';
    var Email = '';
    var IsLocked = '';
    var IsAdmin = '';

    app.factory('AuthenticationService', AuthenticationService);
    AuthenticationService.$inject = ['apiService', '$http', '$base64', 'localStorageService', '$rootScope'];

    function AuthenticationService(apiService, $http, $base64, localStorageService, $rootScope) {

        var apiServiceUrl = '/api/Login/';
        var service = {};

        service.Login = function (Username, Password, callback) {
            var user = {}

            user.Username = Username;
            user.Password = Password;

            apiService.post(apiServiceUrl + 'authenticate', user , null, function (response) {
                if (response.data.Status !== "ERROR") {
                    idUser = response.data.ID;
                    UserName = response.data.Username;
                    Email = response.data.Email;
                    IsLocked = response.data.IsLocked;
                    IsAdmin = response.data.IsAdmin;
                }

                callback(response.data);
            });
        };

        service.SetCredentials = function (Username, Password) {
            var authdata = $base64.encode(Username + ':' + Password);
            
            $rootScope.globals = {
                currentUser: {
                    idUser: idUser,
                    Username: UserName,
                    Email: Email,
                    IsLocked: IsLocked,
                    IsAdmin: IsAdmin,
                    authdata: authdata
                }
            };

            $http.defaults.headers.common['Authorization'] = 'GS ' + authdata; 
            localStorageService.set('globals', $rootScope.globals);
        };

        service.ClearCredentials = function () {
            $rootScope.globals = {};
            localStorageService.clearAll();
            $http.defaults.headers.common.Authorization = 'GS ';
        };

        service.isAdmin = function () {
            return $rootScope.globals.currentUser.IsAdmin !== false;
        };

        service.isUser = function () {
            return $rootScope.globals.currentUser.role === false;
        };

        service.isUserLoggedIn = function () {
            return $rootScope.globals.currentUser !== null && $rootScope.globals.currentUser !== undefined;
        };

        return service;
    }
})(angular.module('common.core'));