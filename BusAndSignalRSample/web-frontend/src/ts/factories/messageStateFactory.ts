import { IMessageState } from "../states/iMessageState"
import { IRegisterMessageCommand } from "../model/iRegisterMessageCommand"
import { IMessageRegisteredEvent } from "../model/iMessageRegisteredEvent"

export const createMessageStateFromCommand = (command: IRegisterMessageCommand) : IMessageState =>
{
    return {
        id: command.messageId,
        message: command.message,
        isProcessing: true,
        error: undefined
    };
}

export const createMessageStatesFromEvent = (messages: IMessageState[], event: IMessageRegisteredEvent): IMessageState[] =>
{
    return messages.map(item => 
    {
        if(item.id === event.messageId)
        {
            return {
                id: item.id,
                message: item.message,
                isProcessing: false,
                error: undefined
            }
        }

        return item;
    });
}

