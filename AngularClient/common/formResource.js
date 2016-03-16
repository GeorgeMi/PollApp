(function () {
    "use strict";
    angular
        .module("common.services")
        .factory("formResource", ["$resource", "appSettings", "$cookies", formResource])

    function formResource($resource, appSettings, $cookies) {
        return {
            //all forms
            get: $resource(appSettings.serverPath + "/api/form", null,
                      {
                          'getForms': {
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
            //specific form
            getForm: $resource(appSettings.serverPath + "/api/form/getform/:form_id", { form_id: '@id' },
                     {
                         'getForm': {
                             method: 'GET',
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
            //voted forms
            getVotedForms: $resource(appSettings.serverPath + "/api/form/voted/" + $cookies.get('username'), null,
                     {
                         'getVotedForms': {
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
            //forms from category
            getCategoryForms: $resource(appSettings.serverPath + "/api/form/category/:category_id", { category_id: '@id' },
                     {
                         'getCategoryForms': {
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
            //add form
            add: $resource(appSettings.serverPath + "/api/form", null,
               {
                   'addForm': {
                       method: 'POST',
                       headers: { 'Content-Type': 'application/json', 'token': $cookies.get('token') }
                   }

               }),
            //send form with vote
            vote: $resource(appSettings.serverPath + "/api/vote", null,
              {
                  'voteForm': {
                      method: 'POST',
                      headers: { 'Content-Type': 'application/json', 'token': $cookies.get('token') }
                  }

              }),
            //all forms from user
            getForms: $resource(appSettings.serverPath + "/api/form/user/" + $cookies.get('username'), null,
                      {
                          'getForms': {
                              method: 'POST',
                              isArray: true,
                              headers: { 'Content-Type': 'application/x-www-form-urlencoded', 'token': $cookies.get('token') },

                          }
                      }),
            //result detail for specific form
            getFormResult: $resource(appSettings.serverPath + "/api/form/result/:form_id", { form_id: '@id' },
                    {
                        'getFormResult': {
                            method: 'GET',
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
            //delete form
            delete: $resource(appSettings.serverPath + "/api/form/:form_id", { form_id: '@id' },
               {
                   'deleteForm': {
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