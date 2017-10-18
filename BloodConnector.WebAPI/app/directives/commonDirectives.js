'use strict';
var compareTo = function () {
    return {
        require: "ngModel",
        scope: {
            otherModelValue: "=compareTo"
        },
        link: function (scope, element, attributes, ngModel) {

            ngModel.$validators.compareTo = function (modelValue) {
                return modelValue === scope.otherModelValue;
            };

            scope.$watch("otherModelValue", function () {
                ngModel.$validate();
            });
        }
    };
};

var jqDatePicker = function() {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, element, attrs, ngModelCtrl) {
            element.datepicker({
                dateFormat: 'dd.mm.yy',
                onSelect: function(date) {
                    scope.$parent.vm.profile.dateOfBirth = date;
                    scope.$apply();
                }
            });
        }
    };
};

app.directive("compareTo", compareTo);
app.directive("jqdatepicker", jqDatePicker);