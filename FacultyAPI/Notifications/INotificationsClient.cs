namespace FacultyApp.Notifications;

public interface INotificationsClient {
    Task Receive(Notification notification);
}