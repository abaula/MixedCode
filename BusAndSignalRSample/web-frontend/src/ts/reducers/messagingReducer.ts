import { IAction } from "../actions/iAction"
import { ActionType } from "../actions/actionType"
import { IMessageState } from "../states/iMessageState"
import { IMessagingState } from "../states/iMessagingState"
import * as MessageStateFactory from "../factories/messageStateFactory"
import { IRegisterMessageCommand } from "../model/iRegisterMessageCommand"
import { IMessageRegisteredEvent } from "../model/iMessageRegisteredEvent";

const initialState : IMessagingState = 
{
    messages: [],
    modifiedAt: new Date()
}

export const messagingReducer = (state: IMessagingState = initialState, action: IAction) : IMessagingState =>
{
    switch (action.type)
    {
        case ActionType.SendMessage:
            return {
                messages: [...state.messages, MessageStateFactory.createMessageStateFromCommand(action.payload as IRegisterMessageCommand) ],
                modifiedAt: new Date()
            }
        
        case ActionType.RecieveMessage:
            return {
                messages: MessageStateFactory.createMessageStatesFromEvent(state.messages, action.payload as IMessageRegisteredEvent),
                modifiedAt: new Date()
            }
        
    }

    return state;
}