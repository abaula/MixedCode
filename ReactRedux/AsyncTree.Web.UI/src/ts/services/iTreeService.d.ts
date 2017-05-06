import { ITreeNodeState } from "../states/iTreeNodeState"
import { TreeNodeStateType } from "../states/treeNodeStateType"

export interface ITreeService
{
    expandNodeAndGetTree(parentId: number): Promise<ITreeNodeState[]>
    collapseNodeAndGetTree(nodeId: number): Promise<ITreeNodeState[]>
}