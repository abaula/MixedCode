import { ActionType } from "./actionType"

export interface IAction
{
    type: ActionType
    payload?: any
}