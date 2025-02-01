import * as React from "react"
import { ITreeNodeState } from "../states/iTreeNodeState"
import { IRootState } from "../states/iRootState"
import { TreeExpandableButton } from "../components/treeExpandableButton"
import { TreeHourglassButton } from "../components/treeHourglassButton"
import { TreeContentButton } from "../components/treeContentButton"
import { connect, Dispatch } from "react-redux"
import { ITreeProps } from "./props/iTreeProps"
import { ITreeState } from "./states/iTreeState"
import { ITreeDispatchProps } from "./props/iTreeDispatchProps"
import * as Action from "../actions/actions"
import { TreeNodeStateType } from "../states/treeNodeStateType"
import { TreeLoadingMessage } from "../components/treeLoadingMessage"
import * as _ from "lodash"

const mapStateToProps = (state: IRootState): ITreeProps => ({
    nodes: state.tree.nodes,
    isLoading: state.tree.isLoading
})

const mapDispatchToProps = (dispatch: Dispatch<any>): ITreeDispatchProps => ({
    loadTree: () => dispatch(Action.loadTree()),
    expandTreeNode: (nodeId: number, end: () => void) => dispatch(Action.expandTreeNode(nodeId, end)),
    collapseTreeNode: (nodeId: number, end: () => void) => dispatch(Action.collapseTreeNode(nodeId, end))
})

class TreeImpl extends React.Component<(ITreeProps & ITreeDispatchProps), ITreeState>
{
    constructor(props?: any)
    {
        super(props)
        this.state = { pendingNodesMap: {} }
    }

    componentDidMount() : void
    {
        this.props.loadTree()
    }

    render(): JSX.Element
    {
        if (this.props.isLoading)
            return <TreeLoadingMessage />

        return (
            <div className="tree">
                {
                    this.props.nodes.map((value: ITreeNodeState, index: number) =>
                    {
                        let nodeCssClasses = ["treeNode", "treeNodeLevel" + value.level].join(" ")
                        let treeButton = this.getButton(value)

                        return (
                            <div key={value.id} className={nodeCssClasses}>
                                {treeButton}
                                <div className="treeNodeElement">{value.text}</div>
                            </div>
                        )
                    })
                }
            </div>
        )
    }  

    private setPendingNode = (nodeId: number, state: TreeNodeStateType): void =>
    {
        let copy = { ...this.state }
        _.set(copy.pendingNodesMap, nodeId, state)
        this.setState(copy)
    }

    private unsetPendingNode = (nodeId: number): void =>
    {
        let copy = { ...this.state }
        _.unset(copy.pendingNodesMap, nodeId)
        this.setState(copy)
    }

    private hasPendingState = (nodeId: number): boolean =>
    {
        let state = _.get(this.state.pendingNodesMap, nodeId, undefined)

        return state === TreeNodeStateType.Collapsing || state === TreeNodeStateType.Expanding
    }

    private onButtonClick = (event: React.MouseEvent<HTMLElement>, nodeId: number, state: TreeNodeStateType) =>
    {
        if (state == TreeNodeStateType.Expanded)
        {
            this.setPendingNode(nodeId, TreeNodeStateType.Collapsing)
            this.props.collapseTreeNode(nodeId, () => { this.unsetPendingNode(nodeId) })
        }
        else if (state == TreeNodeStateType.Collapsed)
        {
            this.setPendingNode(nodeId, TreeNodeStateType.Expanding)
            this.props.expandTreeNode(nodeId, () => { this.unsetPendingNode(nodeId) })
        }
    }

    private getButton = (value: ITreeNodeState): JSX.Element =>
    {
        if (!value.hasChildren)
            return <TreeContentButton />

        if (this.hasPendingState(value.id))
            return <TreeHourglassButton />

        return <TreeExpandableButton nodeId={value.id} state={value.state} onIconClick={this.onButtonClick} />
    }
}

export default connect<ITreeProps, ITreeDispatchProps, any>(
    mapStateToProps,
    mapDispatchToProps
)(TreeImpl)

