import { Component, OnInit } from "@angular/core";
import { UserService } from "../../services/user.service";
import { User } from "../../models/user";

// Include jQuery...
declare var $: any;

@Component({
  selector: "users",
  templateUrl: "./users.component.html"
})
export class UsersComponent implements OnInit {
  private readonly userService: UserService;
  private users: User[] = [];

  private user: User;
  private createMode: boolean = false;

  private tempUsername: string;
  private tempPassword: string;

  public constructor(userService: UserService) {
    this.userService = userService;
  }

  public ngOnInit(): void {
    this.userService.retrieve().subscribe(response => {
      this.users = response;
    });
  }

  public createUser(): void {
    this.createMode = true;
    this.user = new User();
    this.user.role = "Sales";
  }

  public cancel(): void {
    this.createMode = false;
  }

  public submit(): void {
    this.userService.create(this.user).subscribe(rs => {
      this.userService.retrieve().subscribe(response => this.users = response);
      this.tempUsername = rs["username"];
      this.tempPassword = rs["password"];
      this.cancel();

      // Show modal window...
      $("#modalAlert").modal();
    });
  }
}
