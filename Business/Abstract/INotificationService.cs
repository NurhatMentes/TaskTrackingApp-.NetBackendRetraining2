using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface INotificationService
    {
        IResult Add(NotificationAddDto notificationAddDto);
        IResult Update(NotificationUpdateDto notificationUpdateDto);
        IResult Delete(int notificationId);
        IDataResult<List<NotificationDto>> GetAll();
        IDataResult<List<Notification>> GetAllByUserId(int userId);
    }
}
