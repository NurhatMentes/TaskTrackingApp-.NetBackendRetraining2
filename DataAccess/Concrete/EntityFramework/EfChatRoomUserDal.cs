using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
namespace DataAccess.Concrete.EntityFramework
{
    public class EfChatRoomUserDal : EfEntityRepositoryBase<ChatRoomUser, TaskTrackingAppDBContext>, IChatRoomUserDal
    {
        public DataResult<List<ChatRoomUserDto>> GetAllChatRoomsWithUsers()
        {
            using (var context = new TaskTrackingAppDBContext())
            {
                var result = from chatRoomUser in context.ChatRoomUsers
                             join chatRoom in context.ChatRooms on chatRoomUser.ChatRoomId equals chatRoom.Id
                             join user in context.Users on chatRoomUser.UserId equals user.Id
                             select new ChatRoomUserDto
                             {
                                 ChatRoomName = chatRoom.Name, 
                                 UserName = user.FirstName+""+user.LastName,         
                                 UserEmail = user.Email,
                                 OnlineStatus = user.OnlineStatus
                             };

                return new SuccessDataResult<List<ChatRoomUserDto>>(result.ToList());
            }
        }

        public DataResult<List<ChatRoomUserDto>> GetAllChatRoomsWithUsersByChatRoomId(int chatRoomId)
        {
            using (var context = new TaskTrackingAppDBContext())
            {
                var result = from chatRoomUser in context.ChatRoomUsers
                             join chatRoom in context.ChatRooms on chatRoomUser.ChatRoomId equals chatRoom.Id
                             where chatRoom.Id == chatRoomId
                             select new ChatRoomUserDto
                             {
                                 ChatRoomName = chatRoom.Name,
                                 UserName = chatRoomUser.User.FirstName + "" + chatRoomUser.User.LastName,
                                 UserEmail = chatRoomUser.User.Email,
                                 OnlineStatus = chatRoomUser.User.OnlineStatus
                             };

            

                if (!result.Any())
                {
                    return new ErrorDataResult<List<ChatRoomUserDto>>("Sohbet odaları bulunamadı");
                }

                return new SuccessDataResult<List<ChatRoomUserDto>>(result.ToList());
            }
        }

        public DataResult<List<ChatRoomUserDto>> GetAllChatRoomsWithUsersByUserId(int userId)
        {
            using (var context = new TaskTrackingAppDBContext())
            {
                var result = from chatRoomUser in context.ChatRoomUsers
                             join chatRoom in context.ChatRooms on chatRoomUser.ChatRoomId equals chatRoom.Id
                             join user in context.Users on chatRoomUser.UserId equals user.Id
                             where chatRoomUser.UserId == userId    
                             select new ChatRoomUserDto
                             {
                                 ChatRoomName = chatRoom.Name,
                                 UserName = user.FirstName + "" + user.LastName,
                                 UserEmail = user.Email,
                                 OnlineStatus = user.OnlineStatus
                             };

                if (!result.Any())
                {
                    return new ErrorDataResult<List<ChatRoomUserDto>>("Kullanıcı bulunamadı");
                }

                return new SuccessDataResult<List<ChatRoomUserDto>>(result.ToList());
            }
        }
    }
}
