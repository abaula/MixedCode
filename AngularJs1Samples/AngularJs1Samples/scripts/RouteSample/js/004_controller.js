(function (ng, app) {
    "use strict";
    app.controller("ctrl", ["$scope", "$log", function ($scope, $log)
    {
        $scope.explain1 = "Роутинг \"локально\" работает только в Firefox.";
        $scope.explain2 = "Crome выдаёт ошибку - Cross origin requests are only supported for protocol schemes: http, data, chrome, chrome-extension, https, chrome-extension-resource.";
        $scope.explain3 = "Edge выдаёт ошибку - Не найден указанный файл.";
        $scope.viewLoaded = false;

        $scope.$on("$routeChangeStart", function (event, next, current)
        {
            $log.info("$routeChangeStart: currentController - " + (ng.isUndefined(current) ? "undfefined" : current.controller) + ", nextController - " + next.controller);
        });

        $scope.$on("$routeChangeSuccess", function (event, current, previous)
        {
            $log.info("$routeChangeSuccess: currentController - " + current.controller + ", previousController - " + (ng.isUndefined(previous) ? "undefined" : previous.controller));
        });

        $scope.$on("$viewContentLoaded", function ()
        {
            $log.info("$viewContentLoaded");
            $scope.viewLoaded = true;
        });

        $scope.$on("$locationChangeStart", function (event, newUrl)
        {
            $log.info("$locationChangeStart: newUrl - " + newUrl);
        });

        $scope.$on("$locationChangeSuccess", function (event, newUrl)
        {
            $log.info("$locationChangeSuccess: newUrl - " + newUrl);
        });

    }]);
})(angular, angular.module("routedApp"));
