App.controller("MemberCtrl", ["$scope", "$rootScope", "$timeout", "ApiService", MemberCtrl])

function MemberCtrl($scope, $rootScope, $timeout, ApiService) {
    window.gr = $scope;

    $scope.model = {
        showPassword: false
    }

    $scope.InitEdit = function () {
        $timeout(function () {

        });
    }

    $scope.InitList = function () {
        $timeout(function () {
        });
    }

    $scope.GeneratePassword = function () {
        ApiService.GenerateKey().then(function (response) {
            $scope.model.Password = response.data;
            $scope.model.ConfirmPassword = response.data;
            //var result = document.getElementById("Password");
            //result.removeAttribute("type");
            $scope.model.showPassword = true;
        });
    }

    $scope.CopyPasswordToClipboard = function () {
        var result = document.getElementById("Password");
        result.select();
        document.execCommand("Copy");
        $scope.model.showPassword = false;
        // result.addAttribute("type", "password");
    }
}
