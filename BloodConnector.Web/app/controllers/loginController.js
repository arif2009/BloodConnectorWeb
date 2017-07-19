'use strict';
app.controller('loginController', ['$location', 'authService', 'ngAuthSettings', function ($location, authService, ngAuthSettings) {
    var vm = this;

    vm.loginData = {
        userName: "",
        password: "",
        useRefreshTokens: false
    };

    vm.message = "";

    vm.login = function () {

        authService.login(vm.loginData).then(function (response) {

            $location.path('/orders');

        },
         function (err) {
             vm.message = err.error_description;
         });
    };

    vm.authExternalProvider = function (provider) {

        var redirectUri = location.protocol + '//' + location.host + '/authcomplete.html';

        var externalProviderUrl = ngAuthSettings.apiServiceBaseUri + "api/Account/ExternalLogin?provider=" + provider
                                                                    + "&response_type=token&client_id=" + ngAuthSettings.clientId
                                                                    + "&redirect_uri=" + redirectUri;
        window.$windowScope = vm;

        var oauthWindow = window.open(externalProviderUrl, "Authenticate Account", "location=0,status=0,width=600,height=750");
    };

    vm.authCompletedCB = function (fragment) {

        vm.$apply(function () {

            if (fragment.haslocalaccount == 'False') {

                authService.logOut();

                authService.externalAuthData = {
                    provider: fragment.provider,
                    userName: fragment.external_user_name,
                    externalAccessToken: fragment.external_access_token
                };

                $location.path('/associate');

            }
            else {
                //Obtain access token and redirect to orders
                var externalData = { provider: fragment.provider, externalAccessToken: fragment.external_access_token };
                authService.obtainAccessToken(externalData).then(function (response) {

                    $location.path('/orders');

                },
             function (err) {
                 vm.message = err.error_description;
             });
            }

        });
    }
}]);
