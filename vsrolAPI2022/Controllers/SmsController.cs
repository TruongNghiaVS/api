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
            var vendorId = _userCurrent.VendorId;
            if (string.IsNullOrEmpty(linecode))
            {
                linecode = "8888";
            }


            if (string.IsNullOrEmpty(_input.PhoneNumber))
            {
                return Results.BadRequest("Dữ liệu không hợp lệ");
            }

            if (string.IsNullOrEmpty(_input.ContentSms))
            {
                return Results.BadRequest("Dữ liệu không hợp lệ");
            }
            var linkUrl = "https://192.168.1.249/api/send_sms";
            Uri myUri = new Uri(linkUrl);
            WebRequest myWebRequest = HttpWebRequest.Create(myUri);

            HttpWebRequest myHttpWebRequest = (HttpWebRequest)myWebRequest;

            myHttpWebRequest.ContentType = "application/json";
            myHttpWebRequest.Method = "POST";

            var templateNumberViettel = "096|097|098|086|030|031|032|033|034|035|036|037|038|039";
            var templateNumberVina = "091|094|088|083|084|085|081|082|087";
            var templateNumberMobi = "090|093|089|070|079|077|076|078|092|018|056|058|052|053";

            var numberBegin = _input.PhoneNumber.Substring(0, 3);

            int[] portArray = new int[] { };
            if (templateNumberMobi.Contains(numberBegin))
            {
                portArray = new int[] { 0, 1, 2, 3, 4, 5 };
            }

            else if (templateNumberVina.Contains(numberBegin))
            {
                portArray = new int[] { 6, 7, 8, 9, 10 };
            }
            else if (templateNumberViettel.Contains(numberBegin))
            {
                portArray = new int[] { 11, 12, 13, 14, 15 };
            }
            else
            {

                portArray = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            }

            using (var streamWriter = new StreamWriter(myHttpWebRequest.GetRequestStream()))
            {
                var bodyRequest = new SmsContentGateway()
                {
                    text = _input.ContentSms,
                    port = portArray,
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
                VendorId = vendorId,
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
