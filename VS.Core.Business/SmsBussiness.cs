using Newtonsoft.Json;
using System.Net;
using System.Text;
using VS.core.Request;
using VS.Core.Business.Interface;
using VS.Core.dataEntry.User;
using VS.Core.Repository.baseConfig;

namespace VS.Core.Business
{
    public class SmsBussiness : BaseBusiness, ISmsBussiness
    {

        public SmsBussiness(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        public Task<int> Add(SmsMessage entity)
        {
            return _unitOfWork.SmsRe.Add(entity);
        }


        public Task<int> AddSms2(SmsMessage2 entity)
        {
            return _unitOfWork.SmsRe.AddSms2(entity);
        }


        public Task<int> UpdateSms2(SmsMessage2 entity)
        {
            return _unitOfWork.SmsRe.UpdateSms2(entity);
        }

        public Task<int> CheckExitsCall(SmsMessage2 entity)
        {

            return _unitOfWork.SmsRe.CheckExitsCall(entity);
        }


        public Task Delete(SmsMessage entity)
        {
            throw new NotImplementedException();
        }

        public Task<SmsGetallReponse> GetALl(SmsGetallRequest request)
        {
            return _unitOfWork.SmsRe.GetALl(request);
        }
        public Task<SmsGetallReponse> GetAllHandle(SmsGetHandleRequest request)
        {
            return _unitOfWork.SmsRe.GetAllHandle(request);
        }
        public Task<SmsMessage> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsyn(SmsMessage entity)
        {
            throw new NotImplementedException();
        }


        public async Task<bool> HandleSms()
        {
            var resltViettleSearch = new SmsGetHandleRequest()
            {
                Limit = 2,
                Netword = 1

            };
            var resultSearchViettel = await this.GetAllHandle(resltViettleSearch); var dataViettel = resultSearchViettel.Data;

            foreach (var item in resultSearchViettel.Data)
            {
                var itemMessage = item as SmsMessage2;
                await SendSmshanlder(itemMessage);
            }
            var resltMobiVina = new SmsGetHandleRequest()
            {
                Limit = 5,
                Netword = 2

            };
            var resultSearchMobiVina = await this.GetAllHandle(resltMobiVina);
            var dataMobi = resultSearchMobiVina.Data;
            foreach (var item in dataMobi)
            {
                var itemMessage = item as SmsMessage2;
                await SendSmshanlder(itemMessage);
            }
            return true;
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
        private async Task<bool> SendSmshanlder(SmsMessage2 _input)
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
            await this.Add(itemInsert);
            _input.Status = objectReponse.error_code.ToString();

            await this.UpdateSms2(_input);

            return true;
        }



    }
}
