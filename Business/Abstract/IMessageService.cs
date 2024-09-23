using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IMessageService
    {
        IResult Add(MessageAddDto messageCreateDto);
        IResult Update(MessageUpdateDto messageUpdateDto);
        IResult Delete(int id);
        IDataResult<MessageDto> GetById(int id);
        IDataResult<List<MessageDto>> GetAll();
        IDataResult<List<MessageDto>> GetMessagesByChatRoomId(int chatRoomId);
    }
}
