App.controller("IconCtrl", ["$scope", "$rootScope", "$timeout", IconCtrl])

function IconCtrl($scope, $rootScope, $timeout) {
    window.gr = $scope;

    $scope.model = window.model;

	$scope.InitEdit = function () {
		$timeout(function () {
			
		});
    }

    $scope.InitList = function () {
		$timeout(function () {
		});
    }
}
