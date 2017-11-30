import * as React from "react"
import { Switch, Route } from "react-router-dom"
import { Helmet } from "react-helmet"
import { Home } from "../components/home"
import { Page1 } from "../components/page1"
import { Picture } from "../components/picture"

export const Main = () =>
(
    <main>
        <Helmet
            titleTemplate="Your site: %s"
            defaultTitle="Your site"/>
        <Switch>
            <Route exact path="/" component={Home}/>
            <Route exact path="/page1" component={Page1}/>
            <Route exact path="/picture" component={Picture}/>
        </Switch>
    </main>
)
