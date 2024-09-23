using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete
{
    public class NotificationManager : INotificationService
    {
        private readonly INotificationDal _notificationDal;
        private readonly IUserService _userService;

        public NotificationManager(INotificationDal notificationDal, IUserService userService)
        {
            _notificationDal = notificationDal;
            _userService = userService;
        }

        public IResult Add(NotificationAddDto notificationAddDto)
        {
            var result = BusinessRules.Run(CheckIfUserExists(notificationAddDto.UserId));

            if (result != null)
            {
                return result;
            }

            var notification = new Notification
            {
                UserId = notificationAddDto.UserId,
                Message = notificationAddDto.Message,
                CreatedAt = DateTime.Now,
                IsRead = false
            };

            _notificationDal.Add(notification);
            return new SuccessResult(Messages.NotificationAdded);
        }

        public IResult Update(NotificationUpdateDto notificationUpdateDto)
        {
            var notification = _notificationDal.Get(n => n.Id == notificationUpdateDto.Id);
            if (notification == null)
            {
                return new ErrorResult(Messages.NotificationNotFound);
            }

            notification.IsRead = notificationUpdateDto.IsRead;
            _notificationDal.Update(notification);
            return new SuccessResult(Messages.NotificationUpdated);
        }

        public IResult Delete(int notificationId)
        {
            var notification = _notificationDal.Get(n => n.Id == notificationId);
            if (notification == null)
            {
                return new ErrorResult(Messages.NotificationNotFound);
            }

            _notificationDal.Delete(notification);
            return new SuccessResult(Messages.NotificationDeleted);
        }

        [CacheAspect]
        [PerformanceAspect(1)]
        public IDataResult<List<NotificationDto>> GetAll()
        {
            var notifications = _notificationDal.GetAllNotificationsWithUsers();
            return notifications;
        }

        [CacheAspect]
        [PerformanceAspect(1)]
        public IDataResult<List<Notification>> GetAllByUserId(int userId)
        {
            var notifications = _notificationDal.GetAllByUserId(userId);
            return notifications;
        }


        //**************Business Rules**************
        private IResult CheckIfUserExists(int userId)
        {
            var user = _userService.GetById(userId).Data;

            if (user == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }

            return new SuccessResult();
        }
    }
}
