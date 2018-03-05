(function () {
    'use strict';
    angular.module('common.core', [
        'ngRoute',
        'ngSanitize',
        'ngCookies',
        'ngMessages',
        'ngAnimate',
        'LocalStorageModule',
        'base64',
        'ui.bootstrap',
        'ui.bootstrap.tpls',
        'ui.router',
        'cgBusy'
    ]);
})();