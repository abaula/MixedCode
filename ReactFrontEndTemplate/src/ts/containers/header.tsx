import * as React from "react"
import { Link } from "react-router-dom"

export const Header = () =>
(
    <header>
        <div>
            <div>Header</div>
            <nav>
                <ul>
                    <li><Link to='/'>Home</Link></li>
                    <li><Link to='/page1'>Page 1</Link></li>
                    <li><Link to='/picture'>Picture</Link></li>
                </ul>
            </nav>
        </div>
    </header>
)
