import { Component }    from "@angular/core";
import { AuthService }  from "./../../services/auth.service";

@Component({
    selector: "app",
    templateUrl: "./app.component.html",
    styleUrls: ["app.component.css"]
})
export class AppComponent {
    private readonly authService: AuthService;

    public constructor(authService: AuthService) {
        this.authService = authService;
    }
}
