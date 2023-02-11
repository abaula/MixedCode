import { IMessageRegisteredEvent } from "../model/iMessageRegisteredEvent"
import { IRegisterMessageCommand } from "../model/iRegisterMessageCommand"

export default interface IMessagingService
{
    connect(): Promise<void>
    registerMessage(command: IRegisterMessageCommand): Promise<any>
}