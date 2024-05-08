import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { UserData } from '../modules/accounts/dtos/user-data-dto';

export function tokenGetter() {
  return localStorage.getItem('jwt');
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  
  private helper: JwtHelperService = new JwtHelperService();

  constructor( ) {}

  private decode() : any {
    let token = tokenGetter();
    if(token != null) {
      let decoded = this.helper.decodeToken(token);
      return decoded;
    }
  }

  public login(token: string) {
    localStorage.setItem('jwt', token);
  }

  public logout() {
    localStorage.removeItem('jwt');
  }

  public isLoggedIn() : boolean {
    let token = tokenGetter();
    return token != null && !this.helper.isTokenExpired(token);
  }

  public getRole() : any {
    let decoded = this.decode();
    const role = decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
    return role;
  }

  public getEmail() : any {
    let decoded = this.decode();
    let email = decoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'];
    return email;
  }

  public getUserId() : any {
    let decoded = this.decode();
    return decoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'];
  }

  saveUserData(userData: UserData) {
    localStorage.setItem('user-data', JSON.stringify(userData));
  }

  getUserData() : any {
    return localStorage.getItem('user-data');
  }
}
