import { IMessageState } from "../../states/iMessageState"

export interface IHomeProps
{
    messages: IMessageState[],
    modifiedAt: Date
}