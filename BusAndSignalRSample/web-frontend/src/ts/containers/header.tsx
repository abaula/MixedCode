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
                </ul>
            </nav>
        </div>
    </header>
)
