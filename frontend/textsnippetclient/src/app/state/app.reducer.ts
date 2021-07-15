import { AppAction, AppActionNames, AppFailedAction, AppSuccessAction } from "./app.action";
import { IAppState } from "./app.state";

export const appStateInit: IAppState = {
    token: "",
    roles: [],
    paging: {
        count: 0,
        data: []
    },
    selected: {
        id: 0,
        title: "",
        content: ""
    }
}

export function appReducer(appState: IAppState = appStateInit, action: AppAction): IAppState {
    switch (action.type) {
        case AppActionNames.GET_SELECTED:
            return {
                ...appState,
                selected: action.payload
            }
        case AppActionNames.ACTION_SUCCESS:
            return appSuccessReducer(appState, action as AppSuccessAction);
        case AppActionNames.ACTION_FAILED:
            return appFailedReducer(appState, action as AppFailedAction);
        default:
            return appState;
    }
}


export function appSuccessReducer(appState: IAppState, action: AppSuccessAction): IAppState {
    switch (action.subType) {
        case AppActionNames.LOGIN:
            return {
                ...appState,
                token: action.payload.jwtToken,
                roles: action.payload.roles
            };
        case AppActionNames.GET_PAGINATION:
            return {
                ...appState,
                paging: action.payload
            }
        case AppActionNames.CREATE:
            return {
                ...appState,
                selected: action.payload
            }        
        default:
            return appState;
    }
}

export function appFailedReducer(appState: IAppState, action: AppFailedAction): IAppState {
    switch (action.subType) {
        default:
            return appState;
    }
}