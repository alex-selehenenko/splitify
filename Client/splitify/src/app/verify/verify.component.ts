import { Component } from '@angular/core';
import { UserService } from 'src/core/services/user.service';

@Component({
  selector: 'app-verify',
  templateUrl: './verify.component.html',
  styleUrls: ['./verify.component.css']
})
export class VerifyComponent {
  errorMessage = '';
  resendVerificationCodeRequestCompleted = false;

  constructor(private userService: UserService){}
  
  onSubmit(form){
    this.errorMessage = '';
    const code = form.value.code;

    this.userService.verifyUser(code)
      .subscribe({
        next: data => {
          localStorage.setItem('AUTH_TOKEN', data.jwtToken);
          location.reload();
        },
        error: err => {
          this.errorMessage = err.error === undefined || err.error === null
            ? 'Something went wrong. Please, try later.'
            : err.error.detail;
        }});
  }

  onSendAgain(){
    this.userService.resendVerificationCode()
    .subscribe({
      next: _ => this.resendVerificationCodeRequestCompleted = true,
      error: err => {
        this.errorMessage = err.error === undefined || err.error === null
          ? 'Something went wrong. Please, try later.'
          : err.error.detail;
      }
    });
  }

  onCloseModalClicked(){
    this.resendVerificationCodeRequestCompleted = false;
  }
}
