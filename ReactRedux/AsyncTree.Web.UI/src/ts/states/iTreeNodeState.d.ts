import { TreeNodeStateType } from "./treeNodeStateType"

export interface ITreeNodeState
{
    id: number
    parentId: number
    text: string
    state: TreeNodeStateType
    level: number
    hasChildren: boolean
}