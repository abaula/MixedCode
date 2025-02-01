import * as React from "react"
import { ITreeExpandableButtonProps } from "./props/iTreeExpandableButtonProps"
import { TreeNodeStateType } from "../states/treeNodeStateType"

const getIconCssClass = (state: TreeNodeStateType): string =>
{
    switch (state)
    {
        case TreeNodeStateType.Collapsed:
            return "fa fa-stack-1x fa-plus-square-o"
        case TreeNodeStateType.Expanded:
            return "fa fa-stack-1x fa-minus-square-o"
        default: return "fa fa-stack-1x fa-lock"
    }    
}

export const TreeExpandableButton = ({ nodeId, onIconClick, state }: ITreeExpandableButtonProps) =>
{
    let iconCssClass = getIconCssClass(state)

    return (
        <div className="clickableTreeNodeButton fa-stack">
            <i className={iconCssClass} aria-hidden="true" onClick={(e) => onIconClick(e, nodeId, state)}></i>
        </div>
    )
}
