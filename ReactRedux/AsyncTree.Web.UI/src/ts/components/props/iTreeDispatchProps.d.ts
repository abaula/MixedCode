import { ITreeNodeState } from "../../states/iTreeNodeState"

export interface ITreeDispatchProps
{
    loadTree : () => void
    onIconClick: (event: React.MouseEvent<HTMLElement>, nodeId: number, state: TreeNodeStateType) => void
}