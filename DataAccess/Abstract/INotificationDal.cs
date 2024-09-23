using Core.DataAccess;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Abstract
{
    public interface INotificationDal :IEntityRepository<Notification>
    {
        IDataResult<List<Notification>> GetAllByUserId(int userId);
        IDataResult<List<NotificationDto>> GetAllNotificationsWithUsers();
    }
}
