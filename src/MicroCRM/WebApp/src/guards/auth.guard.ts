import { Injectable } from "@angular/core";
import { Router, CanActivate } from "@angular/router";
import { AuthService } from "./../services/auth.service";

@Injectable()
export class AuthGuard implements CanActivate {
  private readonly router: Router;
  private readonly authService: AuthService;

  /**
   * Constructor.
   * @param router The router.
   * @param authService The auth service.
   */
  public constructor(router: Router, authService: AuthService) {
    this.router = router;
    this.authService = authService;
  }

  public canActivate(): boolean {
    if (this.authService.isAuthenticated()) {
      return true;
    }

    this.router.navigate(["/auth"]);
    return false;
  }
}
