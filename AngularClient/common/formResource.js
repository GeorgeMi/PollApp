(function () {
    "use strict";
    angular
        .module("common.services")
        .factory("formResource", ["$resource", "appSettings", formResource])

    function formResource($resource, appSettings) {
        return $resource(appSettings.serverPath + "/api/form");
    }

}());