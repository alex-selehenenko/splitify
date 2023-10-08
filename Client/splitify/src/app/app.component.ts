import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/core/services/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  readonly default = 0;
  readonly unauthorized = 1;
  readonly notVerified = 2;
  readonly authorized = 3;

  displayNewPassword = false;
  
  title = 'splitify';
  userEmail = '';
  state = this.default;

  constructor(private userService: UserService){}

  ngOnInit(): void {

    this.displayNewPassword = location.pathname === '/password_reset';
    
    if (!this.displayNewPassword){
      this.userService.userUnauthorized.subscribe(() => {
        this.state = this.unauthorized;
      });
  
      this.userService.userNotVerified.subscribe(() => {
        this.state = this.notVerified;
      });
  
      this.userService.userAuthorized.subscribe(() => {
        this.state = this.authorized;
      });
  
      this.userService.fetchUser()
          .subscribe(user => {
            this.userEmail = user.email;
            this.state = this.authorized;
          });
    }
  }

  onLogoutClick(){
    localStorage.removeItem('AUTH_TOKEN');
    location.reload();
  } 
}
