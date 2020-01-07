import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { User } from "./../models/user";

@Injectable()
export class UserService {
  private readonly httpClient: HttpClient;

  /**
   * Constructor.
   * @param httpClient The HTTP client.
   */
  public constructor(httpClient: HttpClient) {
    this.httpClient = httpClient;
  }

  public retrieve(offset: number = 0, length: number = 20): Observable<User[]> {
    return this.httpClient.get<User[]>("/api/users?offset=" + offset + "&length=" + length, {
      headers: { "Authorization": "Bearer " + localStorage.getItem("access_token") }
    });
  }

  public create(user: User): Observable<User> {
    return this.httpClient.post<User>("/api/users", user, {
      headers: { "Authorization": "Bearer " + localStorage.getItem("access_token") }
    });
  }
}
