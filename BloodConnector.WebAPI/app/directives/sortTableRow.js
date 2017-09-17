'use strict';
var customSort = function () {
    return {
        restrict: 'A',
        transclude: true,
        scope: {
            order: '=',
            sort: '='
        },
        template:
            ' <a ng-click="sort_by(order)" style="color: #555555;">' +
                '    <span ng-transclude></span>' +
                '    <i class="fa" ng-class="selectedCls(order)" aria-hidden="true"></i>' +
                '</a>',
        link: function (scope) {
            
            // change sorting order
            scope.sort_by = function (newSortingOrder) {
                var sort = scope.$parent.vm.sort;
                console.log("scope.sort_by");
                if (sort.sortingOrder === newSortingOrder) {
                    sort.reverse = !sort.reverse;
                }

                sort.sortingOrder = newSortingOrder;
            };


            scope.selectedCls = function (column) {
                var sort = scope.$parent.vm.sort;
                return column === sort.sortingOrder ? 'fa-chevron-' + ((sort.reverse) ? 'down' : 'up') : 'fa-sort';
            };
        } // end link
    }
};

app.directive("customSort", customSort);