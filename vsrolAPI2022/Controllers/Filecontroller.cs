
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
                    urlDow = "http://192.168.1.10:3002/api/getFileAudio?filePath=" + filePath;
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


        [AllowAnonymous]
        [HttpGet("~/api/file/getaudio10")]
        public async Task<FileResult> getaudio10(string? filePath)
        {


            //await _campagnBusiness.ResetCase();
            string urlDow = "http://192.168.1.10:3002/api/getFileAudio?filePath=" + filePath;

            using (var net = new System.Net.WebClient())
            {
                var fileName = Path.GetFileName(filePath);

                byte[] data;
                try
                {
                    data = net.DownloadData(urlDow);
                    return File(data, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
                }
                catch (Exception)
                {
                    return null;

                }
            }
            return null;

        }

        [AllowAnonymous]
        [HttpGet("~/api/file/getaudio12")]
        public async Task<FileResult> getaudio12(string? filePath)
        {


            //await _campagnBusiness.ResetCase();
            string urlDow = "http://192.168.1.12:3002/api/getFileAudio?filePath=" + filePath;

            using (var net = new System.Net.WebClient())
            {
                var fileName = Path.GetFileName(filePath);

                byte[] data;
                try
                {
                    data = net.DownloadData(urlDow);
                    return File(data, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
                }
                catch (Exception)
                {
                    return null;

                }
            }
            return null;

        }


        [AllowAnonymous]
        [HttpGet("~/api/file/getaudio09")]
        public async Task<FileResult> getaudio09(string? filePath)
        {


            //await _campagnBusiness.ResetCase();
            string urlDow = "http://192.168.1.9:3002/api/getFileAudio?filePath=" + filePath;

            using (var net = new System.Net.WebClient())
            {
                var fileName = Path.GetFileName(filePath);

                byte[] data;
                try
                {
                    data = net.DownloadData(urlDow);
                    return File(data, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
                }
                catch (Exception)
                {
                    return null;

                }
            }
            return null;

        }

        [AllowAnonymous]
        [HttpGet("~/api/file/getaudio151")]
        public async Task<FileResult> getaudio151(string? filePath)
        {


            //await _campagnBusiness.ResetCase();
            string urlDow = "http://192.168.1.151:3002/api/getFileAudio?filePath=" + filePath;

            using (var net = new System.Net.WebClient())
            {
                var fileName = Path.GetFileName(filePath);

                byte[] data;
                try
                {
                    data = net.DownloadData(urlDow);
                    return File(data, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
                }
                catch (Exception)
                {
                    return null;

                }
            }
            return null;

        }
    }
}