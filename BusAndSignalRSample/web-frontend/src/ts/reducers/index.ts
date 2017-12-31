import { combineReducers } from "redux"
import { IRootState } from "../states/iRootState"
import { messagingReducer } from "../reducers/messagingReducer"

export const reducers = combineReducers<IRootState>({
    messaging: messagingReducer
})

