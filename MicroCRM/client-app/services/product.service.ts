import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { Product }    from "./../models/product";

@Injectable()
export class ProductService {
    private readonly httpClient: HttpClient;

    /**
     * Constructor.
     * @param httpClient The HTTP client.
     */
    public constructor(httpClient: HttpClient) {
        this.httpClient = httpClient;
    }

    public retrieve(offset: number = 0, length: number = 20): Observable<Product[]> {
        return this.httpClient.get<Product[]>("/api/products?offset=" + offset + "&length=" + length, {
            headers: { "Authorization": "Bearer " + localStorage.getItem("access_token") }
        });
    }

    public create(product: Product): Observable<Product> {
        return this.httpClient.post<Product>("/api/products", product, {
            headers: { "Authorization": "Bearer " + localStorage.getItem("access_token") }
        });
    }
}
