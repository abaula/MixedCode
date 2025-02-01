(function(ng, app)
{
    "use strict";
    app.service("notifyService", function()
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