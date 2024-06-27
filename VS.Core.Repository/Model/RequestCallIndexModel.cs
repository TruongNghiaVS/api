using VS.Core.dataEntry.User;

namespace VS.Core.Repository.Model
{
    public class RequestCallIndexModel : CampagnProfile
    {

        public string NoAgreement { get; set; }
        public string MobilePhone { get; set; }

        public int TotalRecord { get; set; }
        public string? Id { get; set; }
        public RequestCallIndexModel()
        {
            SkipData = false;
        }

    }
}
