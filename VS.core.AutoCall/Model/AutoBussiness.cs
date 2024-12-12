

namespace VS.core.AutoCall
{
    public class DurationTalkReponse
    {
        public string PhoneNumber { get; set; }
        public string Duration { get; set; }
        public string FullName { get; set; }
        public string RoleName { get; set; }
        public bool HavingCall { get; set; }
        public DurationTalkReponse()
        {
            FullName = "Nguyễn Trường Nghĩa";
            HavingCall = false;
        }
    }
    public class LineCall
    {
        public string Line { get; set; }
        public DateTime? LastUpdate { get; set; }
        public bool Iscall { get; set; }
    }
}
