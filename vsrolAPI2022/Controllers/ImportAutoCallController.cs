using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml;
using System.Net.Http.Headers;
using VS.core.API.model;
using VS.Core.Business.Interface;
using VS.Core.dataEntry.User;


namespace vsrolAPI2022.Controllers
{
    [ApiController]

    [Route("[controller]")]
    public class ImportAutoCallController : BaseImportController
    {

        private readonly ICampagnBussiness _campagnBusiness;
        private Queue<TestAutoItem> Data { get; set; }
        public ImportAutoCallController(ICampagnBussiness campagnBusiness,
            IUserBusiness userBusiness) : base(userBusiness)
        {

            _campagnBusiness = campagnBusiness;
            Data = new Queue<TestAutoItem>();
        }

        [HttpPost("~/api/ImportAutoCall/importCase")]
        public async Task<IResult> ImportCase([FromForm] CampanginDataImport request)
        {
            var userLogin = new Account()
            {
                Id = "1"
            };
            var fileRequest = request.FileData;
            if (fileRequest == null || fileRequest.Count == 0)
            {
                return Results.BadRequest("No error report");
            }
            var fileHandler = fileRequest.FirstOrDefault();
            if (fileHandler == null)
            {
                return Results.BadRequest("No error report");
            }
            await using (MemoryStream ms = new MemoryStream())
            {
                await fileHandler.CopyToAsync(ms);
                using (ExcelPackage package = new ExcelPackage(ms))
                {
                    ExcelWorksheet workSheet = package.Workbook.Worksheets["Sheet1"];
                    int totalRows = workSheet.Dimension.Rows;
                    for (int i = 2; i <= totalRows; i++)
                    {
                        var noAgree = ReadvalueStringExcel(workSheet, i, 2);
                        var phoneNumber = ReadvalueStringExcel(workSheet, i, 3);
                        var thamchieu1 = ReadvalueStringExcel(workSheet, i, 4);
                        var thamchieu2 = ReadvalueStringExcel(workSheet, i, 5);
                        var thamchieu3 = ReadvalueStringExcel(workSheet, i, 6);
                        if (string.IsNullOrEmpty(noAgree))
                        {
                            continue;
                        }
                        Data.Enqueue(new TestAutoItem()
                        {
                            NoAgree = noAgree,
                            Phone = phoneNumber,
                            Phone1 = thamchieu1,
                            Phone2 = thamchieu2,
                            Phone3 = thamchieu3
                        });
                    }
                }
            }
            while (Data.Count > 0)
            {
                var hs = Data.Dequeue();
                if (!string.IsNullOrEmpty(hs.Phone))
                {
                    await AutoCall(hs.Phone);
                }
                if (!string.IsNullOrEmpty(hs.Phone1))
                {
                    await AutoCall(hs.Phone1);
                }
                if (!string.IsNullOrEmpty(hs.Phone2))
                {
                    await AutoCall(hs.Phone2);
                }
                if (!string.IsNullOrEmpty(hs.Phone3))
                {
                    await AutoCall(hs.Phone3);
                }
                if (Data.Count % 20 == 0)
                {
                    using (StreamWriter writetext = new StreamWriter("C:\\Users\\Admin\\Desktop\\test.txt", true))
                    {
                        writetext.WriteLine("thread dung " + Data.Count);
                    }
                    Thread.Sleep(5000);
                }
            }
            return Results.Ok(Data.Count);
        }
        private async Task<bool> AutoCall(string phoneNumber)
        {
            var data = new StringContent(JsonConvert.SerializeObject(new
            {
                phone_number = phoneNumber,
                trunk = "vs_auto"
            }));
            data.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var linkUrl = "http://192.168.1.151";
            var accessToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpYXQiOjE3Mjg2MzgxOTYsImV4cCI6MTcyODY0MTc5NiwiaXNzIjoieW91cl9pc3N1ZXIiLCJkYXRhIjp7InVzZXJJZCI6InZpZXRzdGFydSIsInVzZXJOYW1lIjoidmlldHN0YXJ1In19.6bhpQIgKGY19IsXZKuCmP0kD-SReEcZDWmMwtLvNGTs";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(linkUrl);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                var reponse = await client.PostAsync("autocall/make_call", data);
                var result = await reponse.Content.ReadAsStringAsync();
                using (StreamWriter writetext = new StreamWriter("C:\\Users\\Admin\\Desktop\\test.txt", true))
                {
                    writetext.WriteLine(result);
                }
            }
            return true;
        }

    }
}