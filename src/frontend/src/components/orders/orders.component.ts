import { Component, OnInit } from "@angular/core";
import { OrderService } from "./../../services/order.service";
import { CustomerService } from "./../../services/customer.service";
import { ProductService } from "./../../services/product.service";
import { OrderLine, Order } from "./../../models/order";
import { Customer } from "./../../models/customer";
import { Product } from "./../../models/product";

@Component({
  selector: "orders",
  templateUrl: "./orders.component.html"
})
export class OrdersComponent implements OnInit {
  private readonly orderService: OrderService;
  private readonly customerService: CustomerService;
  private readonly productService: ProductService;

  public orders: Order[] = [];
  public createMode: boolean = false;
  public order: Order;

  public customers: Customer[];
  public products: Product[];

  /**
   * Constructor.
   * @param orderService The order service.
   * @param customerService The customer service.
   * @param productService The product service.
   */
  public constructor(orderService: OrderService, customerService: CustomerService, productService: ProductService) {
    this.orderService = orderService;
    this.customerService = customerService;
    this.productService = productService;
  }

  public ngOnInit(): void {
    this.orderService.retrieve().subscribe(response => this.orders = response);
    this.customerService.retrieve().subscribe(response => this.customers = response);
    this.productService.retrieve().subscribe(response => this.products = response);
  }

  public createOrder(): void {
    this.order = new Order();
    this.order.customer = this.customers[0];
    this.order.orderLines = [];
    this.createMode = true;
  }

  public addOrderLine(): void {
    var orderLine = new OrderLine();
    orderLine.quantity = 1;
    orderLine.product = this.products[0];
    this.order.orderLines.push(orderLine);
  }

  public removeOrderLine(orderLine: OrderLine): void {
    let at = this.order.orderLines.findIndex(ol => ol == orderLine);
    if (at >= 0) {
      this.order.orderLines.splice(at, 1);
    }
  }

  public calculateTotal(): number {
    let result = 0;
    for (let i = 0; i < this.order.orderLines.length; i++) {
      result += this.order.orderLines[i].product.price * this.order.orderLines[i].quantity;
    }
    return result;
  }

  public cancel(): void {
    this.createMode = false;
  }

  public submit(): void {
    this.orderService.create(this.order).subscribe(rs => {
      this.orderService.retrieve().subscribe(response => this.orders = response);
      this.cancel();
    });
  }
}
