import { NgModule } from "@angular/core";
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from "@angular/common/http";
import { Routes, RouterModule } from "@angular/router";
import { AuthGuard } from "./../../guards/auth.guard";
import { SecurityGuard } from "./../../guards/security.guard";
import { CustomersComponent } from "./../customers/customers.component";
import { OrdersComponent } from "./../orders/orders.component";
import { OrderComponent } from "./../orders/order.component";
import { ProductsComponent } from "./../products/products.component"
import { AuthComponent } from "./../auth/auth.component";
import { CustomerService } from "./../../services/customer.service";
import { AuthService } from "./../../services/auth.service";
import { OrderService } from "./../../services/order.service";
import { ProductService } from "./../../services/product.service";
import { UsersComponent } from "./../users/users.component";
import { UserService } from "./../../services/user.service";
import { AppComponent } from "./app.component";

const routes: Routes = [
  {
    path: "auth",
    component: AuthComponent,
  },
  {
    path: "customers",
    component: CustomersComponent,
    canActivate: [AuthGuard]
  },
  {
    path: "products",
    component: ProductsComponent,
    canActivate: [AuthGuard]
  },
  {
    path: "orders",
    component: OrdersComponent,
    canActivate: [AuthGuard]
  },
  {
    path: "orders/:id",
    component: OrderComponent,
    canActivate: [AuthGuard]
  },
  {
    path: "users",
    component: UsersComponent,
    canActivate: [AuthGuard, SecurityGuard]
  },
  {
    path: "**",
    redirectTo: "orders"
  }
];

@NgModule({
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    RouterModule.forRoot(routes)
  ],
  declarations: [
    AuthComponent,
    CustomersComponent,
    ProductsComponent,
    OrdersComponent,
    OrderComponent,
    UsersComponent,
    AppComponent
  ],
  providers: [
    AuthGuard,
    SecurityGuard,
    AuthService,
    CustomerService,
    OrderService,
    ProductService,
    UserService
  ],
  bootstrap: [
    AppComponent
  ]
})
export class AppModule { }
