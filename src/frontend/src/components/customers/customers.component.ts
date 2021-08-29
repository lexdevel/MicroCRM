import { Component, OnInit } from "@angular/core";
import { CustomerService } from "./../../services/customer.service";
import { Customer } from "../../models/customer";

@Component({
  selector: "customers",
  templateUrl: "./customers.component.html"
})
export class CustomersComponent implements OnInit {
  private readonly customerService: CustomerService;

  public customers: Customer[] = [];
  public createMode: boolean = false;
  public customer: Customer = null;

  /**
   * Constructor.
   * @param customerService The customer service.
   */
  public constructor(customerService: CustomerService) {
    this.customerService = customerService;
  }

  public ngOnInit(): void {
    this.customerService.retrieve().subscribe(response => {
      this.customers = response;
    });
  }

  public createCustomer(): void {
    this.createMode = true;
    this.customer = new Customer();
    this.customer.gender = "Female";
  }

  public cancel(): void {
    this.createMode = false;
  }

  public submit(): void {
    this.customerService.create(this.customer).subscribe(_ => {
      this.customerService.retrieve().subscribe(response => this.customers = response);
      this.cancel();
    });
  }
}
