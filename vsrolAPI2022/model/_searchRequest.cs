namespace VS.core.API.model
{
    public partial class EmployeeSearchInput : BaseInputSearch
    {
        public int? VendorId { get; set; }
    }

    public partial class MasterDataSearchInput : BaseInputSearch
    {
        public string GroupStatus { get; set; }

    }

    public partial class MasterDataNewSearchInput : BaseInputSearch
    {
        public int Type { get; set; }

    }

    public partial class GroupReasonSearchInput : BaseInputSearch
    {


    }

    public partial class GroupReasonSearchInput : BaseInputSearch
    {


    }


    public partial class CampagnSearchInput : BaseInputSearch
    {
        public string? CampaignId { get; set; }

    }

    public partial class LineSearchInput : BaseInputSearch
    {


    }


    public partial class CampagnProfileSearchInput : BaseInputSearch
    {
        public string? Id { get; set; }

        public int? Status { get; set; }

        public int? Dpd { get; set; }

        public string? NoAgree { get; set; }

        public string? LineCode { get; set; }

        public string? PhoneSerach { get; set; }
        public string? TypegetData { get; set; }
        public CampagnProfileSearchInput()
        {
            Status = 0;
        }

    }

    public partial class ImpactHistorySearchInput : BaseInputSearch
    {
        public string? ProfileId { get; set; }

    }









}
