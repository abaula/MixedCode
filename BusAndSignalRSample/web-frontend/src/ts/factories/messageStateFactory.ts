import { IMessageState } from "../states/iMessageState"
import { IMessageRegisteredEvent } from "../model/iMessageRegisteredEvent"
import { IMessageRegisteringEvent } from "../model/iMessageRegisteringEvent"

export const createMessageStateFromRegisteringEvent = (event: IMessageRegisteringEvent) : IMessageState =>
{
    return {
        id: event.messageId,
        message: event.message,
        isProcessing: true,
        error: undefined
    };
}

export const createMessageStatesFromRegisteredEvent = (messages: IMessageState[], event: IMessageRegisteredEvent): IMessageState[] =>
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

