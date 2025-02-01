import { ITreeNodeDto } from "./iTreeNodeDto"

export interface ITreeStorage
{
    getChildren(parentId: number): Promise<ITreeNodeDto[]>
}