import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { AccountService } from '../modules/accounts/services/account.service';
import { SignalrService } from './signalr.service';

@Injectable({
  providedIn: 'root'
})
export class SessionService {

  constructor(private authService: AuthService, private accountService: AccountService,
              private signalRService: SignalrService
  ) { }

  public initiateStudentSession(token: string) {
    this.accountService.getUserData().subscribe({
        next: async response => {
            
            let userData = response;

            this.authService.saveUserData(userData);

            const userDataString = this.authService.getUserData();
            const userDataParsed = JSON.parse(userDataString);

            await this.signalRService.startConnection('exams');
            
            if (userDataParsed && userDataParsed.hasOwnProperty('currentYear')) {
                const currentYear = userData.currentYear;

                this.signalRService.subscribeToYearGroup(currentYear);
                this.signalRService.addListener();
            }
          },  
        error: error => {
          console.log(error.message);
        }
    });
}

}
