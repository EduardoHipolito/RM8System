import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
 import { Observable } from 'rxjs';

import { AuthService } from './auth.service';

@Injectable()
export class AuthLoginGuard implements CanActivate {

  constructor(
    private router: Router,
    private authService: AuthService) {}

  canActivate(  

    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | boolean {

    if (!this.authService.isAuthenticated()) {
      return true;
    }

    this.router.navigate(['/system/base/home']);
    return false;
  }
}
