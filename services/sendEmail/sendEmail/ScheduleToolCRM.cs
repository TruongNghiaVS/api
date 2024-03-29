using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace sendEmail
{
    public class ScheduleToolCRM
    {

        public ScheduleToolCRM()
        {
        }


        public async Task Run()
        {

            var data = new StringContent("nghia");
            data.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var linkUrl = "https://localhost:8098";
            using (var requestInfo = new HttpClient())
            {

                {
                    requestInfo.BaseAddress = new Uri(linkUrl);
                    var reponse = await requestInfo.GetAsync("api/job/CalculatingTalktime");
                    if (reponse.StatusCode == HttpStatusCode.OK)
                    {
                        Console.WriteLine("Suceess: " + DateTime.Now.ToString("yyyy-MM-dd: HH:MM:ss"));
                    }
                    else {
                        Console.WriteLine("false Time: " + DateTime.Now.ToString("yyyy-MM-dd: HH:MM:ss"));
                    }  
                }  

            }


        }

  
        public async Task SendEmailReport(Stream urlDowLoad)
        {
            using (SmtpClient client = new SmtpClient("mail92115.maychuemail.com", 587))
            {
                var monthText = DateTime.Now.ToString("yyyy.MM.dd");
                var subjectMail = "[" + monthText + "]" + "Báo cáo tình trạng toàn bộ hồ sơ hàng ngày";
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("noreply@hcmlawfirm.vn", "pQ84gtEMA9");
                string body = "<html> <style> span { display: block; } .titleName{ font-weight: bold; color: #0B5394; font-size: 1.3rem; } strong { color: #0B5394; } body { font-size: 1.1rem; } .dearsend { font-weight: bold; } .info { color: black !important; } li { list-style-type: none; margin-left: 10px; } .alert { color: red; } </style> <body> <br> <span class=\"dearsend\"> Dear bộ phân QC,</span><br> <span class=\"dearsend\"> Dear All </span> <br> <span> Bộ phận IT gửi file báo cáo tình trạng mới nhất, trạng thái  của toàn bộ các hồ sơ. Tính thời đợi điểm  lúc này (thời điểm gửi mail) </span> <p class=\"alert\">Chi tiết xem trong file đính kèm </p> <strong class=\"info\" > Một số lưu ý quan trọng </strong> <li> Trạng thái tác động  được ghi nhận, luôn luôn là mới nhất </li> <li>Chỉ xem dược tác động mới nhất, chứ không có xem đưỢc lịch sử tác động </li> <br><strong class=\"info\"> có gì sai xót, thì phản hồi qua mail support@hcmlawfirm.vn  </strong> <br> <br> <br> <span>Thanks & Best regards, </span> <br><span>---------------------------------------- </span> <br><strong>Bộ phận IT </strong><br> <span class=\"titleName\">Vietstar Group Joint Stock Company</span><br> <span> <strong>Head office: </strong> Floor 2nd, GMA Building, 307/6 Nguyen Van Troi, Ward 1, Tan Binh District, HCMC. </span><br> <span><strong>Tel: </strong>  (+84) 2871 000 555 </span> <br><span><strong>Mobile: </strong> 0866.823.799 – 028.7100555 </span> </body> </html>";
                MailMessage message = new MailMessage(
                                         "noreply@hcmlawfirm.vn", // From field
                                         "qc.khoi@hcmlawfirm.vn", // Recipient field
                                         subjectMail, // Subject of the email message
                                         body // Email message body
                                      );

                message.CC.Add("vu.hth@hcmlawfirm.vn");
                message.CC.Add("nghia.nguyen@vietstargroup.vn");
                message.IsBodyHtml = true;
                message.Body = body;
                message.Attachments.Add
                    (new Attachment(urlDowLoad,
                    DateTime.Now.ToString("yyyy.MM.dd")+ "StatusAllCase.xlsx")); 
                client.Send(message);
                Console.WriteLine("Mail dailly report.");
                Console.Read();

            }
        }
    }
}
