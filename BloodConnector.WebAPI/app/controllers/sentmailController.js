'use strict';
app.controller('sentmailController', ['localStorageService', function (localStorageService) {
    var vm = this;
    vm.email = localStorageService.get('email');

    vm.message = "";

}]);