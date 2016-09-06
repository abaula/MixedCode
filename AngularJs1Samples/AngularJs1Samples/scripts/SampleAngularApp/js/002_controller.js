(function (app) {
    "use strict";

    app.controller("ctrl", ["$scope", function ($scope)
    {
        $scope.products = ["Milk", "Bread", "Cheese"];
    }]);

})(angular.module("simpleApp"));
