App.controller("ItemPropertyCtrl", ["$scope", "$rootScope", "$timeout", ItemPropertyCtrl])
function ItemPropertyCtrl($scope, $rootScope, $timeout) {
    window.gr = $scope;

    $scope.model = {
    }

    $scope.model.TypeOnChange = function () {
        $scope.InitType();
    }

    $scope.model.InnerTypeIdOnChange = function () {
        $scope.InitInnerTypeId();
    }

    //$scope.item = {
    //    TypeOnChange : function () {
    //        $scope.InitType();
    //    },

    //    InnerTypeIdOnChange : function () {
    //        $scope.InitInnerTypeId();
    //    }
    //}

    $scope.InitType = function () {
        var type = $scope.model.Type;
        //$scope.item.ItemIdDisabled = true;
        $scope.model.ShowStringComponents = false;
        $scope.model.ShowInnerTypeComponents = false;

        // 8: class, 11: enum
        if (type == 8 || type == 11) {
            $scope.model.ShowInnerTypeComponents = true;
        }
        else {
            $scope.model.InnerTypeId = null;
        }
        
        if (type == 6) {
            $scope.model.ShowStringComponents = true;
        }
    }

    $scope.InitInnerTypeId = function () {
    }

    $scope.InitEdit = function () {
        $timeout(function () {
            console.log($scope.model);
            // $scope.model.Type = "6";
            $scope.InitType();
            $scope.InitInnerTypeId();
        });
    }

    $scope.InitList = function () {
        $timeout(function () {
        });
    }
}