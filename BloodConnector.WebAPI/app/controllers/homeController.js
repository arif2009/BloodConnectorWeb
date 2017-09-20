'use strict';
app.controller('homeController', ['dataService', function (dataService) {
    var vm = this;
    vm.usersBloodGroup = [];

    vm.$onInit = function () {
        dataService.getUsersBloodGroup.then(function (result) {
            vm.usersBloodGroup = result.data;
        });
    };

}]);