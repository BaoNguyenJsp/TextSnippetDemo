import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { AppService } from "app/app.service";
import { EMPTY } from "rxjs";
import { catchError, map, mergeMap, switchMap, withLatestFrom } from "rxjs/operators";
import { AppActionNames, AppSuccessAction, CreateTextSnippetAction, DeleteTextSnippetAction, GetPaginationTextSnippetAction, LoginAction, UpdateTextSnippetAction } from "./app.action";
import { AppSelector } from "./app.selector";
import { Dispatcher } from "./dispatcher";

@Injectable()
export class AppEffect {
    constructor(
        private actions$: Actions,
        private appService: AppService,
        private appSelector: AppSelector,
        private dispatcher: Dispatcher
    ) { }

    login$ = createEffect(() => this.actions$.pipe(
        ofType(AppActionNames.LOGIN),
        switchMap((action: LoginAction) => this.appService.login(action.payload)
          .pipe(
            map(loginResult => new AppSuccessAction(action.type, loginResult)),
            catchError(() => EMPTY)
          ))
        )
      );

      getPagination$ = createEffect(() => this.actions$.pipe(
        ofType(AppActionNames.GET_PAGINATION),
        switchMap((action: GetPaginationTextSnippetAction) => this.appService.getPagination(action.payload.query, action.payload.pageSize, action.payload.pageNumber)
          .pipe(
            map(data  => {
              var result = { ...data, paging: action.payload };
              return new AppSuccessAction(action.type, result)
            }),
            catchError(() => EMPTY)
          ))
        )
      );

      create$ = createEffect(() => this.actions$.pipe(
        ofType(AppActionNames.CREATE),
        mergeMap((action: CreateTextSnippetAction) => this.appService.create(action.payload)
          .pipe(
            map(data => {
              var paging = {
                query: '',
                pageSize: 5,
                pageNumber: 0
              };
              this.dispatcher.fire(new GetPaginationTextSnippetAction(paging));
              return new AppSuccessAction(action.type, data)
            }),
            catchError(() => EMPTY)
          ))
        )
      );

      update$ = createEffect(() => this.actions$.pipe(
        ofType(AppActionNames.UPDATE),
        mergeMap((action: UpdateTextSnippetAction) => this.appService.update(action.payload.id, action.payload.data)
          .pipe(
            withLatestFrom(this.appSelector.getPaging$),
            map(([data, state]) => {
              var paging = {
                query: state.paging.query,
                pageSize: state.paging.pageSize,
                pageNumber: state.paging.pageNumber
              };
              this.dispatcher.fire(new GetPaginationTextSnippetAction(paging));
              return new AppSuccessAction(action.type, data)
            }),
            catchError(() => EMPTY)
          ))
        )
      );

      delete$ = createEffect(() => this.actions$.pipe(
        ofType(AppActionNames.DELETE),
        mergeMap((action: DeleteTextSnippetAction) => this.appService.delete(action.payload)
          .pipe(
            map(data => {
              var paging = {
                query: '',
                pageSize: 5,
                pageNumber: 0
              };
              this.dispatcher.fire(new GetPaginationTextSnippetAction(paging));
              return new AppSuccessAction(action.type, data)
            }),
            catchError(() => EMPTY)
          ))
        )
      );
}