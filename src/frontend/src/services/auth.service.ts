import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { Token } from "./../models/token";

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
    const token = localStorage.getItem("access_token");
    return token !== null;
  }
}
