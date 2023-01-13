namespace VS.Core.Repository.Model
{
    public class MasterDataNewIndexModel : BaseIndexModel
    {

        public string Code { get; set; }

        public string Name { get; set; }

        public int Status { get; set; }

        public int Type { get; set; }

        public DateTime? CreateAt { get; set; }

        public DateTime? UpdateAt { get; set; }


    }
}
