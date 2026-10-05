using Engineering.Application.Services.CabinTypes.Models.GetsCabinType;
using Engineering.Application.Services.CabinTypes.Models.GetsCabinTypeExcelEnum;
using OfficeOpenXml.Style;
using ExcelStyles = Engineering.Application.Extensions.Excels.Helpers.ExcelStyles;

namespace Engineering.Application.Extensions.Excels.Exporters;
public class CabinTypeExcels
{
    public static byte[] CabinTypeToExcel(
        ICollection<GetsCabinTypeResponseModel> result,
        List<CabinTypeExcelEnum>? excelFilters)
    {
        using var workbook = new ExcelPackage();
        var worksheet = workbook.Workbook.Worksheets.Add("نوع اتاق");
        var currentRow = 1;
        worksheet.View.RightToLeft = true;

        if (excelFilters != null && excelFilters.Count > 0)
        {
            var columns = new Dictionary<CabinTypeExcelEnum, int>();
            for (int counter = 0; counter < excelFilters.Count; counter++)
            {
                worksheet.Cells[1, counter + 1].Value = excelFilters[counter].GetEnumDescription();
                columns[excelFilters[counter]] = counter + 1;

                ExcelStyles.SetHeaderStyle(
                    worksheet.Cells[1, 1, 1, counter + 1],
                    ExcelBorderStyle.Medium);
            }

            foreach (var item in result)
            {
                currentRow++;
                if (columns.ContainsKey(CabinTypeExcelEnum.Row))
                    worksheet.Cells[currentRow, columns[CabinTypeExcelEnum.Row]].Value = currentRow - 1;
                if (columns.ContainsKey(CabinTypeExcelEnum.CabinTypeName))
                    worksheet.Cells[currentRow, columns[CabinTypeExcelEnum.CabinTypeName]].Value = item.CabinTypeName;
                if (columns.ContainsKey(CabinTypeExcelEnum.CabinTypeCode))
                    worksheet.Cells[currentRow, columns[CabinTypeExcelEnum.CabinTypeCode]].Value = item.CabinTypeCode;
                if (columns.ContainsKey(CabinTypeExcelEnum.IsActive))
                    worksheet.Cells[currentRow, columns[CabinTypeExcelEnum.IsActive]].Value = item.IsActive == true ? "فعال" : "غیرفعال";
                if (columns.ContainsKey(CabinTypeExcelEnum.CompanyNameFa))
                    worksheet.Cells[currentRow, columns[CabinTypeExcelEnum.CompanyNameFa]].Value = item.CompanyNameFa;

                ExcelStyles.SetCellStyle(
                    worksheet,
                    currentRow - 1,
                    currentRow,
                    worksheet.Dimension.Start.Column,
                    worksheet.Dimension.End.Column);
            }
        }
        else
        {
            ExcelStyles.SetHeaderStyle(
                worksheet.Cells[1, 1, 1, 5],
                ExcelBorderStyle.Medium);

            worksheet.Cells[1, 1].Value = CabinTypeExcelEnum.Row.GetEnumDescription();
            worksheet.Cells[1, 2].Value = CabinTypeExcelEnum.CabinTypeName.GetEnumDescription();
            worksheet.Cells[1, 3].Value = CabinTypeExcelEnum.CabinTypeCode.GetEnumDescription();
            worksheet.Cells[1, 4].Value = CabinTypeExcelEnum.IsActive.GetEnumDescription();
            worksheet.Cells[1, 5].Value = CabinTypeExcelEnum.CompanyNameFa.GetEnumDescription();

            foreach (var item in result)
            {
                currentRow++;
                worksheet.Cells[currentRow, 1].Value = currentRow - 1;
                worksheet.Cells[currentRow, 2].Value = item.CabinTypeName;
                worksheet.Cells[currentRow, 3].Value = item.CabinTypeCode;
                worksheet.Cells[currentRow, 4].Value = item.IsActive == true ? "فعال" : "غیرفعال";
                worksheet.Cells[currentRow, 5].Value = item.CompanyNameFa;

                ExcelStyles.SetCellStyle(
                    worksheet,
                    currentRow - 1,
                    currentRow,
                    worksheet.Dimension.Start.Column,
                    worksheet.Dimension.End.Column);
            }
        }

        ExcelStyles.SetSummaryCellStyle(
            worksheet.Cells[2, 7, 4, 8],
            worksheet,
            result.Count(c => c.IsActive),
            result.Count, 6);

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }
}
