import IMessagingService from "./iMessagingService"
import { IMessageRegisteredEvent } from "../model/iMessageRegisteredEvent"
import { IMessageRegisteringEvent } from "../model/iMessageRegisteringEvent"
import { IRegisterMessageCommand } from "../model/iRegisterMessageCommand"
import { HubConnection } from "@aspnet/signalr-client"
import { webApiBaseUrl } from "../config/webapiconfig"
import { Dispatch } from "react-redux"
import * as Action from "../actions/actions"

class MessagingServiceImpl implements IMessagingService
{
    private connection: HubConnection = new HubConnection(webApiBaseUrl + "/messaging");

    connect = (): Promise<void> =>
    {
        this.connection.on("messageRegistering", this.onMessageRegistering);
        this.connection.on("messageRegistered", this.onMessageRegistered);
        return this.connection.start();
    }
    registerMessage = (command: IRegisterMessageCommand): Promise<any> =>
    {
        return this.connection.invoke("registerMessage", command);
    }

    onMessageRegistering = (event: IMessageRegisteringEvent): void =>
    {
        Action.onMessageRegistering(event);
    }

    onMessageRegistered = (event: IMessageRegisteredEvent): void =>
    {
        Action.onMessageRegistered(event);
    }
}

export const messagingService = new MessagingServiceImpl() as IMessagingService;