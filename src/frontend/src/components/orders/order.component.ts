import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from '@angular/router';
import { OrderService } from "./../../services/order.service";
import { Order } from "./../../models/order";

@Component({
  selector: "order",
  templateUrl: "./order.component.html"
})
export class OrderComponent implements OnInit {
  private readonly orderService: OrderService;
  private readonly router: Router;

  public id: string;
  public order: Order;

  public constructor(orderService: OrderService, router: Router, activatedRoute: ActivatedRoute) {
    this.orderService = orderService;
    this.router = router;
    this.id = activatedRoute.snapshot.params["id"];
  }

  public ngOnInit(): void {
    this.orderService.load(this.id).subscribe(response => {
      this.order = response;
    });
  }

  public save(): void {
    this.orderService.update(this.order).subscribe(response => {
      this.order = response;
      this.router.navigate(["orders"]);
    });
  }

  public cancel(): void {
    this.router.navigate(["orders"]);
  }
}
