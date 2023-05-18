using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using VS.core.Request;
using VS.Core.Business.Interface;

namespace vsrolAPI2022.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]

    public class SmsController : BaseController
    {
        private readonly IReportBussiness _impactBusiness;

        private readonly ISmsBussiness smsBusiness;
        private IHandleReportBussiness _handleReportBussiness;

        public SmsController(
            IReportBussiness campagnBusiness,
            IUserBusiness userBusiness,
            ISmsBussiness _callLogBussiness,
            IHandleReportBussiness handleReportBussiness
        ) : base(userBusiness)
        {
            _impactBusiness = campagnBusiness;
            smsBusiness = _callLogBussiness;
            _handleReportBussiness = handleReportBussiness;
        }

        private static bool AcceptAllCertifications(
            object sender,
            System.Security.Cryptography.X509Certificates.X509Certificate certification,
            System.Security.Cryptography.X509Certificates.X509Chain chain,
            System.Net.Security.SslPolicyErrors sslPolicyErrors
        )
        {
            return true;
        }

        [HttpPost("~/api/sms/send")]
        public async Task<IResult> Sendsms(SmsRequest _input)
        {
            var _userCurrent = GetCurrentUser();
            var linecode = _userCurrent.LineCode;
            if (string.IsNullOrEmpty(linecode))
            {
                return Results.BadRequest("Dữ liệu không hợp lệ");
            }
            if (linecode.Length < 4)
            {
                return Results.BadRequest("không gọi được");
            }

            if (string.IsNullOrEmpty(_input.PhoneNumber))
            {
                return Results.BadRequest("Dữ liệu không hợp lệ");
            }
            var linkUrl = "https://192.168.1.251/api/send_sms";
            Uri myUri = new Uri(linkUrl);
            WebRequest myWebRequest = HttpWebRequest.Create(myUri);

            HttpWebRequest myHttpWebRequest = (HttpWebRequest)myWebRequest;

            myHttpWebRequest.ContentType = "application/json";
            myHttpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(myHttpWebRequest.GetRequestStream()))
            {
                var bodyRequest = new SmsContentGateway()
                {
                    text = _input.ContentSms,
                    param = new List<ObjectPhone>()
                    {
                        new ObjectPhone() { number = _input.PhoneNumber }
                    }
                };

                JsonSerializer.CreateDefault().Serialize(streamWriter, bodyRequest);
            }

            NetworkCredential myNetworkCredential = new NetworkCredential(
                "vietstar",
                "Vietstar@2021"
            );

            CredentialCache myCredentialCache = new CredentialCache();
            myCredentialCache.Add(myUri, "Digest", myNetworkCredential);

            myHttpWebRequest.PreAuthenticate = true;
            myHttpWebRequest.Credentials = myCredentialCache;

            ServicePointManager.ServerCertificateValidationCallback =
                new System.Net.Security.RemoteCertificateValidationCallback(
                    AcceptAllCertifications
                );

            WebResponse myWebResponse = myWebRequest.GetResponse();
            Stream responseStream = myWebResponse.GetResponseStream();

            StreamReader myStreamReader = new StreamReader(responseStream, Encoding.Default);

            string pageContent = myStreamReader.ReadToEnd();

            responseStream.Close();

            myWebResponse.Close();

            var objectReponse = JsonConvert.DeserializeObject<SmsReponse>(pageContent);

            var itemInsert = new VS.Core.dataEntry.User.SmsMessage()
            {
                Phone = _input.PhoneNumber,
                CreateAt = DateTime.Now,
                LineCode = linecode,
                ProfileId = _input.ProfileId,
                NoAgree = _input.NoAgree,
                TimeBuisiness = DateTime.Now,
                VendorId = 9000,
                CreatedBy = _userCurrent.CreatedBy,
                UserId = _userCurrent.Id,
                Content = _input.ContentSms,
                Deleted = false,
                error_code = objectReponse.error_code,
                sms_in_queue = objectReponse.sms_in_queue,
                Status = "0",
                task_id = objectReponse.task_id,
                sn = objectReponse.sn,
                UpdateAt = DateTime.Now
            };
            await smsBusiness.Add(itemInsert);

            return Results.Ok(new { success = true, data = objectReponse });
        }
    }
}
