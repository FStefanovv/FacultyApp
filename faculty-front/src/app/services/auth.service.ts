import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { UserData } from '../modules/accounts/dtos/user-data-dto';
import { LoginDto } from '../modules/accounts/dtos/login-dto';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  
  constructor(private http: HttpClient) {}

  public login(dto: LoginDto) : Observable<any> {
    const loginUrl = 'https://localhost:5001/login';
    return this.http.post(loginUrl, dto);
  }

  public logout() : Observable<any> {
    const logoutUrl = 'https://localhost:5001/logout';
    return this.http.post(logoutUrl, {});
  }
}
