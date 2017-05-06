import * as React from "react"
import Tree from "../components/tree"
import { Header } from "./header"
import { Footer } from "./footer"
import { Provider } from "react-redux"
import { store } from "../stores/TreeStore"

export class App extends React.Component<any, any>
{
    constructor(props? : any)
    {
        super(props)
    }

    render()
    {
        return (
            <Provider store={store}>
                <div>
                    <Header />
                    <Tree />
                    <Footer />
                </div>
            </Provider>
        )
    }
}
