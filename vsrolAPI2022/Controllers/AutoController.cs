using Microsoft.AspNetCore.Mvc;
using VS.core.AutoCall;
using VS.Core.Business.Interface;

namespace vsrolAPI2022.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class AutoController : BaseController
    {
        private readonly IAutoBussiness _business;
        public AutoController(
            IAutoBussiness autoBussiness,
            IUserBusiness userBusiness
       ) : base(userBusiness)
        {
            _business = autoBussiness;
        }



    }
}

