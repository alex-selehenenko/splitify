import { Component } from '@angular/core';
import { UserService } from 'src/core/services/user.service';

@Component({
  selector: 'app-verify',
  templateUrl: './verify.component.html',
  styleUrls: ['./verify.component.css']
})
export class VerifyComponent {
  constructor(private userService: UserService){}
  
  onSubmit(form){
    const code = form.value.code;

    this.userService.verifyUser(code)
      .subscribe(data => {
        localStorage.setItem('AUTH_TOKEN', data.jwtToken);
        location.reload();
      });
  }
}
