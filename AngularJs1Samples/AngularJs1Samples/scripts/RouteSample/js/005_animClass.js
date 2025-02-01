(function(app)
{
    "use strict";
    app.directive("animClass", ["$route", "$log", "animConfig", function ($route, $log, animConfig)
    {
        return {

            link: function(scope, elm, attrs)
            {
                function hasAnyAnimateClass(elm)
                {
                    for (let prop in animConfig)
                    {
                        if (!animConfig.hasOwnProperty(prop))
                            continue;

                        if (elm.hasClass(animConfig[prop]))
                            return true;
                    }

                    return false;
                }

                $log.info("on Link, elm has classes: " + elm.attr("class"));
                $log.info("on Link, $route.current.animateToShow = \"" + $route.current.animateToShow + "\"");
                $log.info("on Link, call elm.addClass(\"" + $route.current.animateToShow + "\")");

                var enterClass = $route.current.animateToShow;

                // Если элемент загружается 1-й раз, то класс анимации не добавляем.
                // Событие загрузки элемента отмеченного ng-View отслеживается в родительском контроллере, чей $scope мы наследуем по умолчанию.
                if (scope.viewLoaded)
                    elm.addClass(enterClass);

                scope.$on('$destroy', function()
                {
                    $log.info("on $destroy, elm has classes: " + elm.attr("class"));
                    $log.info("on $destroy, call elm.removeClass(\"" + enterClass + "\")");
                    $log.info("on $destroy, $route.current.animateToShow = \"" + $route.current.animateToShow + "\"");
                    $log.info("on $destroy, call elm.addClass(\"" + $route.current.animateToShow + "\")");

                    elm.removeClass(enterClass);
                    elm.addClass($route.current.animateToShow);
                });
            }
        }
    }]);
})(angular.module("routedApp"));