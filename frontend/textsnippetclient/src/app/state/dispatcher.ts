import { Injectable } from "@angular/core";
import { Action, Store } from "@ngrx/store";

@Injectable()
export class Dispatcher {
    public store: Store<any>;

    constructor(data: Store<any>) {
        this.store = data;
    }
    
    public fire(action: Action): any {
        this.store.dispatch(action)
    }
}