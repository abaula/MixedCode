
export interface ITreeNode
{
    id: number
    parent: ITreeNode
    level: number
    text: string
    children: ITreeNode[]
    hasChildren: boolean
    expanded: boolean
    data: any
}