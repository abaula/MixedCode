import * as React from "react"
import { TreeNodeStateType } from "../states/treeNodeStateType"

export interface ITreeExpandableButtonProps
{
    nodeId: number
    state: TreeNodeStateType
    onIconClick: (event: React.MouseEvent<HTMLElement>, nodeId: number, state: TreeNodeStateType) => void
}