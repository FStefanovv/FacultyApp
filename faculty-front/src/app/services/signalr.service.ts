import { Injectable } from '@angular/core';

import * as signalR from '@microsoft/signalr';
import { Notification } from '../models/notification';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class SignalrService {
  private hubConnection!: signalR.HubConnection;

  constructor(private toastr: ToastrService) {}

    public startConnection = async (subscribeTo: string) => {
        this.hubConnection = new signalR.HubConnectionBuilder()
                                  .withUrl('https://localhost:5001/'+subscribeTo,
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
        this.hubConnection.on('SendMessage', (notification: Notification) => {
          this.showNotification(notification);
        });
    };

    public subscribeToYearGroup(year: number) {
      this.hubConnection.invoke('SubscribeToYearGroup', year)
        .then(() => console.log('Successfully subscribed to year group:', year))
        .catch(err => console.error('Error while subscribing to year group: ' + err));
    }

    showNotification(notification: Notification) {
      const [notificationClass, notificationTitle] = this.inferNotificationClass(notification.type);
      this.toastr.info(notification.message, notificationTitle, {
        enableHtml: true, 
        toastClass: notificationClass
      });
    }

    inferNotificationClass(notificationType: number) : [string, string] {
      switch(notificationType) {
        case 0: return ['exam-created', 'New examination scheduled'];
        case 2: return ['exam-cancelled', 'Examination cancelled'];
        default: return ['', ''];
      }
    }
  
}
