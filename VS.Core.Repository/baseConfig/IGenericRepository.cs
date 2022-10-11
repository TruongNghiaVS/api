using VS.Core.dataEntry;

namespace VS.Core.Repository.baseConfig
{
    public interface IGenericRepository<T> where T : class, ITypeWithId
    {
        Task<T> GetByIdAsync(string id);
        Task<int> AddAsync(T entity);

        Task<int> UpdateAsyn(T entity);
        Task Delete(T entity);
    }



}
