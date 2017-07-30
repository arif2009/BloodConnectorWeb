'use strict';
app.factory('dataService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var dataService = {};

    var _getBloodGroup = function () {

        return $http.get(serviceBase + 'api/BloodGroup').then(function (results) {
            return results;
        });
    };

    dataService.getBloodGroup = _getBloodGroup;

    return dataService;

}]);