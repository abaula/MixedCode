import * as React from "react"
import { Helmet } from "react-helmet"
import { webApiBaseUrl } from "../config/webapiconfig"
import { connect, Dispatch } from "react-redux"
import * as Action from "../actions/actions"
import { IHomeProps } from "./props/iHomeProps"
import { IHomeDispatchProps } from "./props/IHomeDispatchProps"
import { IRootState } from "../states/iRootState"
import { IMessagingState } from "../states/iMessagingState"
import { IMessageState } from "../states/iMessageState"
import IMessagingService from "../services/iMessagingService"
import { messagingService } from "../services/messagingService"
import IHomeState from "./states/iHomeState"

const mapStateToProps = (state: IRootState): IHomeProps => ({
    messages: state.messaging.messages,
    modifiedAt: state.messaging.modifiedAt
})

const mapDispatchToProps = (dispatch: Dispatch<any>): IHomeDispatchProps => ({
    sendMessage: (message: string) => dispatch(Action.sendMessage(message))
})

const defaultState: IHomeState = {
    message: "",
    modifiedAt: new Date()
}

class HomeImpl extends React.Component<(IHomeProps & IHomeDispatchProps), IHomeState>
{
    constructor(props?: any)
    {
        super(props);
        this.state = defaultState;
    }

    componentDidMount() : void
    {
        messagingService.connect()
            .then(() => console.log("Connection started!"))
            .catch(err => console.log("Error while establishing connection :("));
    }

    shouldComponentUpdate(nextProps: IHomeProps, nextState: IHomeState): boolean
    {
        if(this.props.modifiedAt === nextProps.modifiedAt
            && this.state.modifiedAt === nextState.modifiedAt)
            return false;

        return true;
    }

    handleMessageInputChange = (event: any): void =>
    {
        const target = event.target;
        this.setState(
        { 
            message: target.value,
            modifiedAt: new Date()
        });
    }

    handleSubmit = (event: any): void =>
    {
        this.props.sendMessage(this.state.message);
        event.preventDefault();
    }

    getMessageItem = (item: IMessageState): JSX.Element =>
    {
        const className = item.isProcessing ? "icon fa fa-spinner fa-pulse fa-fw" : "icon fa fa-check-square";

        return (
            <div key={item.id}>
                <i className={className}></i>
                ({item.id}) {item.message}
            </div>
        )
    }

    render(): JSX.Element
    {
        return (
            <div>
                <Helmet>
                    <title>SignalR form</title>
                </Helmet>
                
                <h1>Asynchronous messaging with microservices</h1>
                <div>
                    <form onSubmit={this.handleSubmit}>
                        <input 
                            type="text"
                            value={this.state.message}
                            onChange={this.handleMessageInputChange} 
                            />
                        <input type="submit" value="Submit" />
                    </form>
                </div>
                <div>
                    { this.props.messages.map(item => this.getMessageItem(item)) }
                </div>
                <p>Your web-api base address is: <i>{webApiBaseUrl}</i></p>
            </div>)
    }
}

export default connect<IHomeProps, IHomeDispatchProps, any>(
    mapStateToProps,
    mapDispatchToProps
)(HomeImpl)