import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { UserData } from '../modules/accounts/dtos/user-data-dto';
import { LoginDto } from '../modules/accounts/dtos/login-dto';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export function tokenGetter() {
  return localStorage.getItem('jwt');
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  
  constructor(private http: HttpClient) {}

  public login(dto: LoginDto) : Observable<any> {
    const loginUrl = 'https://localhost:5001/login';
    return this.http.post(loginUrl, dto);
  }

  /*
  public login(loginData: LoginData) {
    localStorage.setItem('loggedIn', 'true');
    localStorage.setItem('role', loginData.role);
    localStorage.setItem('email', loginData.email);
    localStorage.setItem('userId', loginData.userId);
  }
  */

  public logout() : Observable<any> {
    const logoutUrl = 'https://localhost:5001/logout';
    return this.http.post(logoutUrl, {});
  }

  public isLoggedIn() : boolean {
    const loggedIn = localStorage.getItem('loggedIn');
    if(loggedIn && loggedIn==='true')
      return true;
    else return false;
  }

  public getRole() : any {
    const role = localStorage.getItem('role');
    if(role)
      return role;
    else return undefined;
  }

  public getEmail() : any {
    const email = localStorage.getItem('email');
    if(email)
      return email;
    else return undefined;
  }

  public getUserId() : any {
    const userId = localStorage.getItem('userId');
    if(userId)
      return userId;
    else return undefined;
  }

  saveUserData(userData: UserData) {
    localStorage.setItem('user-data', JSON.stringify(userData));
  }

  getUserData() : any {
    return localStorage.getItem('user-data');
  }
}
