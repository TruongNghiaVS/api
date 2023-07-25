using System.Collections;
using VS.Core.dataEntry.User;
using VS.Core.Repository.Model;

namespace VS.Core.Business.Model
{
    public class CampangeProfileInforReponse

    {
        public CampagnProfile Result { get; set; }

        public string StatusProfile { get; set; }

        public IEnumerable? ListUser { get; set; }
        public Campagn campagn { get; set; }

        public IEnumerable? ListReason { get; set; }

        public IEnumerable? ListHistory { get; set; }

        public List<SkipInfo> ListSkipNew { get; set; }
        public CampangeProfileInforReponse()
        {
            ListHistory = new List<ImpactHistoryIndexModel>();
            Result = new CampagnProfile();
        }

    }
}
