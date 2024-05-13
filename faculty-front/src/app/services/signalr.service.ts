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

    public subscribeToYearGroup(year: number) {
      this.hubConnection.invoke('SubscribeToYearGroup', year)
        .then(() => console.log('Successfully subscribed to year group:', year))
        .catch(err => console.error('Error while subscribing to year group: ' + err));
    }
}
