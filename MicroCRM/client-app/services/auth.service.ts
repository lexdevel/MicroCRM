import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { Token }      from "./../models/token";

@Injectable()
export class AuthService {
    private readonly httpClient: HttpClient;

    /**
     * Constructor.
     * @param httpClient The HTTP client.
     */
    public constructor(httpClient: HttpClient) {
        this.httpClient = httpClient;
    }

    public signIn(username: string, password: string): Observable<Token> {
        return this.httpClient.post<Token>("/api/token", { username, password });
    }

    public isAuthenticated(): boolean {
        return localStorage.getItem("access_token") != null;
    }

    // public generateAuthorizationHeaders(): { "Authorization": string } {
    //     return { "Authorization": "Bearer " + localStorage.getItem("access_token") };
    // }

    public isAdmin(): boolean { return localStorage.getItem("role").toLowerCase() == "admin"; }
    public isOwner(): boolean { return localStorage.getItem("role").toLowerCase() == "owner"; }
    public isSales(): boolean { return localStorage.getItem("role").toLowerCase() == "sales"; }
}
