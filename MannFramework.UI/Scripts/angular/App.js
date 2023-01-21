var App = angular.module("Garcia", [])
    .service('ApiService', ['$q', '$http', '$rootScope', ApiService])
    .run(['$rootScope','$timeout', '$location', 'ApiService', Run])  
    .config(["$locationProvider", function ($locationProvider) {
        $locationProvider.html5Mode({
            enabled: true,
            requireBase: false,
            rewriteLinks: false
        });
    }]);
function Run($rootScope, $cookies, $timeout, $location) {
    window.AA = $rootScope;


}
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

App.controller("CashoutTypeCtrl", ["$scope", "$rootScope", "$timeout", CashoutTypeCtrl])

function CashoutTypeCtrl($scope, $rootScope, $timeout) {
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


"use strict";function ItemCtrl(n,t,i,r){window.gr=n;n.item={ProjectIdOnChange:function(){n.ProjectIdChange()}};n.model=window.model;n.ProjectIdChange=function(){n.item.ProjectId!=undefined&&n.item.ProjectId!=""?r.GetProjectItems(n.item.ProjectId).then(function(t){var r,i;if(typeof t!=UNDEFINED)if(t.data!=null){if(n.Items=t.data,t.data.length!=0){for(r=!1,i=0;i<t.data.length;i++)if(t.data[i].Id==model.ItemId){r=!0;break}(model.ItemId==null||r==!1)&&(model.ItemId=t.data[0].Id)}}else n.Items=null}):n.Items=null};n.InitEdit=function(){i(function(){})};n.InitList=function(){i(function(){})};n.InitCopy=function(){i(function(){n.ProjectIdChange()})}}App.controller("ItemCtrl",["$scope","$rootScope","$timeout","ApiService",ItemCtrl]);
App.controller("ItemCtrl", ["$scope", "$rootScope", "$timeout", "ApiService", ItemCtrl])
// var UNDEFINED = 'undefined';

function ItemCtrl($scope, $rootScope, $timeout, ApiService) {
    window.gr = $scope;

    $scope.item = {
        ProjectIdOnChange: function () {
            $scope.ProjectIdChange();
        }
    }

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
    }

    $scope.InitEdit = function () {
        $timeout(function () {
            // $scope.item.ProjectIdDisabled = true; // server-side disabled'a cekildi. id varsa disabled oluyor, yoksa enabled kaliyor
        });
    }

    $scope.InitList = function () {
        $timeout(function () {
        });
    }

    $scope.InitCopy = function () {
        $timeout(function () {
            $scope.ProjectIdChange();
        });
    }
}

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

App.controller("MemberInRoleCtrl", ["$scope", "$rootScope", "$timeout", MemberInRoleCtrl])

function MemberInRoleCtrl($scope, $rootScope, $timeout) {
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

App.controller("MenuItemRoleCtrl", ["$scope", "$rootScope", "$timeout", MenuItemRoleCtrl])

function MenuItemRoleCtrl($scope, $rootScope, $timeout) {
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

App.controller("ReconciliationCtrl", ["$scope", "$rootScope", "$timeout", ReconciliationCtrl])

function ReconciliationCtrl($scope, $rootScope, $timeout) {
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

App.controller("RoleCtrl", ["$scope", "$rootScope", "$timeout", RoleCtrl])

function RoleCtrl($scope, $rootScope, $timeout) {
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

function ApiService($q, $http, $rootScope) {
    var pendingRequests = [];

    var PATH = {
        Api: {
            Url: document.getElementById("ApiUrl").value // web configden ayarladığım url i layout içinden hidden input değeri olarak alıyorum
        }
    };

    PATH.Api.Current = PATH.Api.Url;

    this.GetProjectItems = function (id) {
        return Get("/Project/Items/" + id);
    }

    this.GetIcon = function (id) {
        return Get("/Icon/" + id);
    }

    this.GetIcons = function (id) {
        return Get("/Icon/");
    }

    this.GenerateKey = function () {
        return Get("/GenerateKey");
    }






    this.GetProductCurrencies = function () {
        return AjaxRequest("GET", PATH.Api.Current + "/product/productcurrencies", "", true);
    }


    //Ürün Türün ve Kategoriye göre listeleme
    this.GetBTProductList = function (data) {
        return AjaxRequest("GET", PATH.Api.Current + "/product/productsbybrandandtype/" + data + "", true);
    }


    //Ürün Markaları müşteri ataması   
    this.SaveCategoryBrand = function (data) {
        return AjaxRequest("POST", PATH.Api.Current + "/CategoryBrand/savecategorybrand", data, "", true);
    }


    this.DeleteCategory = function (data) {
        return AjaxRequest("DELETE", PATH.Api.Current + "/Category/" + data, "", true);
    }





    this.CancelAllRequests = function () {
        angular.forEach(pendingRequests, function (p) {
            p.canceller.resolve();
        });
        pendingRequests.length = 0;
    };

    this.CancelSingleRequests = function (data) {
        for (var i = 0; i < pendingRequests.length; i++) {
            if (pendingRequests[i].url.indexOf(PATH.Api.Current + data) >= 0) {
                pendingRequests[i].canceller.resolve();
                console.log(pendingRequests[i].canceller.resolve())
            }
        }
        pendingRequests.length = 0;
    };

    function Get(path) {
        return AjaxRequest("GET", PATH.Api.Current + path, "", true);
    }

    function Post(path, data) {
        return AjaxRequest("POST", PATH.Api.Current + path, data, "", true);
    }

    function AjaxRequest(methodType, path, data, credientals, header) {
        var deferred = $q.defer();
        pendingRequests.push({
            url: path,
            canceller: deferred
        });

        $http({
            method: methodType,
            url: path,
            data: data,
            timeout: deferred.promise,
            withCredentials: true,
            cache: false
        }).then(function successCallback(response) {
            //console.log(response)            
            deferred.resolve(response);
        }, function errorCallback(response) {
            //toastr.warning(response.config.method + "<br/>" + response.config.url, 'Hata!', { allowHtml: true });
            deferred.resolve(response);
        });

        return deferred.promise;
    }


}