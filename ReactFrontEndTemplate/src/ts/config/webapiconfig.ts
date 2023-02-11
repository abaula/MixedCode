declare var __CORS_DEV__ : boolean
declare var __CORS_TEST__ : boolean
declare var __CORS_PRE__ : boolean
let webapibase: string

if(__CORS_DEV__)
    webapibase = "http://your-dev-server:8080/_api"
else if(__CORS_TEST__)
    webapibase = "http://your-test-server:8080/_api"
else if(__CORS_PRE__)
    webapibase = "http://your-pre-server:8080/_api"
else
    webapibase = "/_api"

export const WebApiBase: string = webapibase