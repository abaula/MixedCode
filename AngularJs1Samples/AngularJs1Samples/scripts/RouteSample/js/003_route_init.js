(function(app)
{
    "use strict";
    app.config(["$routeProvider", "$locationProvider", "animConfig", function ($routeProvider, $locationProvider, animConfig)
    {
        $routeProvider
            .when("/",
                {
                    templateUrl: "defaultPage.html",
                    controller: "defaultPageController",
                    animateToShow: animConfig.animateRight
                })
            .when("/page1",
                {
                    templateUrl: "page1.html",
                    controller: "page1Controller",
                    animateToShow: animConfig.animateLeft
                })
            .otherwise({ redirectTo: "/" });

        // Отключаем для работы с локальным файлом. Если включено, то выдаёт ошибку CORS.
        $locationProvider.html5Mode(false);
    }]);
})(angular.module("routedApp"));