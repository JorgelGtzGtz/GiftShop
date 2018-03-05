(function (app) {
    'use strict';

    app.directive('passwordVerify', passwordVerify);
    
    function passwordVerify() {
        return {
            require: '?ngModel',
            restrict: 'A',
            scope: {
                origin: '=passwordVerify'
            },
            link: function (scope, element, attrs, ctrl) {
                if (!ctrl) {
                    return;
                }

                ctrl.$validators.passwordmatch = function(modelvalue, viewvalue) {
                    var result = scope.origin === viewvalue;

                    if (result)
                        element[0].setCustomValidity('');
                    else
                        element[0].setCustomValidity('passwordmatch');

                    return result;
                }
                
                scope.$watch('origin', function (value) {
                    ctrl.$validate();
                });
            }
        };
    }
})(angular.module('common.core'));