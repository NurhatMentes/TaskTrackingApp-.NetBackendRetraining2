using Core.DataAccess.EntityFramework;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfMessageDal : EfEntityRepositoryBase<Message, TaskTrackingAppDBContext>, IMessageDal
    {
        public IDataResult<List<MessageDto>> GetAllMessages()
        {
            using (var context = new TaskTrackingAppDBContext())
            {
                var result = from message in context.Messages
                             join user in context.Users on message.UserId equals user.Id
                             join chatRoom in context.ChatRooms on message.ChatRoomId equals chatRoom.Id
                             select new MessageDto
                             {
                                 Id = message.Id,
                                 Content = message.Content,
                                 SendAt = message.SendAt,
                                 MessageSenderName = user.FirstName + " " + user.LastName,
                                 ChatRoomName = chatRoom.Name,
                                 SenderMail = user.Email
                             };

                return new SuccessDataResult<List<MessageDto>>(result.ToList());
            }
        }
        public IDataResult<List<MessageDto>> GetMessagesByChatRoomId(int chatRoomId)
        {
            using (var context = new TaskTrackingAppDBContext())
            {
                var result = from message in context.Messages
                             join chatRoom in context.ChatRooms on message.ChatRoomId equals chatRoom.Id
                             join user in context.Users on message.UserId equals user.Id
                             where message.ChatRoomId == chatRoomId
                             select new MessageDto
                             {
                                 Id = message.Id,
                                 ChatRoomName = chatRoom.Name,
                                 MessageSenderName = user.FirstName + " " + user.LastName,
                                 Content = message.Content,
                                 SendAt = message.SendAt,
                                 SenderMail = user.Email
                             };

                return new SuccessDataResult<List<MessageDto>>(result.ToList());
            }
        }
    }
}
