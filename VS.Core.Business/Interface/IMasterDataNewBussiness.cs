using VS.core.Request;
using VS.Core.dataEntry.User;

namespace VS.Core.Business.Interface
{
    public interface IMasterDataNewBussiness : IGenericBussine<MasterDataNew>
    {
        Task<MasterDataNew> Getbyid(string Id);
        Task<bool> CheckDuplicate(string code);
        Task<MasterDataNewReponse> GetALl(MaterDataNewRequest request);

        Task<MasterDataInfoReponse> GetInfo(MaterDataNewRequest request);



    }
}
