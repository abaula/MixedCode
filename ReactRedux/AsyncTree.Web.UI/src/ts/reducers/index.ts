import { IRootState } from "../states/iRootState"
import { treeReducer } from "../reducers/treeReducer"
import { combineReducers } from "redux"

export const reducers = combineReducers<IRootState>({
    tree: treeReducer
})
