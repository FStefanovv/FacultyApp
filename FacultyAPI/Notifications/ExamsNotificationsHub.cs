namespace FacultyApp.Notifications;

using FacultyApp.Enums;
using Microsoft.AspNetCore.SignalR;

public class ExamsNotificationsHub : Hub<INotificationHub> {
    public async Task SubscribeToYearGroup(int year){
        await Groups.AddToGroupAsync(Context.ConnectionId, year.ToString());
    }
}