using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using VS.Core.Business.Interface;

namespace vsrolAPI2022.Controllers
{
    [ApiController]

    [Route("[controller]")]
    public class BaseImportController : BaseController
    {

        protected readonly IUserBusiness _userBusiness;

        public BaseImportController(IUserBusiness userBusiness) : base(userBusiness)
        {
            _userBusiness = userBusiness;

        }

        protected string ReadvalueStringExcel(ExcelWorksheet excelworksheet, int row, int col)
        {
            var cellRange = excelworksheet.Cells[row, col];
            if (cellRange != null)
            {

                if (cellRange.Value != null)
                {
                    return cellRange.Value.ToString();
                }

            }
            return "";

        }


        protected DateTime? ReadvalueDateExcel(ExcelWorksheet excelworksheet, int row, int col)
        {
            try
            {
                var cellRange = excelworksheet.Cells[row, col];
                if (cellRange != null)
                {

                    if (cellRange.Text != null)
                    {
                        try
                        {

                            return DateTime.ParseExact(cellRange.Text.Trim(), "dd/MM/yyyy", null);

                        }
                        catch (Exception)
                        {
                            try
                            {
                                return DateTime.Now.AddYears(-int.Parse(cellRange.Text));
                            }
                            catch (Exception)
                            {

                                return null;
                            }

                            return null;
                        }

                    }

                }
                return null;
            }
            catch (Exception)
            {

                return null;
            }



        }

        protected DateTime? ReadvalueDateExcel2(ExcelWorksheet excelworksheet, int row, int col)
        {
            try
            {
                var cellRange = excelworksheet.Cells[row, col];
                if (cellRange != null)
                {

                    if (cellRange.Text != null)
                    {
                        try
                        {

                            return DateTime.ParseExact(cellRange.Text.Trim(), "dd/MM/yyyy", null);

                        }
                        catch (Exception)
                        {
                            try
                            {
                                return DateTime.Now.AddYears(-int.Parse(cellRange.Text));
                            }
                            catch (Exception)
                            {

                                return null;
                            }

                            return null;
                        }

                    }

                }
                return null;
            }
            catch (Exception)
            {

                return null;
            }


        }

        protected float ReadvaluefloatExcel(ExcelWorksheet excelworksheet, int row, int col)
        {
            try
            {
                var cellRange = excelworksheet.Cells[row, col];
                if (cellRange == null)
                {
                    return 0;
                }

                if (cellRange.Value == null)

                {

                    return 0;
                }
                var valueCell = cellRange.Value.ToString();

                float b1 = 0;
                if (!float.TryParse(valueCell, out b1))
                {

                }
                return b1;
            }
            catch (Exception)
            {

                return 0;
            }

        }

        protected int ReadvalueintExcel(ExcelWorksheet excelworksheet, int row, int col)
        {
            try
            {
                var cellRange = excelworksheet.Cells[row, col];
                if (cellRange == null)
                {
                    return 0;
                }

                if (cellRange.Value == null)

                {

                    return 0;
                }
                var valueCell = cellRange.Value.ToString();

                int b1 = 0;
                if (!int.TryParse(valueCell, out b1))
                {

                }
                return b1;
            }
            catch (Exception)
            {

                return 0;
            }

        }

        protected string? ReadvaluestringExcelWidthNull(ExcelWorksheet excelworksheet, int row, int col)
        {
            try
            {
                var cellRange = excelworksheet.Cells[row, col];
                if (cellRange == null)
                {
                    return null;
                }

                if (cellRange.Value == null)

                {

                    return null;
                }
                var valueCell = cellRange.Value.ToString();


                return valueCell;
            }
            catch (Exception)
            {

                return "";
            }

        }




    }
}