'use strict';
app.factory('dataService', ['$http', 'ngAuthSettings', 'utilsFactory', function ($http, ngAuthSettings, utilsFactory) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var dataService = {};

    dataService.getBloodGroup = function() {
        return $http.get(serviceBase + 'api/bloodgroup');
    }

    dataService.getCountry = function (id) {
        return $http.get(serviceBase + 'api/country');
    }

    dataService.getUsersBloodGroup = function() {
        return $http.get(serviceBase + 'api/bloodgroup/getusersbloodgroup');
    }

    dataService.getUserById = function(id) {
        return $http.get(serviceBase + 'api/users/' + id);
    }

    dataService.getDevelopersInfo = function() {
        return $http.get(serviceBase + 'api/developers').then(function (result) {
            return {
                'arif': utilsFactory.getWhere(result.data, 'userId', ngAuthSettings.developers.arif),
                'jahangir': utilsFactory.getWhere(result.data, 'userId', ngAuthSettings.developers.jahangir),
                'mafi': utilsFactory.getWhere(result.data, 'userId', ngAuthSettings.developers.mafi)
            }
        });
    }

    return dataService;

}]);