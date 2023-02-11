import { ITreeNode } from "./iTreeNode"
import * as _ from "lodash"

export class LogicalTree
{
    private index: any
    root: ITreeNode

    constructor()
    {
        this.index = {}
        this.root = { id: 0, parent: null, level: 0, children: [], data: null, hasChildren: true, expanded: false, text: null }
        this.setIndex(this.root)
    }

    findNode = (id: number): ITreeNode =>
    {
        return _.get(this.index, id, undefined)
    }

    setChildren = (children: ITreeNode[], parent: ITreeNode): ITreeNode =>
    {
        this.removeChildren(parent)
        parent.children = children
        _.forEach(parent.children, (child: ITreeNode): void => this.setIndex(child))
        return parent
    }

    removeChildren = (node: ITreeNode): ITreeNode =>
    {
        _.forEach(node.children, (child: ITreeNode): void => this.unsetIndex(child))
        node.children = []
        return node
    }

    getList = (parent: ITreeNode, includeParent: boolean): ITreeNode[] =>
    {
        let list = new Array<ITreeNode>();

        if (includeParent)
            list.push(parent)

        _.forEach(parent.children, (child: ITreeNode): void => this.walkTreeRecurcive(child, list))

        return list;
    }

    private walkTreeRecurcive = (node: ITreeNode, list: ITreeNode[]): void => 
    {
        list.push(node)
        _.forEach(node.children, (child: ITreeNode): void => this.walkTreeRecurcive(child, list))
    }

    private setIndex = (node: ITreeNode): void => 
    {
        _.set(this.index, node.id, node)
    }

    private unsetIndex = (node: ITreeNode): void => 
    {
        _.unset(this.index, node.id)
    }
}