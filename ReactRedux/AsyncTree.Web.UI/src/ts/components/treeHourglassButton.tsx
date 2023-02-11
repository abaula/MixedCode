import * as React from "react"

export const TreeHourglassButton = () =>
{
    return (
        <div className="fa-stack hourglass-spin">
            <i className="fa fa-stack-1x fa-hourglass-start" aria-hidden="true"></i>
            <i className="fa fa-stack-1x fa-hourglass-half" aria-hidden="true"></i>
            <i className="fa fa-stack-1x fa-hourglass-end" aria-hidden="true"></i>
            <i className="fa fa-stack-1x fa-hourglass-end spin" aria-hidden="true"></i>
        </div>
    )
}