App.controller("ProjectSettingCtrl", ["$scope", "$rootScope", "$timeout", ProjectSettingCtrl])

function ProjectSettingCtrl($scope, $rootScope, $timeout) {
    window.gr = $scope;

	$scope.model = {
	}


	$scope.InitEdit = function () {
		$timeout(function () {
            // $scope.item.ProjectIdDisabled = true;
		});
    }

    $scope.InitList = function () {
		$timeout(function () {
		});
    }
}
