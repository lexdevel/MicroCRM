import { Injectable } from "@angular/core";
import { Router, CanActivate } from "@angular/router";

@Injectable()
export class SecurityGuard implements CanActivate {
  private readonly router: Router;

  /**
   * Constructor.
   * @param router The router.
   */
  public constructor(router: Router) {
    this.router = router;
  }

  public canActivate(): boolean {
    return localStorage.getItem("role").toLowerCase() == "admin";
  }
}
