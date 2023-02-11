import { IAction } from "../actions/iAction"
import { ActionType } from "../actions/actionType"
import { IMessageState } from "../states/iMessageState"
import { IMessagingState } from "../states/iMessagingState"
import * as MessageStateFactory from "../factories/messageStateFactory"
import { IMessageRegisteredEvent } from "../model/iMessageRegisteredEvent"
import { IMessageRegisteringEvent } from "../model/iMessageRegisteringEvent"

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
                messages: [...state.messages, MessageStateFactory.createMessageStateFromRegisteringEvent(action.payload as IMessageRegisteringEvent) ],
                modifiedAt: new Date()
            }
        
        case ActionType.RecieveMessage:
            return {
                messages: MessageStateFactory.createMessageStatesFromRegisteredEvent(state.messages, action.payload as IMessageRegisteredEvent),
                modifiedAt: new Date()
            }   
    }

    return state;
}