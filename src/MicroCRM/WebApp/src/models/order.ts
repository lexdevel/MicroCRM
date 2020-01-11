import { Customer } from "./customer";
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

/**
 * The order model class.
 */
export class Order {
  public id: string;
  public orderStatus: string;
  public orderNumber: string;
  public customer: Customer;
  public date: string;
  public orderLines: OrderLine[];
  public total: number;
}
