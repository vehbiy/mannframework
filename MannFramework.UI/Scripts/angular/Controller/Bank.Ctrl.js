App.controller("BankCtrl", ["$scope", "$rootScope", "$timeout", BankCtrl])

function BankCtrl($scope, $rootScope, $timeout) {
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
