using VS.Core.dataEntry.User;

namespace VS.Core.Repository.Model
{
    public class ProileIndexModel : Profile
    {

        public int TotalRecord { get; set; }


        public string StatusText
        {

            get
            {
                if (Status == 0)
                {
                    return "Chưa phân";
                }

                return "Chưa phân";

            }
        }

        public string AssigneeName { get; set; }

        public string ReasonstatusText
        {

            get
            {
                if (Reasonstatus == 0)
                {
                    return "NKP - Has signal but...";
                }

                return "Chưa rõ tình trạng";

            }
        }





    }
}
