(function () {
    "use strict";

    angular.module("simpleApp", []);
    angular.module("simpleApp").controller("ctrl", ["$scope", function ($scope)
    {
        $scope.products = ["Milk", "Bread", "Cheese"];
    }]);

})();


