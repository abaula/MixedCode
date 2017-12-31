import { IAction } from "../actions/iAction"
import { ActionType } from "../actions/actionType"
import { Dispatch } from "react-redux"
import { IRegisterMessageCommand } from "../model/iRegisterMessageCommand"
import { IMessageRegisteredEvent } from "../model/iMessageRegisteredEvent"
import * as Uuid from "../helpers/uuid"
import store from "../stores/AppStore"
import { messagingService } from "../services/messagingService"

export const sendMessage = (message: string): (dispatch: Dispatch<IAction>) => void =>
    ((dispatch: Dispatch<IAction>): void =>
    {
        let command: IRegisterMessageCommand = {
            messageId: Uuid.uuidv4(),
            message: message
        };

        // dispatch command to state
        dispatch(
            { 
                type: ActionType.SendMessage,
                payload: command
            }
        )

        // send command
        messagingService.registerMessage(command)
        .then()
        .catch(reason => console.log(reason));
    });

export const onMessageRegistered = (event: IMessageRegisteredEvent): void =>
{
    store.dispatch(
    {
        type: ActionType.RecieveMessage,
        payload: event
    });
}