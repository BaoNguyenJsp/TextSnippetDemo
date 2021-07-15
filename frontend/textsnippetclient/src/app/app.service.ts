import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "environments/environment";

@Injectable()
export class AppService {
    baseUrl = environment.baseUrl; 
    
    constructor(protected httpClient: HttpClient) { }

    login(data: any) {
        const url = `${this.baseUrl}/api/auth`;
        return this.httpClient.post<any>(url, data);
    }

    getPagination(query: string, pageSize: number, pageNumber: number) {
        const url = `${this.baseUrl}/api/textsnippet/pagination?query=${encodeURIComponent(query)}&pageSize=${pageSize}&pageNumber=${pageNumber}`;
        return this.httpClient.get<any[]>(url);
    }

    
    create(data: any) {
        const url = `${this.baseUrl}/api/textsnippet`;
        return this.httpClient.post(url, data);
    }

    update(id: number, data: any) {
        const url = `${this.baseUrl}/api/textsnippet/${id}`;
        return this.httpClient.put(url, data);
    }

    delete(id: number) {
        const url = `${this.baseUrl}/api/textsnippet/${id}`;
        return this.httpClient.delete(url);
    }
}