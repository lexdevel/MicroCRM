import { Product } from "./product";

/**
 * The order line model class.
 */
export class OrderLine {
    public id: string;
    public product: Product;
    public quantity: number;
    public total: number;
}
