'use strict';
app.controller('ordersController', ['ordersService', function (ordersService) {

    var vm = this;

    vm.orders = [];

    ordersService.getOrders().then(function (results) {

        vm.orders = results.data;

    }, function (error) {
        //alert(error.data.message);
    });

}]);