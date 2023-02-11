import { IAction } from "../actions/iAction"
import { ActionType } from "../actions/actionType"
import { ITreeNodeState } from "../states/iTreeNodeState"
import { ITreeState } from "../states/iTreeState"

const initialState: ITreeState =
    {
        nodes: new Array<ITreeNodeState>(),
        isLoading: false,
        error: undefined
    }

export const treeReducer = (state: ITreeState = initialState, action: IAction): ITreeState =>
{
    switch (action.type)
    {
        case ActionType.LoadingTree:
            return {
                nodes: [...state.nodes],
                isLoading: true
            }

        case ActionType.LoadTree:
            return {
                nodes: [...(action.payload as ITreeNodeState[])],
                isLoading: false
            }

        case ActionType.LoadTreeError:
            return {
                nodes: [...state.nodes],
                isLoading: false,
                error: action.payload
            }

        case ActionType.ClearTree:
            return {
                nodes: [],
                isLoading: false
            }

        case ActionType.ExpandTreeNode:
            return {
                nodes: [...(action.payload as ITreeNodeState[])],
                isLoading: false
            }

        case ActionType.CollapseTreeNode:
            return {
                nodes: [...(action.payload as ITreeNodeState[])],
                isLoading: false
            }
    }

    return state
}