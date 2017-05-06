import * as React from "react"

export const TreeLoadingMessage = () =>
{
    return (
        <div id="loadOverlay">
            <div>
                <i className="fa fa-spinner fa-pulse" aria-hidden="true"></i>
                <span>Загрузка...</span>
            </div>
        </div>
    )
}

