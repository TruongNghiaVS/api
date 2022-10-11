namespace VS.core.API.model
{
    public class MasterDataAdd
    {
        public string? Code { get; set; }
        public string? DisplayName { get; set; }
        public string? FullName { get; set; }

        public int? Hour { get; set; }
        public int? Day { get; set; }

        public int? GroupId { get; set; }


    }
    public class MasterDataUpdate
    {

        public string? Id { get; set; }
        //public string? Code { get; set; }
        public string? DisplayName { get; set; }
        public string? FullName { get; set; }


        public int? Hour { get; set; }
        public int? Day { get; set; }



    }




}
