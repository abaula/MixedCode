import { IMessageState } from "./iMessageState"

export interface IMessagingState
{
    messages: IMessageState[],
    modifiedAt: Date
}