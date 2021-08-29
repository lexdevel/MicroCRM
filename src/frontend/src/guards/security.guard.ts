import { Injectable } from "@angular/core";
import { CanActivate } from "@angular/router";

@Injectable()
export class SecurityGuard implements CanActivate {

  public canActivate(): boolean {
    const role = localStorage.getItem("role");
    return role.toLowerCase() === "admin";
  }
}
