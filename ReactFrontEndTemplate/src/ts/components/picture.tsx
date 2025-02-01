import * as React from "react"
import { Helmet } from "react-helmet"
import { WebApiBase } from "../config/webapiconfig"

const image = require("../../images/what-is-webpack.png")

export const Picture = () => (
    <div>
        <Helmet>
            <title>Picture page</title>
        </Helmet>
        <img alt="The picture" style={{ width: 500 }} src={image} />
        <p>Your web-api base address is: <i>{WebApiBase}</i></p>
    </div>
)