(function () {
    'use strict';

    angular.module('registration', [])

        .controller('HomeController', function ($scope, $http) {

            $http.get('api/user').then(function (response) {
                $scope.users = response.data;
            });

          

           
        });

})();