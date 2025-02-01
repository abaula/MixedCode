import { createStore, applyMiddleware } from "redux"
import { reducers } from "../reducers"
import { IRootState } from "../states/iRootState"
import logger from "redux-logger"
import thunk from "redux-thunk"

const middleware = applyMiddleware(thunk, logger)
export const store = createStore<IRootState>(reducers, middleware)