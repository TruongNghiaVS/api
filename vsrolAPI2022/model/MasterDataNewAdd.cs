namespace VS.core.API.model
{
    public class MasterDataNewAdd
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Status { get; set; }

        public int Type { get; set; }



    }
    public class MasterDataNewUpdate
    {

        public string Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Status { get; set; }

        public int? Type { get; set; }



    }




}
