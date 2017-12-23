import * as React from "react"
import { Helmet } from "react-helmet"
import { WebApiBase } from "../config/webapiconfig"

export const Page1 = () => (
    <div>
        <Helmet>
            <title>Page 1</title>
        </Helmet>
        <h1>This is page 1</h1>
        <p>Your web-api base address is: <i>{WebApiBase}</i></p>
    </div>
)