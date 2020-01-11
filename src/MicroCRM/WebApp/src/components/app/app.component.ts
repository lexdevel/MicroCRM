import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { AuthService } from "./../../services/auth.service";

@Component({
  selector: "app",
  templateUrl: "./app.component.html"
})
export class AppComponent {
  private readonly authService: AuthService;
  private readonly router: Router;

  public constructor(authService: AuthService, router: Router) {
    this.authService = authService;
    this.router = router;
  }

  public logOff() {
    localStorage.removeItem("access_token");
    localStorage.removeItem("role");
    this.router.navigate(["auth"]);
  }
}
