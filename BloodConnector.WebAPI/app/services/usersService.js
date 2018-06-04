'use strict';
app.factory('usersService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var usersServiceFactory = {};

    usersServiceFactory.getUsers = function () {
        return $http.get(serviceBase + 'api/users').then(function (results) {
            return results;
        });
    };

    usersServiceFactory.updateUser = function (data) {
        return $http.put(serviceBase + 'api/users', data);
    };

    usersServiceFactory.deleteUser = function (id) {
        return $http.delete(serviceBase + 'api/users/'+id);
    };

    return usersServiceFactory;

}]);