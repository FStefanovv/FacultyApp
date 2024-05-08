using FacultyApp.Enums;
using FacultyApp.Notifications;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace FacultyApp.FireForget;

public class NotificationFireForgetHandler {
    private readonly ILogger<NotificationFireForgetHandler> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IHubContext<ExamsNotificationsHub, INotificationHub> _examinationNotifications;

    public NotificationFireForgetHandler(ILogger<NotificationFireForgetHandler> logger,
                                        IServiceScopeFactory serviceScopeFactory,
                                        IHubContext<ExamsNotificationsHub, INotificationHub> examinationNotifications) {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
        _examinationNotifications = examinationNotifications;
    }

    public void CreatedExamination(string examinationId){
        Task.Run( async () => {
            try {
                using var scope = _serviceScopeFactory.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<StudentsDbContext>();
                var examination = await context.Examinations
                                    .Include(e => e.Teacher)
                                    .Include(e => e.Course)
                                    .FirstOrDefaultAsync(e => e.Id == examinationId);

                await _examinationNotifications.Clients.Group(examination.Course.Year.ToString()).SendMessage(
                    new Notification {
                        Type = NotificationType.EXAMINATION_CREATED,
                        Message = $"{examination.Teacher.Email} has scheduled an examination " +
                                $"for subject {examination.Course.Name} " +
                                $"on {examination.ScheduledFor}"
                    }
                );
               
            } catch(Exception ex) {
                _logger.LogError(ex.Message);
            }
        }
        );
    }

    public void CancelledExamination(string id)
    {
        Task.Run( async () => {
            try {
                using var scope = _serviceScopeFactory.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<StudentsDbContext>();
                var examination = await context.Examinations
                                    .Include(e => e.Teacher)
                                    .Include(e => e.Course)
                                    .FirstOrDefaultAsync(e => e.Id == id);

                await _examinationNotifications.Clients.Group(examination.Course.Year.ToString()).SendMessage(
                    new Notification {
                        Type = NotificationType.EXAMINATION_CANCELLED,
                        Message = $"{examination.Teacher.Email} has cancelled the examination " +
                                $"for subject {examination.Course.Name} " +
                                $"on {examination.ScheduledFor}"
                    }
                );
               
            } catch(Exception ex) {
                _logger.LogError(ex.Message);
            }
        }
        );
    }
}