using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VS.Core.Business.GlobalClass
{
    public class CrmReportRequest
    {
        public string UserName { get; set; }
        private DateTime? FromTimeAss { get; set; }

        public DateTime? From
        {

            get
            {
                return FromTimeAss;
            }
            set
            {
                if (value.HasValue)
                {
                    FromTimeAss = new DateTime(value.Value.Year, value.Value.Month, value.Value.Day, 0, 0, 0);

                }
                else
                {
                    FromTimeAss = null;
                }

            }
        }


        private DateTime? ToTimeAss { get; set; }
        public DateTime? To
        {
            get
            {
                return ToTimeAss;
            }
            set
            {
                if (value.HasValue)
                {
                    ToTimeAss = new DateTime(value.Value.Year, value.Value.Month, value.Value.Day, 23, 59, 59);

                }
                else
                {
                    ToTimeAss = null;
                }

            }
        }
    }
}
