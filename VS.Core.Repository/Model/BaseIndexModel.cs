namespace VS.Core.Repository.Model
{
    public class BaseIndexModel
    {

        public int TotalRecord { get; set; }
        public string? Id { get; set; }

        public bool Success { get; set; }
        public DateTime? CreatedTime { get; set; }

        public DateTime? UpdatedTime { get; set; }
        public string AuthorName { get; set; }

        public string UpdateByName
        {
            get; set;
        }
        public BaseIndexModel()
        {
            Success = true;
        }
    }

}
