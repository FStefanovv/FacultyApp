import { Component } from '@angular/core';
import { AuthService } from '../../../../services/auth.service';
import { Router } from '@angular/router';
import { SessionService } from '../../../../services/session.service';

@Component({
  selector: 'logout',
  templateUrl: './logout.component.html',
  styleUrl: './logout.component.css'
})
export class LogoutComponent {

  constructor(private authService: AuthService, private router: Router, private sessionService: SessionService){}

  logout() {
    this.authService.logout().subscribe({
      next: () => {
        sessionStorage.clear();
        this.router.navigate(['/login']);
      },
      error: error => {
        console.log(error.message);
      }
    })
  }
}
