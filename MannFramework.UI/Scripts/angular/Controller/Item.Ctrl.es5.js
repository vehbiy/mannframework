"use strict";

App.controller("ItemCtrl", ["$scope", "$rootScope", "$timeout", "ApiService", ItemCtrl]);
// var UNDEFINED = 'undefined';

function ItemCtrl($scope, $rootScope, $timeout, ApiService) {
    window.gr = $scope;

    $scope.item = {
        ProjectIdOnChange: function ProjectIdOnChange() {
            $scope.ProjectIdChange();
        }
    };

    $scope.model = window.model;

    $scope.ProjectIdChange = function () {
        if ($scope.item.ProjectId != undefined && $scope.item.ProjectId != "") {
            ApiService.GetProjectItems($scope.item.ProjectId).then(function (res) {
                if (typeof res != UNDEFINED) {
                    if (res.data != null) {
                        $scope.Items = res.data;
                        // console.log(model.ItemId); // donguyle donup dogru itemi sectirebiliriz
                        //for (var i = 0; i < res.data.length; i++) {
                        //    if (res.data[i].Id == model.ItemId) {
                        //        $scope.item.Item = res.data[i];
                        //        break;
                        //    }
                        //}

                        //if (model.ItemId == null && res.data.length != 0) {
                        //    model.ItemId = res.data[0].Id;
                        //}

                        //if (res.data.length != 0) {
                        //    model.ItemId = res.data[0].Id;
                        //}

                        if (res.data.length != 0) {
                            var found = false;

                            for (var i = 0; i < res.data.length; i++) {
                                if (res.data[i].Id == model.ItemId) {
                                    found = true;
                                    break;
                                }
                            }

                            if (model.ItemId == null || found == false) {
                                model.ItemId = res.data[0].Id;
                            }
                        }
                    } else {
                        $scope.Items = null;
                    }
                }
            });
        } else {
            $scope.Items = null;
        }
    };

    $scope.InitEdit = function () {
        $timeout(function () {
            // $scope.item.ProjectIdDisabled = true; // server-side disabled'a cekildi. id varsa disabled oluyor, yoksa enabled kaliyor
        });
    };

    $scope.InitList = function () {
        $timeout(function () {});
    };

    $scope.InitCopy = function () {
        $timeout(function () {
            $scope.ProjectIdChange();
        });
    };
}

