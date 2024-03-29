using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VS.Core.dataEntry.User;

namespace VS.Core.Business.GlobalClass
{
    public class ConatinnerCall
    {
       
        private static ConatinnerCall instance = null;
        public  List<CampagnProfile> DataCall { get; set; }
        private ConatinnerCall() {
            DataCall = new List<CampagnProfile>();
        }

        public static ConatinnerCall Contanner
        {
            get
            {
                if (instance == null)
                {
                    instance = new ConatinnerCall();
                }
                return instance;
            }
        }
    }
}
