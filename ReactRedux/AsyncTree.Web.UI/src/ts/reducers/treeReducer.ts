import { IAction } from "../actions/iAction"
import { ActionType } from "../actions/actionType"
import { ITreeNodeState } from "../states/iTreeNodeState"
import { ITreeNodeStatePayload } from "../states/iTreeNodeStatePayload"
import { ITreeState } from "../states/iTreeState"
import { TreeNodeStateType } from "../states/treeNodeStateType"
import { IPendingTreeNodeOperation } from "../states/iPendingTreeNodeOperation"
import * as _ from "lodash"

const initialState: ITreeState =
    {
        nodes: new Array<ITreeNodeState>(),
        isLoading: false,
        error: undefined,
        pendingTreeNodesOperationsMap: {}
    }

const copyNodesWithUpdatingStateType = (nodes: ITreeNodeState[], pendingOperationsMap: any): ITreeNodeState[] =>
{
    return nodes.map((node: ITreeNodeState): ITreeNodeState =>
    {
        let copy = { ...node }
        let pendingState = _.get(pendingOperationsMap, copy.id, undefined)

        if (pendingState !== undefined)
            copy.state = pendingState

        return copy
    })
}

const copyPendingOperationsWithAddOperation = (state: ITreeState, nodeId: number, stateType: TreeNodeStateType): any =>
{
    let copy = { ...state.pendingTreeNodesOperationsMap }
    return _.set(copy, nodeId, stateType)
}

const copyPendingOperationsWithRemoveOperation = (state: ITreeState, nodeId: number): any =>
{
    let copy = { ...state.pendingTreeNodesOperationsMap }
    _.unset(copy, nodeId)
    return copy
}

const copyState = (sourceNodes: ITreeNodeState[], newPendingOperationsMap: any, isLoading: boolean, error: any): ITreeState =>
{
    return {
        nodes: copyNodesWithUpdatingStateType(sourceNodes, newPendingOperationsMap),
        isLoading: isLoading,
        error: error,
        pendingTreeNodesOperationsMap: newPendingOperationsMap
    }
}

export const treeReducer = (state: ITreeState = initialState, action: IAction): ITreeState =>
{
    switch (action.type)
    {
        case ActionType.LoadingTree:
            return {
                nodes: [...state.nodes],
                isLoading: true,
                pendingTreeNodesOperationsMap: { ...state.pendingTreeNodesOperationsMap }
            }

        case ActionType.LoadTree:
            return {
                nodes: [...(action.payload as ITreeNodeState[])],
                isLoading: false,
                pendingTreeNodesOperationsMap: { ...state.pendingTreeNodesOperationsMap }
            }

        case ActionType.LoadTreeError:
            return {
                nodes: [...state.nodes],
                isLoading: false,
                error: action.payload,
                pendingTreeNodesOperationsMap: { ...state.pendingTreeNodesOperationsMap }
            }

        case ActionType.ClearTree:
            return {
                nodes: [],
                isLoading: false,
                pendingTreeNodesOperationsMap: {}
            }

        case ActionType.ExpandingTreeNode:
            return copyState(state.nodes,
                copyPendingOperationsWithAddOperation(state, action.payload as number, TreeNodeStateType.Expanding),
                false,
                undefined)

        case ActionType.ExpandTreeNode:
            {
                let payload = action.payload as ITreeNodeStatePayload

                return copyState(payload.nodes,
                    copyPendingOperationsWithRemoveOperation(state, payload.nodeId),
                    false,
                    undefined)
            }
        case ActionType.CollapsingTreeNode:
            return copyState(state.nodes,
                copyPendingOperationsWithAddOperation(state, action.payload as number, TreeNodeStateType.Collapsing),
                false,
                undefined)

        case ActionType.CollapseTreeNode:
            {
                let payload = action.payload as ITreeNodeStatePayload

                return copyState(payload.nodes,
                    copyPendingOperationsWithRemoveOperation(state, payload.nodeId),
                    false,
                    undefined)
            }
    }

    return state
}