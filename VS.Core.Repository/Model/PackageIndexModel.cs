namespace VS.Core.Repository.Model
{
    public class PackageIndexModel : BaseIndexModel
    {
        public string? Name { get; set; }

        public int? Status { get; set; }
        public string? Type { get; set; }
        public string? Value { get; set; }

        public string? IdUser { get; set; }


        public int? Priorities { get; set; }
        public int? VendorId { get; set; }
        public int? Min { get; set; }
        public int? Max { get; set; }


    }


    public class GetCountBYMinMaxIndexModel
    {
        public int Total { get; set; }

        public int Remain { get; set; }
    }
}
