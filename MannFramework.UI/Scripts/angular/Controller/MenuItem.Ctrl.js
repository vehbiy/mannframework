App.controller("MenuItemCtrl", ["$scope", "$rootScope", "$timeout", "ApiService", MenuItemCtrl])

function MenuItemCtrl($scope, $rootScope, $timeout, ApiService) {
    window.gr = $scope;
    $scope.model = window.model;

    $scope.InitIcons = function () {
        ApiService.GetIcons().then(function (res) {
            $scope.model.Icons = res.data;
            $scope.IconIdOnChange();
            // burada responsea bakarak hareket etmek lazim
        });
    }

    $scope.model.IconIdOnChange = function () {
        $scope.IconIdOnChange();
    }

    $scope.IconIdOnChange = function () {
        


        //ApiService.GetIcon($scope.model.IconId).then(function (res) {
        //    console.log(res.data);
        //    $scope.model.Icon = res.data;
        //});
        //for (var i = 0; i < $scope.model.Icons.length; i++) {
        //    if ($scope.model.Icons[i].Id == $scope.model.IconId) {
        //        $scope.model.Icon = $scope.model.Icons[i];
        //        break;
        //    }
        //}
    }

    $scope.InitEdit = function () {
        $timeout(function () {
            $scope.InitIcons();
            // $scope.IconIdOnChange();
        });
    }

    $scope.InitList = function () {
        $timeout(function () {
        });
    }
}
