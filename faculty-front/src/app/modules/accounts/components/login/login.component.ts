import { Component } from '@angular/core';

import { LoginDto } from '../../dtos/login-dto';
import { AccountService } from '../../services/account.service';
import { AuthService } from '../../../../services/auth.service';
import { SessionService } from '../../../../services/session.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  loginDto: LoginDto = new LoginDto();
  loginError!: string;

  constructor(private accountService: AccountService, private authService: AuthService,
              private sessionService: SessionService, private router: Router){}


  login(){
      this.authService.login(this.loginDto).subscribe({
        next: async () => {
          if(this.authService.getRole() == 'Student') {
            await this.sessionService.initiateStudentSession();
          }
          else this.router.navigate(['/my-exams']) 
        },
        error: error => {       
          if(error.status === 404)
            this.loginError = 'No used found with the provided email';
          else if(error.status === 401)
            this.loginError = 'Wrong password';
          else {
            console.log(error.message);
          }    
        }
      });
  }


}
