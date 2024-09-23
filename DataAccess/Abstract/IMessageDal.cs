using Core.DataAccess;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System.Linq.Expressions;

namespace DataAccess.Abstract
{
    public interface IMessageDal : IEntityRepository<Message>
    {
        IDataResult<List<MessageDto>> GetAllMessages();
        IDataResult<List<MessageDto>> GetMessagesByChatRoomId(int chatRoomId);
    }
}
