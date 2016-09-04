(function () {
    "use strict";

    angular.module("sampleNumberDirectiveApp")
    .controller("ctrl", ["$scope", function ($scope)
    {
        $scope.myNumber = undefined;
        $scope.myNumberIsNumber = function()
        {
            var n = $scope.myNumber;
            return !isNaN(parseFloat(n)) && isFinite(n);
        }
    }]);

})();
