import { Injectable } from '@angular/core';
import { Notification } from '../models/notification';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class NotifDisplayService {
  
  constructor(private toastr: ToastrService) { }


  public handleNotification(notification: Notification) {
    this.showNotification(notification);
  }

  private showNotification(notification: Notification) {
    switch(notification.type) {
      case 0: 
            this.toastr.success(notification.message, 'New examination scheduled');
            break;
  
      case 2: 
            this.toastr.error(notification.message, 'Examination cancelled');
            break;
    }
  
  }

 
}
