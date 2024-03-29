﻿using VS.core.Request;
using VS.Core.Business.Model;
using VS.Core.dataEntry.User;

namespace VS.Core.Business.Interface
{
    public interface IMasterDataBussiness : IGenericBussine<MasterData>
    {
        Task<MasterData> Getbyid(string Id);
        Task<bool> CheckDuplicate(string code, string vendorId = null);
        Task<MasterData> GetbyCode(string code, string vendorId = null);
        Task<MasterDataReponse> GetALl(MaterDataRequest request);

        Task<MasterDataReponse> GetDataForExport(MaterDataRequest request);

        Task<bool> HandleImport(MasterDataImportRequest request, Account user = null);

    }
}
