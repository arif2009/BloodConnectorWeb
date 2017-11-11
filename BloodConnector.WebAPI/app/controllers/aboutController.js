'use strict';
app.controller('aboutController', ['dataService', function (dataService) {
    var vm = this;
    vm.data = [];

    vm.$onInit = function () {
        dataService.getDevelopersInfo().then(function(result) {
            vm.data = result;
        });
    }

}]);