import { ITreeService } from "./iTreeService"
import { ITreeNodeState } from "../states/iTreeNodeState"
import { TreeNodeStateType } from "../states/treeNodeStateType"
import { LogicalTree } from "../model/logicalTree"
import { ITreeNode } from "../model/iTreeNode"
import { ITreeStorage } from "../dal/iTreeStorage"
import { ITreeNodeDto } from "../dal/iTreeNodeDto"

export default class TreeService implements ITreeService
{
    private logicalTree: LogicalTree
    private treeStorage: ITreeStorage

    constructor(storage: ITreeStorage)
    {
        this.treeStorage = storage
        this.logicalTree = new LogicalTree()
    }

    expandNodeAndGetTree = (parentId: number): Promise<ITreeNodeState[]> =>
    {
        return new Promise<ITreeNodeState[]>((resolve: (value: ITreeNodeState[]) => void, reject: (reason?: any) => void): void =>
        {
            let parent = this.logicalTree.findNode(parentId)

            if (!parent)
            {
                reject("Узел (id = " + parentId + ") не найден")
                return
            }

            this.treeStorage.getChildren(parent.id)
                .then((data: ITreeNodeDto[]) =>
                {
                    // Оптимизация - если оказалось, что после загрузки данных с сервера родительский узел уже удалён из дерева,
                    // то отдаём вызывающей стороне undefined.
                    if (!this.logicalTree.findNode(parent.id))
                    {
                        resolve(undefined)
                        return
                    }

                    let values = data.map((value: ITreeNodeDto): ITreeNode => this.toITreeNode(value, parent))
                    this.logicalTree.setChildren(values, parent)
                    parent.expanded = true
                    let treeNodesList = this.logicalTree.getList(this.logicalTree.root, false)
                    let nodes = treeNodesList.map((value: ITreeNode): ITreeNodeState => this.toITreeNodeState(value))
                    resolve(nodes)
                })
                .catch((reason: any) => reject(reason))
        })
    }

    collapseNodeAndGetTree = (nodeId: number): Promise<ITreeNodeState[]> =>
    {
        return new Promise<ITreeNodeState[]>((resolve: (value: ITreeNodeState[]) => void, reject: (reason?: any) => void): void =>
        {
            let node = this.logicalTree.findNode(nodeId)

            if (!node)
            {
                reject("Узел (id = " + nodeId + ") не найден")
                return
            }

            this.logicalTree.removeChildren(node)
            node.expanded = false
            let treeNodesList = this.logicalTree.getList(this.logicalTree.root, false)
            let nodes = treeNodesList.map((value: ITreeNode): ITreeNodeState => this.toITreeNodeState(value))
            resolve(nodes)
        })
    }

    private toITreeNodeState = (value: ITreeNode): ITreeNodeState =>
    {
        return {
            id: value.id,
            parentId: value.parent ? value.parent.id : 0,
            state: value.expanded ? TreeNodeStateType.Expanded : TreeNodeStateType.Collapsed,
            hasChildren: value.hasChildren,
            level: value.level,
            text: value.text
        }
    }

    private toITreeNode = (value: ITreeNodeDto, parent: ITreeNode): ITreeNode =>
    {
        return {
            id: value.id,
            parent: parent,
            children: [],
            data: value,
            expanded: false,
            hasChildren: value.hasChildren,
            level: value.level,
            text: value.text
        }
    }
}