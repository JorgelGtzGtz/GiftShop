(function () {
    'use strict';

    var app = angular.module('GSApp', ['common.core', 'common.ui']).config(config).run(run);

    app.constant('pageSize', 50);
    app.value('cgBusyDefaults', {
        message: 'Loading...',
        backdrop: true,
        templateUrl: './app/views/templates/angular-busy.html',
        delay: 0,
        minDuration: 0,
        wrapperClass: 'cg-busy cg-busy-animation'
    });

    config.$inject = ['$stateProvider', '$locationProvider', 'localStorageServiceProvider', '$urlRouterProvider', '$urlMatcherFactoryProvider', '$uibModalProvider'];
    function config($stateProvider, $locationProvider, localStorageServiceProvider, $urlRouterProvider, $urlMatcherFactoryProvider, $uibModalProvider) {
        $uibModalProvider.options.keyboard = false;
        $uibModalProvider.options.backdrop = 'static';

        $urlRouterProvider.otherwise("/home");
        $urlMatcherFactoryProvider.strictMode(false);
        var milliseconds = new Date().getMilliseconds();

        $stateProvider
            .state("login", {
                url: '/login',
                views: {
                    'main@': {
                        templateUrl: './app/views/account/login.html?' + milliseconds,
                        controller: "loginCtrl"
                    }
                }
            })
            .state("home", {
                url: '/home',
                views: {
                    'main@': {
                        templateUrl: './app/views/home/index.html?' + milliseconds,
                        controller: "homeCtrl"
                    }
                },
                data: {
                    authorization: true,
                    redirectTo: 'login',
                    memory: true
                }
            })
            .state("home.user", {
                url: '/user',
                views: {
                    'content@home': {
                        templateUrl: './app/views/catalogs/users/list.html?' + milliseconds,
                        controller: "usersCtrl"
                    }
                },
                data: {
                    authorization: true,
                    redirectTo: 'login',
                    memory: true
                }
            }).state("home.categories", {
                url: '/categories',
                views: {
                    'content@home': {
                        templateUrl: './App/views/catalogs/category/list.html',
                        controller: "categoryCtrl"
                    }
                },
                data: {
                    authorization: true,
                    redirectTo: 'login',
                    memory: true
                }
            }).state("home.products", {
                url: '/products',
                views: {
                    'content@home': {
                        templateUrl: './App/views/catalogs/products/list.html',
                        controller: "productsCtrl"
                    }
                },
                data: {
                    authorization: true,
                    redirectTo: 'login',
                    memory: true
                }
            }).state("home.cart", {
                url: '/cart',
                views: {
                    'content@home': {
                        templateUrl: './App/views/catalogs/cart/list.html',
                        controller: "cartCtrl"
                    }
                },
                data: {
                    authorization: true,
                    redirectTo: 'login',
                    memory: true
                }
            });

        $locationProvider.html5Mode(true);
    }

    run.$inject = ['$rootScope', 'localStorageService', '$http', '$timeout', '$state', '$templateCache', '$window'];
    function run($rootScope, localStorageService, $http, $timeout, $state, $templateCache, $window) {

        $rootScope.ShopContent = [],
        $rootScope.ShopTotalPrice = 0,
        $rootScope.ShopTotalProducts = 0;

        $rootScope.isLoadingApp = true;

        $rootScope.globals = localStorageService.get('globals') || {};

        $rootScope.SistemaGestionPromise = undefined;

        if ($rootScope.globals.currentUser) {
            $http.defaults.headers.common['Authorization'] = 'GS ' + $rootScope.globals.currentUser.authdata;
        }

        $rootScope.view = {
            setTitle: function (title) {
                this.title = title;
            },
            setTitleTemplate: function (template) {
                this.titleTemplate = template;
            }
        };

        $rootScope.$on('$stateChangeStart', function (e, toState, toParams, fromState, fromParams) {
            if (!$rootScope.ignorePreviousState) {
                $rootScope.previousState = fromState;
                $rootScope.previousStateParams = fromParams;
            }

            $rootScope.ignorePreviousState = false;

            if (toState.title) {
                $rootScope.view.setTitle(toState.title || '');
                $rootScope.view.setTitleTemplate(toState.titleTemplate || '');
            }
        });

        $rootScope.onLogout = function () {
            Authorization.clear();
            $rootScope.repository = {};
            localStorageService.remove('repository');
            $http.defaults.headers.common.Authorization = '';
            $http.defaults.headers.common.Token = '';
            $state.go('login');
        };

        $(document).ready(function () {
            $rootScope.isLoadingApp = false;
            setTimeout(function () { $('.page-loader-wrapper').fadeOut(); }, 50);
        });
    }

    isAuthenticated.$inject = ['AutenticacionService', '$rootScope', '$state'];
    function isAuthenticated(AutenticacionService, $rootScope, $state) {
        if (!AutenticacionService.isUserLoggedIn()) {
            $rootScope.previousState = $state.current;
            $state.go('account.login');
        }
    }

    isNotAuthenticated.$inject = ['AutenticacionService', '$state'];
    function isNotAuthenticated(AutenticacionService, $state) {
        if (AutenticacionService.isUserLoggedIn()) {
            $state.go('account.login');
        }
    }
})();
