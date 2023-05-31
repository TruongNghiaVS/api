using VS.core.Request;
using VS.Core.dataEntry.User;

namespace VS.Core.Business.Interface
{
    public interface IDpdManagementBussiness : IGenericBussine<Dpd>
    {
        Task<Dpd> Getbyid(string Id);
        Task<DpdReponse> GetALl(DPDRequest request);

    }
}
