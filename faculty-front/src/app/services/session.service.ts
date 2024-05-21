import { Injectable } from '@angular/core';
import { AccountService } from '../modules/accounts/services/account.service';
import { SignalrService } from './signalr.service';

@Injectable({
  providedIn: 'root'
})
export class SessionService {

  constructor(private accountService: AccountService,
              private signalRService: SignalrService) { }

  public async setUserSessionData(): Promise<void> {
    return new Promise<void>((resolve, reject) => {
        this.accountService.getUserData().subscribe({ 
            next: response => { 
                let userData = response;
                
                if (userData.currentYear) {
                    localStorage.setItem('role', 'Student'); 
                    localStorage.setItem('currentYear', userData.currentYear.toString());
                } else {
                    localStorage.removeItem('currentYear');
                    localStorage.setItem('role', 'Teacher'); 
                }
                resolve(); 
            },  
            error: error => {
                console.log(error.message);
                reject(error);
            }
        });
    });
  }
            
  public async initiateStudentSession() {
    await this.signalRService.startConnection();

    const currentYear = localStorage.getItem("currentYear");

    if(currentYear){
      await this.signalRService.subscribeToYearGroup(Number(currentYear));
      this.signalRService.addListener();  
    }
  }

  public initiateTeacherSession(){
    
  }

  public getRole() : string | null {
    if(this.isLoggedIn())
      return localStorage.getItem('role');

    else return null;
  }

  public isLoggedIn() : boolean {
    const exists = this.expirationCookieExists("X-Expiration-Cookie");

    if(exists) {
      return true;
    }
    else {
      localStorage.clear();
      return false;
    }
  }

  private expirationCookieExists(name: string): boolean {
    const cookiePattern = new RegExp('(^|;)\\s*' + name + '\\s*=');

    return cookiePattern.test(document.cookie);
  }
}