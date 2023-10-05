import { Component } from '@angular/core';
import { UserService } from 'src/core/services/user.service';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css']
})
export class AuthComponent {
  isLoginForm = false;
  submitButtonText = this.isLoginForm ? 'Login' : 'Sign Up'

  constructor(private userService: UserService){}

  onSubmit(form){
    const email = form.value.email;
    const password = form.value.password;

    this.userService.createUser(email, password)
      .subscribe(data => {
        localStorage.setItem('AUTH_TOKEN', data.jwtToken);
        location.reload();
      });
  }
}
