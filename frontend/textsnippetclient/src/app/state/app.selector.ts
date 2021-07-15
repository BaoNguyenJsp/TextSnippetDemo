import { Injectable } from "@angular/core";
import { createFeatureSelector, createSelector, Store } from "@ngrx/store";
import { Observable } from "rxjs";
import { IAppState } from "./app.state";

export const getAppSate = createFeatureSelector<IAppState>('app');

const getToken = createSelector(
    getAppSate,
    (state: IAppState) => state?.token
)

const getRoles = createSelector(
    getAppSate,
    (state: IAppState) => state?.roles
)

const getPaging = createSelector(
    getAppSate,
    (state: IAppState) => state?.paging
)

const getSelected = createSelector(
    getAppSate,
    (state: IAppState) => state?.selected
)

@Injectable()
export class AppSelector {
    public getToken$: Observable<string>;
    public getRoles$: Observable<string[]>;
    public getPaging$: Observable<any>;
    public getSelected$: Observable<any>;

    constructor(private store: Store<any>) {
        this.getToken$ = this.store.select(getToken);
        this.getRoles$ = this.store.select(getRoles);
        this.getPaging$ = this.store.select(getPaging);
        this.getSelected$ = this.store.select(getSelected);
    }
}