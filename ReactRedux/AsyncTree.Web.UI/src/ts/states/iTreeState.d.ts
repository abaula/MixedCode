import { ITreeNodeState } from "../states/iTreeNodeState"
import { IPendingTreeNodeOperation } from "../states/iPendingTreeNodeOperation"

export interface ITreeState
{
    nodes: ITreeNodeState[]
    isLoading: boolean
    error?: any
}