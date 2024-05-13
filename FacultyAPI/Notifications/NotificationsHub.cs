namespace FacultyApp.Notifications;

using Microsoft.AspNetCore.SignalR;

public class NotificationsHub : Hub<INotificationsClient> {
    public async Task SubscribeToYearGroup(int year){
        await Groups.AddToGroupAsync(Context.ConnectionId, "year/"+year.ToString());
    }
}