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

var myEnter = function () {
    return function (scope, element, attrs) {
        element.bind("keydown keypress", function (event) {
            if (event.which === 13) {
                scope.$apply(function () {
                    scope.$eval(attrs.myEnter);
                });

                event.preventDefault();
            }
        });
    };
};

app.directive('uploadFiles', function () {
    return {
        scope: true,        //create a new scope  
        link: function (scope, el, attrs) {
            el.bind('change', function (event) {
                var files = event.target.files;
                //iterate files since 'multiple' may be specified on the element  
                for (var i = 0; i < files.length; i++) {
                    //emit event upward  
                    scope.$emit("seletedFile", { file: files[i] });
                }
            });
        }
    };
});

app.directive("compareTo", compareTo);
app.directive("jqdatepicker", jqDatePicker);
app.directive("myEnter", myEnter);