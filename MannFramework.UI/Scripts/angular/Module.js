
var App = angular.module("Garcia", [])
    .service('ApiService', ['$q', '$http', '$rootScope', ApiService])
    .run(['$rootScope','$timeout', '$location', 'ApiService', Run])  
    .config(["$locationProvider", function ($locationProvider) {
        $locationProvider.html5Mode({
            enabled: true,
            requireBase: false,
            rewriteLinks: false
        });
    }]);