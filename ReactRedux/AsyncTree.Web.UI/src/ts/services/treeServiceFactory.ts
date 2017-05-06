import TreeService from "./treeService"
import { ITreeService } from "./iTreeService"
import TreeStorage from "../dal/treeStorage"

export const createTreeService = (): ITreeService =>
{
    let storage = new TreeStorage()
    return new TreeService(storage)
}
