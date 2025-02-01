declare var __CORS_DEV__ : boolean
let webapibase: string

if(__CORS_DEV__)
    webapibase = "http://localhost:62736"
else
    webapibase = "/"

export const webApiBaseUrl: string = webapibase