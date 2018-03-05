(function (app) {
    'use strict';

    app.directive('shPagination', pagination);
    pagination.$inject = ['pageSize'];

    function pagination(pageSize) {
        return {
            scope: {
                page: '@',
                pagesCount: '@',
                totalCount: '@',
                pageChanged: '&',
                customPath: '@',
                pagerText: '@'
            },
            replace: true,
            restrict: 'E',
            templateUrl: './app/views/templates/pager.html?' + new Date().getMilliseconds(),
            controller: ['$scope', function ($scope) {
                $scope.onPageChanged = function (i) {
                    if ($scope.pageChanged) {
                        $scope.pageChanged({ page: i });
                    }
                };
                $scope.range = function () {
                    if (!$scope.pagesCount) { return []; }
                    var step = 2;
                    var doubleStep = step * 2;
                    var start = Math.max(0, $scope.page - step);
                    var end = start + 1 + doubleStep;
                    if (end > $scope.pagesCount) { end = $scope.pagesCount; }

                    var ret = [];
                    for (var i = start; i != end; ++i) {
                        ret.push(i);
                    }

                    return ret;
                };
                $scope.pagePlus = function (count) {
                    return +$scope.page + count;
                }

                $scope.$watch("page", updatePageText);
                $scope.$watch("totalCount", updatePageText);

                function updatePageText() {
                    var fromRows = (($scope.page * pageSize) + 1),
                    rows = ($scope.page * pageSize) + pageSize,
                    totalRows = rows > $scope.totalCount ? $scope.totalCount : rows;
                    $scope.pagerText = "Show " + fromRows + " from " + totalRows + " from " + $scope.totalCount + " Rows";
                }
            }]
        }
    }
})(angular.module('common.ui'));