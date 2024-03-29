﻿'use strict';
app.factory('utilsFactory', ['$location', '$timeout', function ($location, $timeout) {
    return {
        processModelstateError: function(modelstate) {
            var errors = [];
            for (var key in modelstate) {
                for (var i = 0; i < modelstate[key].length; i++) {
                    errors.push(modelstate[key][i]);
                }
            }
            return errors;
        },
        redirectToLogin : function() {
            var timer = $timeout(function () {
                $timeout.cancel(timer);
                $location.path('/login');
            }, 2000);
        },
        redirectToUsers : function() {
            $location.path('/users');
        },
        getWhere: function (array, field, value) {

            for (var i = 0; i < array.length; i++) {
                if (array[i][field] === value)
                    return array[i];
            }
            return false;
        },
        getCompositeWhere: function (array, field1, field2, value1, value2) {

            for (var i = 0; i < array.length; i++) {
                if (array[i][field1] === value1 && array[i][field2] === value2)
                    return array[i];
            }
            return false;
        },
        getWhereMultiple: function (array, field, value) {

            var multiple = [];

            for (var i = 0; i < array.length; i++) {
                if (array[i][field] === value)
                    multiple.push(array[i]);
            }
            return multiple;
        },
        deleteElement: function (array, field, value) {

            for (var i = 0; i < array.length; i++) {
                if (array[i][field] === value)
                    array.splice(i, 1);
            }
        }
    };
}]);