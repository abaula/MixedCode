import { TreeNodeStateType } from "./treeNodeStateType"

export interface IPendingTreeNodeOperation
{
    nodeId: number
    pendingStateType: TreeNodeStateType 
}