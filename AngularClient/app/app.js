(function () {
    "use strict";
    
    var userManagementModule = angular.module("userManagement", ["common.services", "ngCookies"]);
    var formManagementModule=angular.module("formManagement", ["common.services"]);
    var categoryManagementModule = angular.module("categoryManagement", ["common.services"]);

    var app = angular.module("app", ["userManagement", "formManagement", "categoryManagement"]);
   
      
       

}());
