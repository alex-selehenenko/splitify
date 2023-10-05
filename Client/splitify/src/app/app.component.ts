import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/core/services/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'splitify';
  state: number = 2;

  constructor(private userService: UserService){}

  ngOnInit(): void {
    this.userService.userUnauthorized.subscribe(() => {
      this.state = 0;
    });

    this.userService.userNotVerified.subscribe(() => {
      this.state = 1;
    });

    this.userService.userAuthorized.subscribe(() => {
      this.state = 2;
    });
  }
}
