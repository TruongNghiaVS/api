﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VS.core.API.Error.Model;
using VS.core.API.model;
using VS.core.Request;
using VS.core.Utilities;
using VS.Core.Business.Interface;
using VS.Core.dataEntry.User;


namespace vsrolAPI2022.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class CampagnProfileController : BaseController
    {

        private readonly ICampagnBussiness _campagnBusiness;

        public CampagnProfileController(
            ICampagnBussiness campagnBusiness,
            IUserBusiness userBusiness) : base(userBusiness)
        {
            _campagnBusiness = campagnBusiness;
        }

        [AllowAnonymous]
        [HttpPost("~/api/campagnProfile/getById")]
        public async Task<IResult> GetById(InputIdRequest inputRequest)
        {
            if (string.IsNullOrEmpty(inputRequest.Id) ||
                string.IsNullOrEmpty(inputRequest.Id))
            {
                return Results.BadRequest(_message.CommonError_ErrorRequestInput);
            }
            var result = await _campagnBusiness.GetProfile(inputRequest.Id);
            return Results.Ok(result);

        }


        [Authorize]
        [HttpPost("~/api/campagnProfile/getAll")]
        public async Task<IResult> getAll(CampagnProfileSearchInput request)
        {

            var user = GetCurrentUser();
            int? VendorId = null;
            if (user.RoleId == "4")
            {
                VendorId = int.Parse(user.Id);
            }
            var dpdMax = -1;
            var dpdMin = -1;
            if (request.Dpd == 0)
            {
                dpdMax = 30;
                dpdMin = 0;
            }

            if (request.Dpd == 1)
            {
                dpdMax = 60;
                dpdMin = 31;
            }

            if (request.Dpd == 2)
            {
                dpdMax = 90;
                dpdMin = 61;
            }

            if (request.Dpd == 3)
            {
                dpdMax = 180;
                dpdMin = 91;
            }

            if (request.Dpd == 4)
            {
                dpdMax = 360;
                dpdMin = 181;
            }


            if (request.Dpd == 5)
            {
                dpdMax = 361;
                dpdMin = 1000;
            }
            if (request.Dpd == 6)
            {
                dpdMax = 10000;
                dpdMin = 1001;
            }
            var searchRequest = new GetAllProfileByCampang()
            {
                Token = request.Token,
                Status = request.Status,
                Page = request.Page,
                Limit = request.Limit,
                To = request.To,
                DpdMax = dpdMax,
                DpdMin = dpdMin,
                Id = request.Id,
                From = request.From,
                UserId = user.Id,
                VendorId = VendorId,
                PhoneSerach = request.PhoneSerach,
                LineCode = request.LineCode,
                NoAgreement = request.NoAgree,
                TypegetData = request.TypegetData
            };
            if (searchRequest.To.HasValue)
            {
                searchRequest.To = searchRequest.To.ToEndDateTime();
            }
            var resultSearch = await _campagnBusiness.GetALlProfileByCampaign(searchRequest);
            return Results.Ok(resultSearch);
        }

        [Authorize]
        [HttpPost("~/api/campagnProfile/add")]
        public async Task<IResult> Add(CampagnProfileAdd employeeAdd)
        {
            var user = GetCurrentUser();
            if (string.IsNullOrEmpty(employeeAdd.NoAgreement))
            {
                return Results.BadRequest("Không có thông tin mã hợp đồng");
            }

            var account = new Profile()
            {


            };
            var result = await _campagnBusiness.AddProfile(account);
            return Results.Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("~/api/campagnProfile/update")]
        public async Task<IResult> Update(CampagnProfileUpdate request)
        {
            //var user = GetCurrentUser();
            if (string.IsNullOrEmpty(request.Id))
            {
                return Results.BadRequest("Không có thông tin ID");
            }
            var accoutUpdate = await _campagnBusiness.GetProfile(request.Id);
            if (accoutUpdate == null)
            {
                return Results.BadRequest("Không có thông tin profile tương ứng");
            }
            accoutUpdate.CustomerName = request.CustomerName;
            accoutUpdate.NoAgreement = request.NoAgreement;
            accoutUpdate.NationalId = request.NationalId;
            accoutUpdate.MobilePhone = request.MobilePhone;
            accoutUpdate.Phone1 = request.Phone1;
            accoutUpdate.HouseNumber = request.HouseNumber;
            accoutUpdate.OfficeNumber = request.OfficeNumber;
            accoutUpdate.OtherPhone = request.OtherPhone;
            accoutUpdate.Email = request.Email;
            accoutUpdate.DayOfBirth = request.DayOfBirth;
            accoutUpdate.Road = request.Road;
            accoutUpdate.SuburbanDir = request.SuburbanDir;
            accoutUpdate.Provice = request.Provice;

            accoutUpdate.Road1 = request.Road1;
            accoutUpdate.SuburbanDir1 = request.SuburbanDir1;

            accoutUpdate.Provice1 = request.Provice1;
            accoutUpdate.Road2 = request.Road2;

            accoutUpdate.SuburbanDir2 = request.SuburbanDir2;
            accoutUpdate.Provice2 = request.Provice2;

            accoutUpdate.StatusPayMent = request.StatusPayMent;
            accoutUpdate.DPD = request.DPD;


            accoutUpdate.RegisterDay = request.RegisterDay;
            accoutUpdate.DebitOriginal = request.DebitOriginal;
            accoutUpdate.AmountLoan = request.AmountLoan;
            accoutUpdate.EMI = request.EMI;

            accoutUpdate.TotalFines = request.TotalFines;
            accoutUpdate.TotalMoneyPaid = request.TotalMoneyPaid;
            accoutUpdate.TotalFines = request.TotalFines;
            accoutUpdate.TotalMoneyPaid = request.TotalMoneyPaid;
            accoutUpdate.Tenure = request.Tenure;
            accoutUpdate.NoTenure = request.NoTenure;
            accoutUpdate.TotalPaid = request.TotalPaid;

            accoutUpdate.LastPaid = request.LastPaid;
            accoutUpdate.LastPadDay = request.LastPadDay;
            accoutUpdate.NameProduct = request.NameProduct;
            accoutUpdate.CodeProduct = request.CodeProduct;
            accoutUpdate.PriceProduct = request.PriceProduct;
            accoutUpdate.NoteFirstTime = request.NoteFirstTime;
            accoutUpdate.NoteRel = request.NoteRel;
            accoutUpdate.SkipContent = request.SkipContent;
            var result = await _campagnBusiness.UpdateProfile(accoutUpdate);
            return Results.Ok(result);
        }



        [HttpPost("~/api/campagnProfile/updateskip")]
        public async Task<IResult> updateskip(CampagnProfileUpdate request)
        {
            var user = GetCurrentUser();
            if (string.IsNullOrEmpty(request.Id))
            {
                return Results.BadRequest("Không có thông tin ID");
            }
            var accoutUpdate = await _campagnBusiness.GetProfile(request.Id);
            if (accoutUpdate == null)
            {
                return Results.BadRequest("Không có thông tin profile tương ứng");
            }
            accoutUpdate.SkipContent = request.SkipContent;
            accoutUpdate.UpdatedBy = user.Id;
            var result = await _campagnBusiness.UpdateProfile(accoutUpdate);
            return Results.Ok(result);
        }


        //[Authorize]
        //[HttpPost("~/employee/delete")]

        [AllowAnonymous]
        [HttpPost("~/api/campagnProfile/delete")]
        public async Task<IResult> Delete(DeleteModelRequest request)
        {

            if (string.IsNullOrEmpty(request.Id))
            {
                return Results.BadRequest(_message.CommonError_ErrorRequestInput);
            }
            var accoutDelete = await _campagnBusiness.GetByIdAsync(request.Id);
            if (accoutDelete == null)
            {
                return Results.Ok(new ErrorReponse()
                {
                    StatusCode = StatusCodes.Status200OK,
                    Success = false,
                    Message = _message.CommonError_NotFound

                });
            }
            var result = _campagnBusiness.Delete(accoutDelete);
            return Results.Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("~/api/campagnProfile/exportData")]
        public async Task<IResult> ExportData(CampagnSearchInput request)
        {
            var searchRequest = new CampagnRequest()
            {
                UserId = "1",
                Token = request.Token,
                Status = request.Status,
                Page = request.Page,
                Limit = 1000,
                To = request.To,
                From = request.From
            };
            var resultSearch = await _campagnBusiness.GetDataForExport(searchRequest);
            return Results.Ok(resultSearch);
        }


        [AllowAnonymous]
        [HttpPost("~/api/campagnProfile/getInfo")]
        public async Task<IResult> getInfo(InputIdRequest inputRequest)
        {
            if (string.IsNullOrEmpty(inputRequest.Id) ||
                string.IsNullOrEmpty(inputRequest.Id))
            {
                return Results.BadRequest(_message.CommonError_ErrorRequestInput);
            }
            var result = await _campagnBusiness.GetIno(inputRequest.Id);
            return Results.Ok(result);
        }

        [HttpPost("~/api/campagnProfile/Asignee")]
        public async Task<IResult> AsigneeProfile(AssignedRequest inputRequest)
        {
            if (string.IsNullOrEmpty(inputRequest.Id) ||
                string.IsNullOrEmpty(inputRequest.Id))
            {
                return Results.BadRequest(_message.CommonError_ErrorRequestInput);
            }

            if (string.IsNullOrEmpty(inputRequest.AssignedId) ||
                string.IsNullOrEmpty(inputRequest.AssignedId))
            {
                return Results.BadRequest(_message.CommonError_ErrorRequestInput);
            }
            var result = await _campagnBusiness.AssignedTask(inputRequest.Id, inputRequest.AssignedId);
            return Results.Ok(result);
        }


        [HttpPost("~/api/campagnProfile/handleCase")]
        public async Task<IResult> HandleCase(CampaignProfile_caseRequest inputRequest)
        {
            var result = await _campagnBusiness.HandleCase(inputRequest);
            return Results.Ok(result);
        }
    }
}