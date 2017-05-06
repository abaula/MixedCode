import * as React from "react"
import { ITreeNodeState } from "../states/iTreeNodeState"
import { IRootState } from "../states/iRootState"
import { TreeExpandableButton } from "../components/treeExpandableButton"
import { TreeHourglassButton } from "../components/treeHourglassButton"
import { TreeContentButton } from "../components/treeContentButton"
import { connect, Dispatch } from "react-redux"
import { ITreeProps } from "./props/iTreeProps"
import { ITreeDispatchProps } from "./props/iTreeDispatchProps"
import * as Action from "../actions/actions"
import { TreeNodeStateType } from "../states/treeNodeStateType"
import { TreeLoadingMessage } from "../components/treeLoadingMessage"

const mapStateToProps = (state: IRootState): ITreeProps => ({
    nodes: state.tree.nodes,
    isLoading: state.tree.isLoading
})

const mapDispatchToProps = (dispatch: Dispatch<any>): ITreeDispatchProps => ({
    loadTree: () => dispatch(Action.loadTree()),
    onIconClick: (event: React.MouseEvent<HTMLElement>, nodeId: number, state: TreeNodeStateType) =>
    {
        if (state == TreeNodeStateType.Expanded)
            dispatch(Action.collapseTreeNode(nodeId))    
        else if (state == TreeNodeStateType.Collapsed)
            dispatch(Action.expandTreeNode(nodeId))    
    }        
})

class TreeImpl extends React.Component<(ITreeProps & ITreeDispatchProps), any>
{
    constructor(props?: any)
    {
        super(props)
    }

    componentDidMount() : void
    {
        this.props.loadTree()
    }

    render()
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

    private getButton = (value: ITreeNodeState): JSX.Element =>
    {
        if (!value.hasChildren)
            return <TreeContentButton />

        if (value.state == TreeNodeStateType.Collapsing || value.state == TreeNodeStateType.Expanding)
            return <TreeHourglassButton />

        return <TreeExpandableButton nodeId={value.id} state={value.state} onIconClick={this.props.onIconClick} />
    }
}

export default connect<ITreeProps, ITreeDispatchProps, any>(
    mapStateToProps,
    mapDispatchToProps
)(TreeImpl)

