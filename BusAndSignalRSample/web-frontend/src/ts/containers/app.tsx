import * as React from "react"
import { Provider } from "react-redux"
import store from "../stores/AppStore"
import { Header } from "./header"
import { Footer } from "./footer"
import { Main } from "./main"

export const App = () => (
    <Provider store={store}>
        <div>
            <Header/>
            <Main/>
            <Footer/>
        </div>
    </Provider>
)
