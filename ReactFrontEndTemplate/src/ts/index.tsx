import * as React from "react"
import * as ReactDOM from "react-dom"
import { App } from "./containers/app"
import { BrowserRouter } from "react-router-dom"
import "../css/index.scss"

ReactDOM.render(
    <BrowserRouter>
        <App/>
    </BrowserRouter>,
    document.getElementById("app")
)