(function () {
    "use strict";
    angular
        .module("common.services")
        .factory("categoryResource", ["$resource", "appSettings", categoryResource])

    function categoryResource($resource, appSettings) {
        return $resource(appSettings.serverPath + "/api/category");
    }

}());