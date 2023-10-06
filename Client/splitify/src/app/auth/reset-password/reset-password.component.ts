import { Component, EventEmitter, Output } from '@angular/core';
import { UserService } from 'src/core/services/user.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent {
  @Output() closed: EventEmitter<void> = new EventEmitter();

  constructor(private userService: UserService){}

  onSubmit(form){
    this.userService.sendResetPasswordCode(form.value.email)
      .subscribe({
          next: data => {
            this.closed.emit();
          },
          error: err => {
            console.log(err);
          }
        }
      )
  }
}
