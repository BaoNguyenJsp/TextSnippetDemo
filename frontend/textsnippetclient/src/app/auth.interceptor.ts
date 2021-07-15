import { HttpEvent, HttpHandler, HttpHeaders, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { AppSelector } from "./state/app.selector";

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
    token = "";

    constructor(private appSelector: AppSelector) {
        this.appSelector.getToken$.subscribe(token => {
            this.token = token;
        })
    }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        let requestConfig;
        let headers = new HttpHeaders();
        if (!!this.token) {
            headers = headers.append('Authorization', 'Bearer ' + this.token);
            requestConfig = {
                headers,
                withCredentials: true
            }
        } else {
            requestConfig = {
                headers,
                withCredentials: false
            }
        }
        req = req.clone(requestConfig);
        return next.handle(req);
    }
}