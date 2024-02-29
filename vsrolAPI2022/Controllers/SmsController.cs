   using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using VS.core.API.model;
using VS.core.Request;
using VS.Core.Business.Interface;
using VS.Core.dataEntry.User;

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

        [AllowAnonymous]
        [HttpPost("~/api/sms/add")]
        public async Task<IResult> Add(SmsAdd employeeAdd)
        {
            var user = GetCurrentUser();
            var lineCode = "";
            if (user != null)
            {
                lineCode = user.LineCode;
            }
            var type = 0;

            var templateNumberViettel = "096|097|098|086|030|031|032|033|034|035|036|037|038|039";
            var templateNumberVina = "091|094|088|083|084|085|081|082|087";
            var templateNumberMobi = "090|093|089|070|079|077|076|078|092|018|056|058|052|053";

            var numberBegin = employeeAdd.PhoneNumber.Substring(0, 3);

            if (templateNumberMobi.Contains(numberBegin))
            {
                type = 4;
            }

            else if (templateNumberVina.Contains(numberBegin))
            {
                type = 1;
            }
            else if (templateNumberViettel.Contains(numberBegin))
            {
                type = 2;
            }
            else
            {

                type = 3;
            }


            var itemInser = new SmsMessage2()
            {
                Content = employeeAdd.ContentSms,
                LineCode = lineCode,
                Netword = type,
                NoAgree = employeeAdd.NoAgree,
                CreateAt = DateTime.Now,
                Phone = employeeAdd.PhoneNumber,
                Status = "0",
                ProfileId = employeeAdd.ProfileId,
                TimeBuisiness = DateTime.Now,
                UserId = user.Id,
                VendorId = user.VendorId
            };
            var resultAdd = await smsBusiness.AddSms2(itemInser);

            return Results.Ok(resultAdd);
        }





        [HttpPost("~/api/sms/getAll")]
        public async Task<IResult> getAll(SmsSearchInput request)
        {
            var user = GetCurrentUser();
            int? vendorId = user.VendorId;
            if (user.RoleId == "4")
            {
                vendorId = int.Parse(user.Id);
            }

            var searchRequest = new SmsGetallRequest()
            {
                Limit = request.Limit,
                Userid = user.Id,
                VendorId = vendorId,
                Page = request.Page



            };
            var resultSearch = await smsBusiness.GetALl(searchRequest);
            return Results.Ok(resultSearch);
        }

        [AllowAnonymous]
        [HttpGet("~/api/sms/handlerMessage")]
        public async Task<IResult> HandlerMessage()
        {

            var resltViettleSearch = new SmsGetHandleRequest()
            {
                Limit = 2,
                Netword = 1

            };
            var resultSearchViettel = await smsBusiness.GetAllHandle(resltViettleSearch); var dataViettel = resultSearchViettel.Data;

            foreach (var item in resultSearchViettel.Data)
            {
                var itemMessage = item as SmsMessage2;

                var resultCheck = await smsBusiness.CheckExitsCall(itemMessage);
                if (resultCheck < 1)
                {
                    itemMessage.Status = "2999";
                    await smsBusiness.UpdateSms2(itemMessage);
                    continue;
                }
                await SendSmshanlder(itemMessage);
            }
            var resltMobiVina = new SmsGetHandleRequest()
            {
                Limit = 5,
                Netword = 2

            };
            var resultSearchMobiVina = await smsBusiness.GetAllHandle(resltMobiVina);
            var dataMobi = resultSearchMobiVina.Data;
            foreach (var item in dataMobi)
            {
                var itemMessage = item as SmsMessage2;
                var resultCheck = await smsBusiness.CheckExitsCall(itemMessage);
                if (resultCheck < 1)
                {
                    itemMessage.Status = "2999";
                    await smsBusiness.UpdateSms2(itemMessage);
                    continue;
                }
                await SendSmshanlder(itemMessage);
            }
            return Results.Ok(new { success = true });
        }


        private async Task<IResult> SendSmshanlder(SmsMessage2 _input)
        {


            var linkUrl = "https://192.168.1.249/api/send_sms";
            Uri myUri = new Uri(linkUrl);
            WebRequest myWebRequest = HttpWebRequest.Create(myUri);

            HttpWebRequest myHttpWebRequest = (HttpWebRequest)myWebRequest;

            myHttpWebRequest.ContentType = "application/json";
            myHttpWebRequest.Method = "POST";

            var templateNumberViettel = "096|097|098|086|030|031|032|033|034|035|036|037|038|039";
            var templateNumberVina = "091|094|088|083|084|085|081|082|087";
            var templateNumberMobi = "090|093|089|070|079|077|076|078|092|018|056|058|052|053";

            var numberBegin = _input.Phone.Substring(0, 3);

            int[] portArray = new int[] { };
            if (templateNumberMobi.Contains(numberBegin))
            {
                portArray = new int[] { 0, 1, 2, 3, 4, 5, 6, 7 };
            }

            else if (templateNumberVina.Contains(numberBegin))
            {
                portArray = new int[] { 0, 1, 2, 3, 4, 5, 6, 7 };
            }
            else if (templateNumberViettel.Contains(numberBegin))
            {
                portArray = new int[] { 8, 9, 10, 11, 12, 13, 14, 15 };
            }
            else
            {

                portArray = new int[] { 0, 1, 2, 3, 4, 5, 6, 7 };
            }

            using (var streamWriter = new StreamWriter(myHttpWebRequest.GetRequestStream()))
            {
                var bodyRequest = new SmsContentGateway()
                {
                    text = _input.Content,
                    port = portArray,
                    param = new List<ObjectPhone>()
                    {
                        new ObjectPhone() { number = _input.Phone }
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

            var itemInsert = new SmsMessage()
            {
                Phone = _input.Phone,
                CreateAt = DateTime.Now,
                LineCode = _input.LineCode,
                ProfileId = _input.ProfileId,
                NoAgree = _input.NoAgree,
                TimeBuisiness = DateTime.Now,
                VendorId = _input.VendorId,
                CreatedBy = _input.CreatedBy,
                UserId = _input.Id,
                Content = _input.Content,
                Deleted = false,
                error_code = objectReponse.error_code,
                sms_in_queue = objectReponse.sms_in_queue,
                Status = "0",
                task_id = objectReponse.task_id,
                sn = objectReponse.sn,
                UpdateAt = DateTime.Now
            };
            await smsBusiness.Add(itemInsert);
            _input.Status = objectReponse.error_code.ToString();

            await smsBusiness.UpdateSms2(_input);

            return Results.Ok(new { success = true });
        }


        [HttpPost("~/api/sms/send")]
        public async Task<IResult> Sendsms(SmsRequest _input)
        {

            return await this.Add(new SmsAdd()
            {
                ContentSms = _input.ContentSms,
                NoAgree = _input.NoAgree,
                PhoneNumber = _input.PhoneNumber,
                ProfileId = _input.ProfileId

            });
            var _userCurrent = GetCurrentUser();
            var linecode = _userCurrent.LineCode;
            var vendorId = _userCurrent.VendorId;
            if (string.IsNullOrEmpty(linecode))
            {
                linecode = "8888";
            }
            //if (linecode != "8888")
            //{
            //    return Results.BadRequest("Không gọi được");
            //}

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
                portArray = new int[] { 0, 1, 2, 3, 4, 5, 6, 7 };
            }

            else if (templateNumberVina.Contains(numberBegin))
            {
                portArray = new int[] { 0, 1, 2, 3, 4, 5, 6, 7 };
            }
            else if (templateNumberViettel.Contains(numberBegin))
            {
                portArray = new int[] { 8, 9, 10, 11, 12, 13, 14, 15 };
            }
            else
            {

                portArray = new int[] { 0, 1, 2, 3, 4, 5, 6, 7 };
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
