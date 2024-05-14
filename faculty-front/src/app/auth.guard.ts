import { ActivatedRouteSnapshot, CanActivate, GuardResult, MaybeAsync, Router, RouterStateSnapshot } from '@angular/router';
import { AuthService } from './services/auth.service';
import { Injectable } from '@angular/core';
import { SessionService } from './services/session.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private sessionService: SessionService, private router: Router){}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): MaybeAsync<GuardResult> {
      const isLoggedIn = this.sessionService.isLoggedIn();

      if(!isLoggedIn) {
        this.router.navigate(['/login']);
        return false;
      }

      const role = this.sessionService.getRole();

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
