App.controller("ProjectCtrl", ["$scope", "$rootScope", "$timeout", ProjectCtrl])

function ProjectCtrl($scope, $rootScope, $timeout) {
    window.gr = $scope;

    $scope.item = {
    }

    $scope.model = window.model;

    $scope.InitEdit = function () {
        $timeout(function () {
        });
    }
}