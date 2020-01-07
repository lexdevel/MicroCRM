import { Customer } from "./customer";
import { OrderLine } from "./order-line";

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
