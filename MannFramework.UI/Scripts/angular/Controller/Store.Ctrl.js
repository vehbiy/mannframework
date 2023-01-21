App.controller("StoreCtrl", ["$scope", "$rootScope", "$timeout", StoreCtrl])

function StoreCtrl($scope, $rootScope, $timeout) {
    window.gr = $scope;

	$scope.model = {
	}


	$scope.InitEdit = function () {
		$timeout(function () {
			
		});
    }

    $scope.InitList = function () {
		$timeout(function () {
		});
    }
}
