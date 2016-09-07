(function(ng, app)
{
    "use strict";
    app.factory("notifyService", function()
    {
        var eventHandlers = [];

        return {
            fireTextChanged: function(text)
            {
                ng.forEach(eventHandlers, function(handler)
                {
                    handler(text);
                });
            },
            subscribeOnTextChanged: function(handler)
            {
                eventHandlers.push(handler);
            }
        };
    });
})(angular, angular.module("communicateBtwServicesApp"));