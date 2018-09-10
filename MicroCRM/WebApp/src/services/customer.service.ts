import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { Customer } from "./../models/customer";

@Injectable()
export class CustomerService {
  private readonly httpClient: HttpClient;

  /**
   * Constructor.
   * @param httpClient The HTTP client.
   */
  public constructor(httpClient: HttpClient) {
    this.httpClient = httpClient;
  }

  public retrieve(offset: number = 0, length: number = 20): Observable<Customer[]> {
    return this.httpClient.get<Customer[]>("/api/customers?offset=" + offset + "&length=" + length, {
      headers: { "Authorization": "Bearer " + localStorage.getItem("access_token") }
    });
  }

  public create(customer: Customer): Observable<Customer> {
    return this.httpClient.post<Customer>("/api/customers", customer, {
      headers: { "Authorization": "Bearer " + localStorage.getItem("access_token") }
    });
  }
}
