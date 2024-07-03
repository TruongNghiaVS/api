using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using VS.core.Request;
using VS.Core.Business.Interface;
using VS.Core.Repository.Model;

namespace vsrolAPI2022.Controllers
{

    public class TrackingCallApi
    {

        public string Response { get; set; }
        public string Actionid { get; set; }
        public string Message { get; set; }

        public List<string> Output { get; set; }
    }

    public class TrackingCallAs
    {
        public string Channel { get; set; }
        public string Context { get; set; }
        public string Extension { get; set; }
        public string Prio { get; set; }
        public string State { get; set; }
        public string Application { get; set; }
        public string Data { get; set; }
        public string CallerID { get; set; }
        public string Duration { get; set; }
        public string Accountcode { get; set; }
        public string PeerAccount { get; set; }
    }
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class TrackingCallController : BaseController
    {
        private readonly IGroupEmpBussiness _groupEmpBussiness;
        public TrackingCallController(
       IUserBusiness userBusiness,
       IGroupEmpBussiness groupEmpBussiness,

       IReportTalkTimeGroupByDayBussiness reportTalkTimeGroupByDayBussiness) : base(userBusiness)
        {

            _reportTalkTimeGroupByDayBussiness = reportTalkTimeGroupByDayBussiness;
            _groupEmpBussiness = groupEmpBussiness;
        }
        private static int DataList = 0;
        private static DateTime? Start;
        private IReportTalkTimeGroupByDayBussiness _reportTalkTimeGroupByDayBussiness;
        //public static List<TrackingCallAs> ListDataResut = new List<TrackingCallAs>();

        public static GetAllTrackingGroupByLineCodeReponse dataGroupCheck;

        public static List<TrackingRecordGroupByLineCodeIndexModel> OutPutData = new List<TrackingRecordGroupByLineCodeIndexModel>();





        [AllowAnonymous]
        [HttpGet("~/api/trackingCall/getValue")]
        public async Task<IResult> GetValue()
        {
            DataList += 1;
            return Results.Ok(new
            {
                DataList
            });
        }

        [HttpPost("~/api/trackingCall/getAll")]
        public async Task<IResult> getAll(GetAllRecordGroupByLineCodeRequest _input)
        {
            var currentUser = GetCurrentUser();
            if (currentUser.RoleId == "2" || currentUser.RoleId == "4" || currentUser.RoleId == "3")
            {

            }
            else
            {
                _input.LineCode = currentUser.LineCode;
            }

            int? VendorId = null;
            if (currentUser.RoleId == "4")
            {
                VendorId = int.Parse(currentUser.Id);
            }
            _input.VendorId = VendorId;
            _input.UserId = currentUser.Id;

            if (OutPutData.Count < 1)
            {

                await LoadData();

            }
            if (DataMember.Count < 1)
            {
                await LoadAllMember();
            }

            List<int> GroupMemer = new List<int>();
            var userIdInput = int.Parse(currentUser.Id);
            if (userIdInput == 2366)
            {
                GroupMemer.Add(23);
            }
            else if (userIdInput == 3447)
            {
                GroupMemer.Add(28);
            }
            else if (userIdInput == 3463)
            {
                GroupMemer.Add(29);
            }
            else if (userIdInput == 3471)
            {
                GroupMemer.Add(30);
            }
            else if (userIdInput == 3487)
            {
                GroupMemer.Add(31);
            }
            else if (userIdInput == 3509)
            {
                GroupMemer.Add(32);
            }
            else if (userIdInput == 3518)
            {
                GroupMemer.Add(33);
            }
            else if (userIdInput == 3518)
            {
                GroupMemer.Add(33);
            }
            else if (userIdInput == 3640)
            {

                GroupMemer.Add(36);
                GroupMemer.Add(35);
            }
            else if (userIdInput == 3614)
            {
                GroupMemer.Add(36);
                GroupMemer.Add(35);

            }

            else if (userIdInput == 1)
            {
                //GroupMemer.Add(33);
                //GroupMemer.Add(28);
                //GroupMemer.Add(29);
                //GroupMemer.Add(31);
                //GroupMemer.Add(32);
                //GroupMemer.Add(30);
                //GroupMemer.Add(23);
                //GroupMemer.Add(29);
            }
            else if (userIdInput == 3590)
            {
                GroupMemer.Add(34);
            }

            if (userIdInput == 1 || userIdInput == 3446)
            {
                return Results.Ok(new
                {
                    Data = OutPutData,
                    Total = OutPutData.Count

                });
            }
            else
            {
                var listLineCode = DataMember.Where(x => GroupMemer.Contains(x.Groupid)).Select(x => x.LineCode).ToArray();
                return Results.Ok(new
                {
                    Data = OutPutData.Where(x => listLineCode.Contains(x.LineCode)).ToList(),
                    Total = OutPutData.Count

                });
            }


        }


        public static List<GroupEmployeeViewIndexModel> DataMember = new List<GroupEmployeeViewIndexModel>();

        public async Task LoadAllMember()
        {
            var memberList = await _groupEmpBussiness.getMemberByGroup(new MemberGroupByIdRequest()
            {

                Limit = 1000

            });

            DataMember = memberList.Data as List<GroupEmployeeViewIndexModel>;

        }
        public async Task LoadData(int userId = 1)
        {
            var request = new GetAllRecordGroupByLineCodeRequest()
            {
                From = DateTime.Now,
                To = DateTime.Now,
                UserId = "1",
                TimeSelect = DateTime.Now
            };
            dataGroupCheck = await _reportTalkTimeGroupByDayBussiness.GetAllTracking

            (
                request
            );
            if (dataGroupCheck != null)
            {
                OutPutData = dataGroupCheck.Data as List<TrackingRecordGroupByLineCodeIndexModel>;
            }
            else
            {
                OutPutData = new List<TrackingRecordGroupByLineCodeIndexModel>();
            }
            await Task.FromResult(true);
        }



        public static bool IsValidPhoneNumber(string phoneCheck)
        {
            if (phoneCheck.Length < 9 || phoneCheck.Length > 12)
            {
                return false;
            }
            var regex = "^(0|84)(2(0[3-9]|1[0-6|8|9]|2[0-2|5-9]|3[2-9]|4[0-9]|5[1|2|4-9]|6[0-3|9]|7[0-7]|8[0-9]|9[0-4|6|7|9])|3[2-9]|5[5|6|8|9]|7[0|6-9]|8[0-6|8|9]|9[0-4|6-9])([0-9]{7})$";
            var phoneNumber = phoneCheck.Trim()
                .Replace("\"", "")
                .Replace(" ", "")
                .Replace("-", "")
                .Replace("(", "")
                .Replace(")", "");
            return Regex.Match(phoneNumber, regex).Success;
        }

        private bool IsValidChanel(string chanelCheck)
        {
            if (chanelCheck.Length != 4)
            {
                return false;
            }
            var regex = @"\d{4}";
            var phoneNumber = chanelCheck.Trim()
                .Replace(" ", "")
                .Replace("-", "")
                .Replace("(", "")
                .Replace(")", "");
            return Regex.Match(phoneNumber, regex).Success;
        }

        private bool IsValidTiemHour(string This)
        {
            var regex = "^(?:(?:([01]?\\d|2[0-3]):)?([0-5]?\\d):)?([0-5]?\\d)$";
            var phoneNumber = This.Trim()
                .Replace(" ", "")
                .Replace("-", "")
                .Replace("(", "")
                .Replace(")", "");
            return Regex.Match(phoneNumber, regex).Success;
        }
        [AllowAnonymous]
        [HttpGet("~/api/trackingCall/getListActive")]
        public async Task<IResult> GetListActive()
        {

            var data = new StringContent(JsonConvert.SerializeObject(new
            {

            }));
            data.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var linkUrl = "http://192.168.1.151:3002";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(linkUrl);
                var reponse = await client.PostAsync("api/chanelGetStatus", data);
                var result = await reponse.Content.ReadAsStringAsync();
                var resultPart = JsonConvert.DeserializeObject<TrackingCallApi>(result);
                var listData = new List<TrackingCallAs>();
                OutPutData.ForEach(c => c.IsCalling = false);
                foreach (var item in resultPart.Output)
                {
                    var cdrItem = Regex.Split(item.Replace("(Outgoing Line)", "(OutgoingLine)").Replace(" ", "       "), @"\s\s+");
                    var phoneNumber = "";
                    var chanel = "";
                    var timehour = "";
                    foreach (var item1 in cdrItem)
                    {
                        if (IsValidPhoneNumber(item1))
                        {
                            phoneNumber = item1;
                        }
                        if (IsValidChanel(item1))
                        {
                            chanel = item1;
                        }
                        if (IsValidTiemHour(item1))
                        {
                            timehour = item1;
                        }
                    }

                    var callidtemp = chanel;
                    if (!string.IsNullOrEmpty(phoneNumber))
                    {
                        callidtemp = phoneNumber;
                    }
                    string lineInput = chanel;
                    if (!string.IsNullOrEmpty(phoneNumber))
                    {
                        var tiemCall = MakeCallController.GlobalLogCall.Where(x => x.Phone == callidtemp)
                            .FirstOrDefault();
                        if (tiemCall != null)
                        {
                            lineInput = tiemCall.LineCode;
                        }
                    }

                    if (lineInput == "")
                    {
                        continue;
                    }

                    var itemGroupUpdate = OutPutData.Where(x => x.LineCode == lineInput).FirstOrDefault();
                    if (itemGroupUpdate == null)
                    {
                    }
                    else
                    {
                        itemGroupUpdate.DurationRealTime = timehour;
                        itemGroupUpdate.IsCalling = true;
                    }
                }
                return Results.Ok(true);

            }
        }







    }
}