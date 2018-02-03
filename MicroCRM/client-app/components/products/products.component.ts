import { Component, OnInit } from "@angular/core";
import { ProductService }    from "./../../services/product.service";
import { Product }           from "../../models/product";

@Component({
    selector: "products",
    templateUrl: "./products.component.html"
})
export class ProductsComponent implements OnInit {
    private readonly productService: ProductService;
    private products: Product[] = [];

    private product: Product = null;
    private createMode: boolean = false;

    /**
     * Constructor.
     * @param productService The product service.
     */
    public constructor(productService: ProductService) {
        this.productService = productService;
    }

    public ngOnInit(): void {
        this.productService.retrieve().subscribe(response => {
            this.products = response;
        });
    }

    public createProduct(): void {
        this.createMode = true;
        this.product = new Product();
        this.product.costPrice = 0.0;
        this.product.price = 0.0;
    }

    public cancel(): void {
        this.createMode = false;
    }

    public submit(): void {
        this.productService.create(this.product).subscribe(rs => {
            this.productService.retrieve().subscribe(response => this.products = response);
            this.cancel();
        });
    }
}
