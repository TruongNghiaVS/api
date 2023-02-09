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
                if (string.IsNullOrEmpty(Assignee) || Assignee == "-1" || Status < 1)
                {
                    return "Chưa phân";
                }


                return "Đã phân";

            }
        }

        public string AssigneeName { get; set; }

        public string ReasonstatusText
        {

            get; set;
        }

        public bool? Skipp { get; set; }



        public string? Id { get; set; }

        public bool Success { get; set; }
        public DateTime? CreatedTime { get; set; }

        public DateTime? UpdatedTime { get; set; }
        public string AuthorName { get; set; }

        public string UpdateByName
        {
            get; set;
        }

    }
}
