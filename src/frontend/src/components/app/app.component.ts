import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { AuthService } from "../../services/auth.service";

@Component({
  selector: "app",
  templateUrl: "./app.component.html"
})
export class AppComponent {
  private readonly router: Router;
  public readonly authService: AuthService;

  public constructor(router: Router, authService: AuthService) {
    this.router = router;
    this.authService = authService;
  }

  public logOff() {
    localStorage.removeItem("access_token");
    localStorage.removeItem("role");

    this.router.navigate(["auth"]);
  }
}
