

using MailKit.Net.Smtp;
using MimeKit;


namespace sendEmail
{
    public class Sendmail
    {

        private readonly string MAIL_HOST = "mail9054.maychuemail.com";
        private readonly int MAIL_POST = 465;
        private readonly string USERNAME = "noreply@vietstargroup.vn";
        private readonly string PASSWORD = "AERu5e256T";
        private readonly string MAIL_REC = "admin@smartbank.com.vn";
        private readonly string LOCAL_FILE = "C:\\local\\";
        private readonly string LOCAL_SENDFILE = "C:\\local\\FileSended\\";
        public Sendmail()
        {
        }
        public async Task send()
        {
            await sendemailreport();

        }
        public async Task sendemailreport()
        {
            var nameFileRun = "payment_" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx";
            var pathFileRun = Path.Combine(LOCAL_FILE, nameFileRun);
            var pathFileMove = Path.Combine(LOCAL_SENDFILE, nameFileRun);
            if (File.Exists(pathFileMove))
            {
                return;
            }
            if (!File.Exists(pathFileRun))
            {
                return;
            }
            var monthtext = DateTime.Now.ToString("yyyy.mm.dd");
            var subjectmail = "[" + monthtext + "]" + "báo cáo payment hàng ngày";
            var message = new MimeMessage();
            var titleMail = "File PAYMENT ngày " + DateTime.Now.ToString("dd.MM.yyyy");
            message.From.Add(new MailboxAddress(titleMail, USERNAME));
            var recipientAddress = MAIL_REC;
            message.To.Add(new MailboxAddress("", recipientAddress));
            message.Subject = subjectmail;
            var multipart = new Multipart("mixed");
            multipart.Add(new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = "Dear Admin  <br><br>" +
                " Dữ liệu payment hàng ngày của  ( đối tác) gửi qua <br><br>" +
                " Dữ liệu được tính đến thời điểm gửi mail. " +
                "<br><br> Thanks, Admin"
            });
            var excelFilename = pathFileRun;
            var streams = new List<Stream>();

            var stream = File.OpenRead(excelFilename);
            var attachment = new MimePart("application",
           "vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                Content = new MimeContent(stream, ContentEncoding.Default),
                ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                ContentTransferEncoding = ContentEncoding.Binary,
                FileName = Path.GetFileName(excelFilename)
            };

            streams.Add(stream);
            multipart.Add(attachment);
            message.Body = multipart;
            using (var client = new SmtpClient())
            {
                client.Connect(MAIL_HOST, MAIL_POST, true);
                client.Authenticate(USERNAME, PASSWORD);
                client.Send(message);
                client.Disconnect(true);
                Console.WriteLine("mail dailly report.");
            }

            foreach (var stream1 in streams)
                stream1.Dispose();
            File.Move(pathFileRun, pathFileMove);
            Console.WriteLine("Have move file.");
            Console.Read();
        }
    }
}
