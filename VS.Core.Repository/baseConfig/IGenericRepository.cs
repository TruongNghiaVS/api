using VS.Core.dataEntry;

namespace VS.Core.Repository.baseConfig
{
    public interface IGenericRepository<T> where T : class, ITypeWithId
    {
        Task<T> GetById(string id);
        Task<int> Add(T entity);

        Task<int> Update(T entity);
        Task Delete(T entity);
    }



}
