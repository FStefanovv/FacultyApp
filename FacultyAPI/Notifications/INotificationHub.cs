namespace FacultyApp.Notifications;

public interface INotificationHub {
    Task SendMessage(Notification notification);
}