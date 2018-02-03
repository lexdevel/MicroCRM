import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { Order }      from "./../models/order";

@Injectable()
export class OrderService {
    private readonly httpClient: HttpClient;

    /**
     * Constructor.
     * @param httpClient The HTTP client.
     */
    public constructor(httpClient: HttpClient) {
        this.httpClient = httpClient;
    }

    public retrieve(offset: number = 0, length: number = 20): Observable<Order[]> {
        return this.httpClient.get<Order[]>("/api/orders?offset=" + offset + "&length=" + length, {
            headers: { "Authorization": "Bearer " + localStorage.getItem("access_token") }
        });
    }

    public load(id: string): Observable<Order> {
        return this.httpClient.get<Order>("/api/orders/" + id, {
            headers: { "Authorization": "Bearer " + localStorage.getItem("access_token") }
        });
    }

    public create(order: Order): Observable<Order> {
        return this.httpClient.post<Order>("/api/orders", order, {
            headers: { "Authorization": "Bearer " + localStorage.getItem("access_token") }
        });
    }

    public update(order: Order): Observable<Order> {
        return this.httpClient.put<Order>("/api/orders", order, {
            headers: { "Authorization": "Bearer " + localStorage.getItem("access_token") }
        });
    }
}
