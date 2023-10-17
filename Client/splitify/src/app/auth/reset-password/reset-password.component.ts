import { Component, EventEmitter, Output } from '@angular/core';
import { UserService } from 'src/core/services/user.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent {
  @Output() closed: EventEmitter<void> = new EventEmitter();

  errorMessage = "";
  requestCompleted = false;

  constructor(private userService: UserService){}

  onSubmit(form){
    this.errorMessage = '';
    this.userService.sendResetPasswordCode(form.value.email)
      .subscribe({
          next: _ => {
            this.requestCompleted = true;
          },
          error: err => {
            this.errorMessage = err.status === 500 || err.error === undefined || err.error === null
              ? 'Something went wrong. Please, try later.'
              : err.error.detail;
          }
        }
      )
  }

  onCloseClicked(){
    this.errorMessage = '';
    this.closed.emit();
  }
}
