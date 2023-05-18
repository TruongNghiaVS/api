using VS.Core.dataEntry;

namespace VS.Core.Business.Interface
{
    public interface IGenericBussine<T> where T : class, ITypeWithId
    {
        Task<T> GetByIdAsync(string id);

        Task<int> Add(T entity);
        Task<int> UpdateAsyn(T entity);
        Task Delete(T entity);
    }
}
