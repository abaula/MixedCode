(function(app)
{
    "use strict";
    app.controller("defaultPageController", ["$scope",
        function ($scope)
        {
            $scope.name = "Антон";
        }]);
})(angular.module("routedApp"));