'use strict';
app.controller('usersController', ['usersService', function (usersService) {

    var vm = this;

    vm.users = [];

    usersService.getUsers().then(function (results) {

        vm.users = results.data;

    }, function (error) {
        //alert(error.data.message);
    });

}]);