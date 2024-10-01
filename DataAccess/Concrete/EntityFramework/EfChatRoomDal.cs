using Core.DataAccess.EntityFramework;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfChatRoomDal : EfEntityRepositoryBase<ChatRoom, TaskTrackingAppDBContext>, IChatRoomDal
    {
        public IDataResult<List<ChatRoomDetailDto>> GetAll()
        {
            using (var context = new TaskTrackingAppDBContext())
            {
                var chatRooms = from cr in context.ChatRooms
                                join u in context.Users on cr.CreatedByUserId equals u.Id into userGroup
                                from u in userGroup.DefaultIfEmpty()
                                join p in context.Projects on cr.RelatedProjectId equals p.Id into projectGroup
                                from p in projectGroup.DefaultIfEmpty()

                                select new ChatRoomDetailDto
                                {
                                    Id = cr.Id,
                                    Name = cr.Name,
                                    RelatedProjectName = p != null ? p.Name : null,
                                    CreatedAt = cr.CreatedAt,
                                    CreatedByUserName = u != null ? u.FirstName + " " + u.LastName : " ",
                                    CreatedByUserEmail = u != null ? u.Email : null
                                };

                return new SuccessDataResult<List<ChatRoomDetailDto>>(chatRooms.ToList());
            }
        }
    }
}
