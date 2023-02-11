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

export const expandTreeNode = (nodeId: number, end: () => void): (dispatch: Dispatch<IAction>) => void =>
    ((dispatch: Dispatch<IAction>): void =>
    {
        treeService.expandNodeAndGetTree(nodeId)
            .then((nodes: ITreeNodeState[]) =>
            {
                if (nodes)
                    dispatch({ type: ActionType.ExpandTreeNode, payload: nodes })

                end()
            })
            .catch((reason: any) =>
            {
                dispatch({ type: ActionType.ExpandTreeNodeError, payload: reason })
                end()
            })
    })

export const collapseTreeNode = (nodeId: number, end: () => void): (dispatch: Dispatch<IAction>) => void =>
    ((dispatch: Dispatch<IAction>): void =>
    {
        treeService.collapseNodeAndGetTree(nodeId)
            .then((nodes: ITreeNodeState[]) =>
            {
                if (nodes)
                    dispatch({ type: ActionType.CollapseTreeNode, payload: nodes })

                end()
            })
            .catch((reason: any) =>
            {
                dispatch({ type: ActionType.CollapseTreeNodeError, payload: reason })
                end()
            })
    })
