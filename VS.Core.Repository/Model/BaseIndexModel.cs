namespace VS.Core.Repository.Model
{
    public class BaseIndexModel
    {

        public int TotalRecord { get; set; }
        public string? Id { get; set; }

        public bool Success { get; set; }
        public DateTime? CreatedTime { get; set; }

        public DateTime? UpdatedTime { get; set; }
        public string AuthorName
        {
            get
            {
                return "Nguyễn Trường Nghĩa";

            }
        }

        public string UpdateByName
        {
            get
            {
                return "Nguyễn Trường Nghĩa";

            }
        }
        public BaseIndexModel()
        {
            Success = true;
        }
    }

}
