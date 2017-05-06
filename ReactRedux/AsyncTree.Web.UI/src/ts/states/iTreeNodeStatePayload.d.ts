import { ITreeNodeState } from "./iTreeNodeState"

export interface ITreeNodeStatePayload
{
    nodeId: number
    nodes: ITreeNodeState[]
}