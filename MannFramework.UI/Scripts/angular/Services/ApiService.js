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