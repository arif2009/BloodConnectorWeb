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

//https://stackoverflow.com/a/33129539/3835843
var jqDatePicker = function ($filter) {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, element, attrs, ngModelCtrl) {
            $.datepicker.setDefaults($.datepicker.regional['en-US']);
            element.datepicker({
                changeMonth: true,
                changeYear: true,
                yearRange: "-50:+0",
                onSelect: function (date) {
                    scope.$parent.vm.profile.dateOfBirth = date;
                    scope.$apply();
                }
            });
            ngModelCtrl.$formatters.unshift(function (v) {
                return $filter('date')(v, 'shortDate');
            });
        }
    };
};

app.directive("compareTo", compareTo);
app.directive("jqdatepicker", jqDatePicker);