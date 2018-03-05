(function (app) {
    'use strict';

    app.factory('notificationService', notificationService);

    function notificationService() {

        var service = {
            displaySuccess: displaySuccess,
            displayError: displayError,
            displayWarning: displayWarning,
            displayInfo: displayInfo
        };

        return service;

        function displaySuccess(message) {
            var placementFrom = 'top';
            var placementAlign = 'right';
            var animateEnter = '';
            var animateExit = '';
            var colorName = 'alert-success';

            showNotification(colorName, message, placementFrom, placementAlign, animateEnter, animateExit);
        }

        function displayError(error) {
            var placementFrom = 'top';
            var placementAlign = 'right';
            var animateEnter = '';
            var animateExit = '';
            var colorName = 'alert-danger';
            if (Array.isArray(error)) {
                error.forEach(function (err) {
                    showNotification(colorName, err, placementFrom, placementAlign, animateEnter, animateExit);
                });
            } else {
                showNotification(colorName, error, placementFrom, placementAlign, animateEnter, animateExit);
            }
        }

        function displayWarning(message) {
            var placementFrom = 'top';
            var placementAlign = 'right';
            var animateEnter = '';
            var animateExit = '';
            var colorName = 'alert-warning';

            showNotification(colorName, message, placementFrom, placementAlign, animateEnter, animateExit);
        }

        function displayInfo(message) {
            var placementFrom = 'top';
            var placementAlign = 'right';
            var animateEnter = '';
            var animateExit = '';
            var colorName = 'alert-info';

            showNotification(colorName, message, placementFrom, placementAlign, animateEnter, animateExit);
        }

        function showNotification(colorName, text, placementFrom, placementAlign, animateEnter, animateExit) {
            if (colorName === null || colorName === '') { colorName = 'bg-black'; }
            if (text === null || text === '') { text = 'Mensaje en blanco'; }
            if (animateEnter === null || animateEnter === '') { animateEnter = 'animated fadeInDown'; }
            if (animateExit === null || animateExit === '') { animateExit = 'animated fadeOutUp'; }
            var allowDismiss = true;

            $.notify({
                message: text
            },
                {
                    type: colorName,
                    allow_dismiss: allowDismiss,
                    newest_on_top: true,
                    timer: 1000,
                    placement: {
                        from: placementFrom,
                        align: placementAlign
                    },
                    animate: {
                        enter: animateEnter,
                        exit: animateExit
                    },
                    template: '<div data-notify="container" class="bootstrap-notify-container alert alert-dismissible {0} ' + (allowDismiss ? "p-r-35" : "") + '" role="alert">' +
                    '<button type="button" aria-hidden="true" class="close" data-notify="dismiss">×</button>' +
                    '<span data-notify="icon"></span> ' +
                    '<span data-notify="title">{1}</span> ' +
                    '<span data-notify="message">{2}</span>' +
                    '<div class="progress" data-notify="progressbar">' +
                    '<div class="progress-bar progress-bar-{0}" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%;"></div>' +
                    '</div>' +
                    '<a href="{3}" target="{4}" data-notify="url"></a>' +
                    '</div>'
                });
        }
    }

})(angular.module('common.core'));