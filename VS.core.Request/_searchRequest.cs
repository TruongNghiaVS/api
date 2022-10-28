﻿namespace VS.core.Request
{
    public class EmployeeSearchRequest : BaseSearchRequest
    {




        public EmployeeSearchRequest()
        {
            this.Page = 1;
            this.Limit = 10;
            this.From = new DateTime(2022, 09, 20);
            this.To = new DateTime(2022, 10, 01);
        }
    }

    public class EmployeeSearchReponse : BaseSearchRepons
    {

        public EmployeeSearchReponse()
        {
            Total = 0;
        }


    }


    public class MaterDataRequest : BaseSearchRequest
    {
        public string Msg { get; set; }
        public string GroupStatus { get; set; }
        public MaterDataRequest()
        {
            this.Page = 1;
            this.Limit = 10;
            this.From = new DateTime(2022, 09, 20);
            this.To = new DateTime(2022, 10, 01);
        }
    }

    public class MasterDataReponse : BaseSearchRepons

    {
        public MasterDataReponse()
        {
            Total = 0;
        }
    }



    public class GroupReasonRequest : BaseSearchRequest
    {
        public string Msg { get; set; }

        public GroupReasonRequest()
        {
            this.Page = 1;
            this.Limit = 10;
            this.From = new DateTime(2022, 09, 20);
            this.To = new DateTime(2022, 10, 01);
        }
    }

    public class GroupReasonReponse : BaseSearchRepons

    {
        public GroupReasonReponse()
        {
            Total = 0;
        }
    }


    public class CampagnRequest : BaseSearchRequest
    {
        public string? Msg { get; set; }

        public string? CampaignId { get; set; }
        public CampagnRequest()
        {
            this.Page = 1;
            this.Limit = 10;
            this.From = new DateTime(2022, 09, 20);
            this.To = new DateTime(2022, 10, 01);
        }
    }

    public class CampagnRequestReponse : BaseSearchRepons

    {
        public CampagnRequestReponse()
        {
            Total = 0;
        }
    }




    public class GetAllProfileByCampang : BaseSearchRequest
    {
        public string? Id { get; set; }

        public int? Status { get; set; }


    }


    public class GetAllProfileByCampangReponse : BaseSearchRepons

    {
        public GetAllProfileByCampangReponse()
        {
            Total = 0;
        }
    }

    public partial class CampagnAsiggeeByCampagnIdReponse : BaseSearchRepons
    {

        public CampagnAsiggeeByCampagnIdReponse()
        {

        }

    }




}
