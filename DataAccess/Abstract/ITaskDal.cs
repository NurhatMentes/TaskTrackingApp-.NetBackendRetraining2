using Core.DataAccess;

namespace DataAccess.Abstract
{
    public interface ITaskDal : IEntityRepository<Entities.Concrete.Task>
    {
    }

}
