import * as React from "react"
import { Helmet } from "react-helmet"
import { WebApiBase } from "../config/webapiconfig"

export const Home = () => (
    <div>
        <Helmet>
            <title>Home page</title>
        </Helmet>
        <h1>This is homepage</h1>
        <p>Your web-api base address is: <i>{WebApiBase}</i></p>
    </div>
)