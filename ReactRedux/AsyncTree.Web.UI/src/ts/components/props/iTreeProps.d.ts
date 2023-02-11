import { ITreeNodeState } from "../../states/iTreeNodeState"

export interface ITreeProps
{
    nodes: ITreeNodeState[],
    isLoading: boolean,
    error?: string
}