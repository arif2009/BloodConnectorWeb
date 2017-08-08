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
                '    <i ng-class="selectedCls(order)"></i>' +
                '</a>',
        link: function (scope) {
            
            // change sorting order
            scope.sort_by = function (newSortingOrder) {
                var sort = scope.sort;
                console.log("scope.sort_by");
                console.log(sort);
                console.log(newSortingOrder);
                if (sort.sortingOrder == newSortingOrder) {
                    sort.reverse = !sort.reverse;
                }

                sort.sortingOrder = newSortingOrder;
            };


            scope.selectedCls = function (column) {
                console.log("scope.selectedCls");
                console.log(scope);
                if (column == scope.sort.sortingOrder) {
                    return ('icon-chevron-' + ((scope.sort.reverse) ? 'down' : 'up'));
                } else {
                    return 'icon-sort';
                }
            };
        } // end link
    }
};

app.directive("customSort", customSort);