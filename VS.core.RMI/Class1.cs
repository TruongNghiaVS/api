namespace VS.core.RMI
{
    public class Connfig
    {
        public string Host { get; set; }

        public string Port { get; set; }

        public string User { get; set; }

        public string Pass { get; set; }

        public Connfig()
        {
            Port = "5038";
            Pass = "";
            User = "admin";
            Host = "192.168.1.151";


        }




    }
}