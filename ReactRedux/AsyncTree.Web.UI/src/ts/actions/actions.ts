import { IAction } from "../actions/iAction"
import { ActionType } from "../actions/actionType"
import { Dispatch } from "react-redux"
import TreeService from "../services/treeService"
import { createTreeService } from "../services/treeServiceFactory"
import { ITreeService } from "../services/iTreeService"
import { ITreeNodeState } from "../states/iTreeNodeState"

var treeService: ITreeService = null

export const loadTree = (): (dispatch: Dispatch<IAction>) => void =>
    ((dispatch: Dispatch<IAction>): void =>
    {
        dispatch({
            type: ActionType.LoadingTree
        })

        treeService = createTreeService()
        treeService.expandNodeAndGetTree(0)
            .then((nodes: ITreeNodeState[]) => dispatch(
                {
                    type: ActionType.LoadTree,
                    payload: nodes
                }))
            .catch((reason: any) => dispatch(
                {
                    type: ActionType.LoadTreeError,
                    payload: reason
                }))
    })

export const clearTree = (): (dispatch: Dispatch<IAction>) => void =>
    ((dispatch: Dispatch<IAction>): void =>
    {
        treeService = null
        dispatch({ type: ActionType.ClearTree })
    })

export const expandTreeNode = (nodeId: number): (dispatch: Dispatch<IAction>) => void =>
    ((dispatch: Dispatch<IAction>): void =>
    {
        dispatch({
            type: ActionType.ExpandingTreeNode,
            payload: nodeId
        })

        treeService.expandNodeAndGetTree(nodeId)
            .then((nodes: ITreeNodeState[]) => dispatch(
                {
                    type: ActionType.ExpandTreeNode,
                    payload: { nodeId: nodeId, nodes: nodes }
                }))
            .catch((reason: any) => dispatch(
                {
                    type: ActionType.ExpandTreeNodeError,
                    payload: reason
                }))
    })

export const collapseTreeNode = (nodeId: number): (dispatch: Dispatch<IAction>) => void =>
    ((dispatch: Dispatch<IAction>): void =>
    {
        dispatch({
            type: ActionType.CollapsingTreeNode,
            payload: nodeId
        })

        treeService.collapseNodeAndGetTree(nodeId)
            .then((nodes: ITreeNodeState[]) => dispatch(
                {
                    type: ActionType.CollapseTreeNode,
                    payload: { nodeId: nodeId, nodes: nodes }
                }))
            .catch((reason: any) => dispatch(
                {
                    type: ActionType.CollapseTreeNodeError,
                    payload: reason
                }))
    })
