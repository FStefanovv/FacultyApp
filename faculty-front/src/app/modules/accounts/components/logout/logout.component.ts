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

  constructor(private authService: AuthService, private router: Router){}

  logout() {
    this.authService.logout().subscribe({
      next: () => {
        localStorage.clear();
      },
      error: error => {
        console.error(error);
      }
    });
    this.router.navigate(['/login']);  
  }
}