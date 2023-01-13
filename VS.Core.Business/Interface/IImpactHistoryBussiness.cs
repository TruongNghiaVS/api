using VS.core.Request;
using VS.Core.dataEntry.Campagn;


namespace VS.Core.Business.Interface
{
    public interface IImpactHistoryBussiness : IGenericBussine<ImpactHistory>
    {
        Task<ImpactHistory> Getbyid(string Id);
        //Task<bool> CheckDuplicate(string code);
        Task<ImpactHistoryReponse> GetALl(ImpactHistorySerarchRequest request);

    }
}
