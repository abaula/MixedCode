import * as React from "react"
import { Switch, Route } from "react-router-dom"
import { Helmet } from "react-helmet"
import Home from "../components/home"

export const Main = () =>
(
    <main>
        <Helmet
            titleTemplate="Bus&amp;SignalRSample: %s"
            defaultTitle="Bus&amp;SignalRSample"/>
        <Switch>
            <Route exact path="/" component={Home}/>
        </Switch>
    </main>
)
