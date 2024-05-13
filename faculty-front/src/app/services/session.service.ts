import { Injectable } from '@angular/core';
import { AccountService } from '../modules/accounts/services/account.service';
import { SignalrService } from './signalr.service';

@Injectable({
  providedIn: 'root'
})
export class SessionService {

  constructor(private accountService: AccountService,
              private signalRService: SignalrService
  ) { }

  public setUserSessionData(){
    this.accountService.getUserData().subscribe({ 
      next: response => {            
        let userData = response;
        if(userData.currentYear){
          sessionStorage.setItem('role', 'Student'); 
          sessionStorage.setItem('currentYear', userData.currentYear.toString());
        }
        else {
          console.log("it's a teacher");
          sessionStorage.setItem('role', 'Teacher'); 
        }
      },  
      error: error => {
        console.log(error.message);
      }
    })
  }

  public async initiateStudentSession() {
    
    await this.signalRService.startConnection();

    const currentYear = sessionStorage.getItem("currentYear");

    if(currentYear){
      this.signalRService.subscribeToYearGroup(Number(currentYear));
      this.signalRService.addListener();  
    }
  }

  public initiateTeacherSession(){
    
  }

  public getRole() : string | null {
    return sessionStorage.getItem('role');
  }

  public isLoggedIn() : boolean {
    const cookieValue = this.getCookie("X-Expiration-Cookie");
    if(cookieValue){
      const isExpired = this.isCookieExpired(cookieValue);
      return !isExpired;
    }
    else return false;
  }

  private getCookie(name: string): string | null {
    const cookieValue = document.cookie.match('(^|;)\\s*' + name + '\\s*=\\s*([^;]+)');
    
    return cookieValue ? cookieValue?.pop() ?? null : null;
  }

  private isCookieExpired(cookieValue: string): boolean {
    const decodedValue = decodeURIComponent(cookieValue);
    const expirationDate = new Date(decodedValue);
  
    if (isNaN(expirationDate.getTime())) {
      return true; 
    }
  
    return expirationDate <= new Date();
  }



}
