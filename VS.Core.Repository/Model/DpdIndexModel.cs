namespace VS.Core.Repository.Model
{
    public class DpdIndexModel : BaseIndexModel
    {
        public string? Name { get; set; }

        public int? Status { get; set; }

        public int? VendorId { get; set; }
        public int? Min { get; set; }
        public int? Max { get; set; }


    }
}
