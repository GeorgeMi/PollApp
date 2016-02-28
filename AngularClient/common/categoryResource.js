(function () {
    "use strict";
    angular
        .module("common.services")
        .factory("categoryResource", ["$resource", "appSettings", "$cookies", categoryResource])

    function categoryResource($resource, appSettings, $cookies) {
        return {
            get: $resource(appSettings.serverPath + "/api/category", null,
                      {
                          'getCategories': {
                              method: 'GET',
                              isArray: true,
                              headers: { 'Content-Type': 'application/x-www-form-urlencoded', 'token': $cookies.get('token') },
                              transformRequest: function (data, headersGetter) {
                                  var str = [];
                                  for (var d in data) {
                                      str.push(encodeURIComponent(d) + "=" + encodeURIComponent(data[d]));
                                  }
                                  return str.join("&");
                              }
                          }
                      }),

            add: $resource(appSettings.serverPath + "/api/category", null,
               {
                   'addCategory': {
                       method: 'POST',
                       headers: { 'Content-Type': 'application/x-www-form-urlencoded', 'token': $cookies.get('token') },
                       transformRequest: function (data, headersGetter) {
                           var str = [];
                           for (var d in data) {
                               str.push(encodeURIComponent(d) + "=" + encodeURIComponent(data[d]));
                           }
                           return str.join("&");
                       }
                   }
               }),
            delete: $resource(appSettings.serverPath + "/api/category/:cat_id", { cat_id: '@id' },
               {
                   'deleteCategory': {
                       method: 'DELETE',
                       headers: { 'Content-Type': 'application/x-www-form-urlencoded', 'token': $cookies.get('token') },
                       transformRequest: function (data, headersGetter) {
                           var str = [];
                           for (var d in data) {
                               str.push(encodeURIComponent(d) + "=" + encodeURIComponent(data[d]));
                           }
                           return str.join("&");
                       }
                   }
               })
        }
    }

}());