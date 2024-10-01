using Core.DataAccess;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Abstract
{
    public interface IChatRoomDal :IEntityRepository<ChatRoom>
    {
        IDataResult<List<ChatRoomDetailDto>> GetAll();
    }
}
