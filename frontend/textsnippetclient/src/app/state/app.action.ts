import { Action } from "@ngrx/store";

export enum AppActionNames {
    LOGIN = '[Auth] Login',
    GET_PAGINATION = '[TextSnippet] Get Pagination',
    GET_SELECTED = '[TextSnippet] Get Selected',
    CREATE = '[TextSnippet] Create',
    UPDATE = '[TextSnippet] Update',
    DELETE = '[TextSnippet] Delete',

    ACTION_SUCCESS = '[App] Action sucess',
    ACTION_FAILED = '[App] Action failed'
}

export interface AppAction extends Action {
    type: AppActionNames;
    payload?: any;
}

export class LoginAction implements AppAction {
    type = AppActionNames.LOGIN;
    constructor(public payload: any) { }
}

export class GetPaginationTextSnippetAction implements AppAction {
    type = AppActionNames.GET_PAGINATION;
    constructor(public payload: any) { }
}

export class GetSelectedTextSnippetAction implements AppAction {
    type = AppActionNames.GET_SELECTED;
    constructor(public payload: any) { }
}

export class CreateTextSnippetAction implements AppAction {
    type = AppActionNames.CREATE;
    constructor(public payload: any) { }
}

export class UpdateTextSnippetAction implements AppAction {
    type = AppActionNames.UPDATE;
    constructor(public payload: any) { }
}

export class DeleteTextSnippetAction implements AppAction {
    type = AppActionNames.DELETE;
    constructor(public payload: any) { }
}

export class AppSuccessAction implements AppAction {
    type = AppActionNames.ACTION_SUCCESS;
    constructor(public subType: AppActionNames, public payload: any) { }
}

export class AppFailedAction implements AppAction {
    type = AppActionNames.ACTION_FAILED;
    constructor(public subType: AppActionNames, public payload: any) { }
}