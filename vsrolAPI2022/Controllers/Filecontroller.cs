
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VS.Core.Business.Interface;


namespace vsrolAPI2022.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class Filecontroller : BaseController
    {

        private readonly ICampagnBussiness _campagnBusiness;
        public Filecontroller(ICampagnBussiness campagnBusiness,
            IUserBusiness userBusiness) : base(userBusiness)
        {
            _campagnBusiness = campagnBusiness;
        }

        [AllowAnonymous]
        [HttpGet("~/api/file/getFileAudio")]
        public async Task<ActionResult> getFileAudio(string? filePath)
        {
            //await _campagnBusiness.ResetCase();
            string urlDow = "http://192.168.1.12:3002/api/getFileAudio?filePath=" + filePath;
            using (var net = new System.Net.WebClient())
            {

                byte[] data;
                try
                {
                    data = net.DownloadData(urlDow);
                }
                catch (Exception)
                {
                    urlDow = "http://192.168.1.9:3002/api/getFileAudio?filePath=" + filePath;
                    try
                    {
                        data = net.DownloadData(urlDow);

                    }
                    catch (Exception)
                    {

                        return null;
                    }

                }
                if (data == null)
                {
                    return null;
                }
                if (data.Length > 0)
                {
                    var content = new System.IO.MemoryStream(data);
                    var contentType = "APPLICATION/octet-stream";

                    return File(content, contentType);
                };
            }
            return null;

        }


    }
}