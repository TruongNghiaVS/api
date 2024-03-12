using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace sendEmail
{ 
    public class Sendmail
    {
        
        public Sendmail() { }   

        public void Send()
        {
            using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("nghiait06@gmail.com", "shrbrcumqktfxeaa");
                // client.UseDefaultCredentials = true;
                var body = "Link tải report: ";
                MailMessage message = new MailMessage(
                                         "nghiait06@gmail.com", // From field
                                         "nghia.nguyen@vietstargroup.vn", // Recipient field
                                         "mail test", // Subject of the email message
                                         body // Email message body
                                      );

                

                message.IsBodyHtml = true;
                message.Body = body;
                

                client.Send(message);
                
                Console.WriteLine("Email has been sent.");
                Console.Read();

            }
        }


    }
}
