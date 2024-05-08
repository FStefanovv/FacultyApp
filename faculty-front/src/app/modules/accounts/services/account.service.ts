import { Injectable } from '@angular/core';

import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http'
import { LoginDto } from '../dtos/login-dto';
import { Observable } from 'rxjs';
import { Jwt } from '../../../responses/jwt';
import { tokenGetter } from '../../../services/auth.service';
import { UserData } from '../dtos/user-data-dto';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(private http: HttpClient) { }

  attemptLogin(dto: LoginDto) : Observable<Jwt> {
    const loginUrl = 'https://localhost:5001/login';
    return this.http.post<Jwt>(loginUrl, dto);
  }

  getUserData() : Observable<UserData> {
    const token = tokenGetter()
    const headers = {'Authorization': `Bearer ${token}`}

    const userDataUrl = 'https://localhost:5001/user-data';

    return this.http.get<UserData>(userDataUrl, {headers})
  }

}
