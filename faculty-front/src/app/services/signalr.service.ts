import { Injectable } from '@angular/core';

import * as signalR from '@microsoft/signalr';
import { Notification } from '../models/notification';
import { ToastrService } from 'ngx-toastr';
import { NotifDisplayService } from './notif-display.service';

@Injectable({
  providedIn: 'root'
})
export class SignalrService {
  private hubConnection!: signalR.HubConnection;

  constructor(private notifDisplayService: NotifDisplayService) {}

    public startConnection = async () => {
        if (this.hubConnection) {
          this.hubConnection.stop();
        }
        this.hubConnection = new signalR.HubConnectionBuilder()
                                  .withUrl('https://localhost:5001/notifs',
                                    { skipNegotiation: true,
                                      transport: signalR.HttpTransportType.WebSockets
                                    })
                                  .build();;
        try {
          await this.hubConnection.start();
          console.log('Connection started');
        } catch (error) {
          console.log('Error while starting connection: ' + error);
        }
    }

    public addListener = () => {
        this.hubConnection.on('Receive', (notification: Notification) => {
          this.notifDisplayService.handleNotification(notification);
        });
    };

    public async subscribeToYearGroup(year: number) {
      try {
        await this.hubConnection.invoke('SubscribeToYearGroup', year);
        console.log('Successfully subscribed to year group:', year);
      } catch (error) {
        console.error('Error while subscribing to year group:', error);
      }
    }
}
