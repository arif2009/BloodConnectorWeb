'use strict';
app.factory('dataService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var dataService = {};

    dataService.getBloodGroup = function() {
        return $http.get(serviceBase + 'api/bloodgroup');
    }

    dataService.getUsersBloodGroup = function() {
        return $http.get(serviceBase + 'api/bloodgroup/getusersbloodgroup');
    }

    dataService.getUserById = function(id) {
        return $http.get(serviceBase + 'api/users/' + id);
    }

    return dataService;

}]);