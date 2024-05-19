import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { SessionService } from './services/session.service';

export const authGuard: CanActivateFn = (route, state) => {
  const sessionService = inject(SessionService);
  const router = inject(Router);

  const isLoggedIn = sessionService.isLoggedIn();

  if(!isLoggedIn) {
    router.navigate(['/login']);
    return false;
  }

  const role = sessionService.getRole();

  const { allowedRoles } = route.data;

  if(allowedRoles && !allowedRoles.includes(role)) {
    router.navigate(['/login']);
    return false;
  }
  else {
    return true;
  }
}