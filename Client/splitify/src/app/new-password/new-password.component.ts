import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/core/services/user.service';

@Component({
  selector: 'app-new-password',
  templateUrl: './new-password.component.html',
  styleUrls: ['./new-password.component.css']
})
export class NewPasswordComponent implements OnInit{
  errorMessage = '';
  isTokenValid = false;
  passwordResetCompleted = false;

  token = '';

  constructor(private userService: UserService){}
  
  ngOnInit(){
    const queryParams = new URLSearchParams(location.toString().split('?')[1]);
    this.token = queryParams.get('token');

    this.userService.verifyResetPasswordToken(this.token)
      .subscribe({
        next: data => this.isTokenValid = true,
        error: err => this.isTokenValid = false
      });
  }

  onSubmit(form){
    this.errorMessage = '';
    const password = form.value.password;
    const confirmPassword = form.value.confirmPassword;

    if (password !== confirmPassword){
      this.errorMessage = "Password mismatch";
      return;
    }

    this.userService.setNewPassword(this.token, password)
      .subscribe({
        next: _ => this.passwordResetCompleted = true,
        error: err => {
          this.errorMessage = err.error === undefined || err.error === null
            ? 'Something went wrong. Please, try later.'
            : err.error.detail;
        }
      });
  }

  onSuccessClick(){
    this.errorMessage = '';
    location.replace('/');
  }
}
