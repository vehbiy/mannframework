App.controller("SubProjectCtrl", ["$scope", "$rootScope", "$timeout", SubProjectCtrl])

function SubProjectCtrl($scope, $rootScope, $timeout) {
    window.gr = $scope;

	$scope.model = {
	}


	$scope.InitEdit = function () {
		$timeout(function () {
            $scope.item.ProjectIdDisabled = true;
		});
    }

    $scope.InitList = function () {
		$timeout(function () {
		});
    }
}
