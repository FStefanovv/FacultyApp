import { ActivatedRouteSnapshot, CanActivate, GuardResult, MaybeAsync, Router, RouterStateSnapshot } from '@angular/router';
import { AuthService } from './services/auth.service';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router){}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): MaybeAsync<GuardResult> {
      const isAuthenticated = this.authService.isLoggedIn();

      if(!isAuthenticated) {
        this.router.navigate(['/login'])
        return false;
      }

      const role = this.authService.getRole();

      const { allowedRoles } = route.data;
      if(allowedRoles && !allowedRoles.includes(role)) {
        this.router.navigate(['/login']);
        return false;
      }
      else {
        return true;
      }
  }

};
