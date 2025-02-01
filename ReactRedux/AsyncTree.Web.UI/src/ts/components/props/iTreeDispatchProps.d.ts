import { ITreeNodeState } from "../../states/iTreeNodeState"

export interface ITreeDispatchProps
{
    loadTree: () => void
    expandTreeNode: (nodeId: number, end: () => void) => void
    collapseTreeNode: (nodeId: number, end: () => void) => void
}