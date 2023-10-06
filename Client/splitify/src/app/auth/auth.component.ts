import { Component, ViewChild } from '@angular/core';
import { UserService } from 'src/core/services/user.service';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css']
})
export class AuthComponent {
  @ViewChild('form') form: any

  isLoginForm = true;
  submitButtonText = 'Login';
  errorMessage= '';

  constructor(private userService: UserService){}

  onSubmit(form){
    const email = form.value.email;
    const password = form.value.password;

    let action = this.isLoginForm
      ? this.userService.loginUser(email, password)
      : this.userService.createUser(email, password);

    action.subscribe({
        next: res => { 
          localStorage.setItem('AUTH_TOKEN', res.jwtToken);
          location.reload();
        },
        error: err => {
          this.errorMessage = err.error.detail;
        }
      });
  }

  onToggleForm(){
    this.form.resetForm();
    this.isLoginForm = !this.isLoginForm;
    this.errorMessage = '';
    this.submitButtonText = this.isLoginForm ? 'Login' : 'Sign Up';
  }
}
