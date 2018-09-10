import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { AuthService } from "./../../services/auth.service";

@Component({
  selector: "auth",
  templateUrl: "./auth.component.html"
})
export class AuthComponent {
  private readonly authService: AuthService;
  private readonly router: Router;
  public username: string = "";
  public password: string = "";
  public hasError: boolean = false;
  public errorMessage: string = "";

  public constructor(authService: AuthService, router: Router) {
    this.authService = authService;
    this.router = router;
  }

  public validate(): boolean {
    const regexp = /^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;
    return (this.username.length < 2 || this.password.length < 2 || !regexp.test(this.username));
  }

  public auth(): void {
    this.authService.signIn(this.username, this.password).subscribe(response => {
      localStorage.setItem("access_token", response.access_token);
      localStorage.setItem("role", response.role);
      this.router.navigate(["/orders"]);
    }, errorState => {
      console.log(errorState);
      this.hasError = true;
      this.errorMessage = "User not found or password is incorrect!";
    });
  }
}
