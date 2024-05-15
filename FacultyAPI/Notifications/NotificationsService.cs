using FacultyApp.Enums;
using FacultyApp.Model;
using FacultyApp.Notifications;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace FacultyApp.Notifications;

public class NotificationsService {
    private readonly ILogger<NotificationsService> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IHubContext<NotificationsHub, INotificationsClient> _notificationsHub;

    public NotificationsService(ILogger<NotificationsService> logger,
                                        IServiceScopeFactory serviceScopeFactory,
                                        IHubContext<NotificationsHub, INotificationsClient> notificationsHub) {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
        _notificationsHub = notificationsHub;
    }

    public void NotifyStudentsAboutExamination(string examinationId, NotificationType notificationType){
        Task.Run( async () => {
            try {
                using var scope = _serviceScopeFactory.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<StudentsDbContext>();
                
                var examination = await context.Examinations
                                    .Include(e => e.Teacher)
                                    .Include(e => e.Course)
                                    .FirstOrDefaultAsync(e => e.Id == examinationId);

                Notification notification = GenerateExaminationNotificationForStudents(examination, notificationType);
                await _notificationsHub.Clients.Group("year/"+examination.Course.Year).Receive(notification);
            } catch(Exception ex) {
                _logger.LogError(ex.Message);
            }
        });
    }
   
    private Notification GenerateExaminationNotificationForStudents(Examination examination, NotificationType notificationType){
        Notification notification;
        if(notificationType == NotificationType.EXAMINATION_CREATED){
            notification = new Notification {
                        Type = NotificationType.EXAMINATION_CREATED,
                        Message = $"{examination.Teacher.Email} has scheduled an examination " +
                                $"for subject {examination.Course.Name} " +
                                $"on {examination.ScheduledFor}"
                    };
        }  
        else if(notificationType == NotificationType.EXAMINATION_CANCELLED) {
            notification = new Notification {
                        Type = NotificationType.EXAMINATION_CANCELLED,
                        Message = $"{examination.Teacher.Email} has cancelled the examination " +
                                $"for subject {examination.Course.Name} " +
                                $"on {examination.ScheduledFor}"
                    };
        }  
        else throw new Exception("Invalid notification type.");

        return notification;
    }
}