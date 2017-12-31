import * as React from "react"
import { Helmet } from "react-helmet"
import { webApiBaseUrl } from "../config/webapiconfig"
import { connect, Dispatch } from "react-redux"
import * as Action from "../actions/actions"
import { IHomeProps } from "./props/iHomeProps"
import { IHomeDispatchProps } from "./props/IHomeDispatchProps"
import { IRootState } from "../states/iRootState"
import { IMessagingState } from "../states/iMessagingState"
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
    message: ""
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

    shouldComponentUpdate(nextProps: IHomeProps): boolean
    {
        //if(this.props.modifiedAt === nextProps.modifiedAt)
        //    return false;

        return true;
    }

    handleMessageInputChange = (event: any): void =>
    {
        const target = event.target;
        this.setState({ message: target.value });
    }

    handleSubmit = (event: any): void =>
    {
        this.props.sendMessage(this.state.message);
        event.preventDefault();
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
                    { 
                        this.props.messages.map(item => 
                            {
                                if(item.isProcessing)
                                    return (
                                    <div 
                                        key={item.id}
                                        style={{ backgroundColor: "#f0f0f0" }}>
                                        <b>id:</b>{item.id} - <b>message:</b> { item.message }
                                    </div>
                                    )
                                
                                return (
                                    <div key={item.id}><b>id:</b>{item.id} - <b>message:</b> { item.message }</div>
                                )
                            }) 
                    }
                </div>
                <p>Your web-api base address is: <i>{webApiBaseUrl}</i></p>
            </div>)
    }
}

export default connect<IHomeProps, IHomeDispatchProps, any>(
    mapStateToProps,
    mapDispatchToProps
)(HomeImpl)