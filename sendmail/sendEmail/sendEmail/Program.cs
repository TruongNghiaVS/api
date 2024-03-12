namespace sendEmail
{
    internal class Program
    {


        static void Main(string[] args)
        {
            var mail = new Sendmail();
            mail.Send();
        }


    }
}
