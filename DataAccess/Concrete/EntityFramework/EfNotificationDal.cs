using Core.DataAccess.EntityFramework;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfNotificationDal : EfEntityRepositoryBase<Notification,TaskTrackingAppDBContext>,INotificationDal
    {
        public IDataResult<List<Notification>> GetAllByUserId(int userId)
        {
            using (var context = new TaskTrackingAppDBContext())
            {
                var notifications = context.Notifications
                    .Where(n => n.UserId == userId)
                    .ToList();

                return new SuccessDataResult<List<Notification>>(notifications);
            }
        }
        public IDataResult<List<NotificationDto>> GetAllNotificationsWithUsers()
        {
            using (var context = new TaskTrackingAppDBContext())
            {
                var result = from notification in context.Notifications
                             join user in context.Users on notification.UserId equals user.Id
                             select new NotificationDto
                             {
                                 Id = notification.Id,
                                 UserName = user.FirstName + " " + user.LastName,
                                 Message = notification.Message,
                                 IsRead = notification.IsRead,
                                 CreatedAt = notification.CreatedAt
                             };

                return new SuccessDataResult<List<NotificationDto>>(result.ToList());
            }
        }
    }
}
