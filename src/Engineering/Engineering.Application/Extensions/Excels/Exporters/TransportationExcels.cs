using Engineering.Application.Services.Transportations.Models.GetsTransportationExcelEnum;
using Engineering.Application.Services.Transportations.Models.GetsTransportationExcelExporter;
using OfficeOpenXml.Style;
using ExcelStyles = Engineering.Application.Extensions.Excels.Helpers.ExcelStyles;

namespace Engineering.Application.Extensions.Excels.Exporters;
public class TransportationExcels
{
    public static byte[] TransportationToExcel(
        ICollection<GetsTransportationExcelExporterModel> result,
        List<TransportationExcelEnum>? excelFilters)
    {
        using var workbook = new ExcelPackage();
        var worksheet = workbook.Workbook.Worksheets.Add("ترابری");
        var currentRow = 1;
        worksheet.View.RightToLeft = true;

        if (excelFilters != null && excelFilters.Count > 0)
        {
            var columns = new Dictionary<TransportationExcelEnum, int>();
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
                if (columns.ContainsKey(TransportationExcelEnum.Row))
                    worksheet.Cells[currentRow, columns[TransportationExcelEnum.Row]].Value = currentRow - 1;
                if (columns.ContainsKey(TransportationExcelEnum.TransportationName))
                    worksheet.Cells[currentRow, columns[TransportationExcelEnum.TransportationName]].Value = item.TransportationName;
                if (columns.ContainsKey(TransportationExcelEnum.TransportationCode))
                    worksheet.Cells[currentRow, columns[TransportationExcelEnum.TransportationCode]].Value = item.TransportationCode;
                if (columns.ContainsKey(TransportationExcelEnum.IsPassenger))
                    worksheet.Cells[currentRow, columns[TransportationExcelEnum.IsPassenger]].Value = item.IsPassenger;
                if (columns.ContainsKey(TransportationExcelEnum.IsActive))
                    worksheet.Cells[currentRow, columns[TransportationExcelEnum.IsActive]].Value = item.IsActive == true ? "فعال" : "غیرفعال";
                if (columns.ContainsKey(TransportationExcelEnum.CompanyNameFa))
                    worksheet.Cells[currentRow, columns[TransportationExcelEnum.CompanyNameFa]].Value = item.CompanyNameFa;

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
               worksheet.Cells[1, 1, 1, 6],

               ExcelBorderStyle.Medium);

            worksheet.Cells[1, 1].Value = TransportationExcelEnum.Row.GetEnumDescription();
            worksheet.Cells[1, 2].Value = TransportationExcelEnum.TransportationName.GetEnumDescription();
            worksheet.Cells[1, 3].Value = TransportationExcelEnum.TransportationCode.GetEnumDescription();
            worksheet.Cells[1, 4].Value = TransportationExcelEnum.IsPassenger.GetEnumDescription();
            worksheet.Cells[1, 5].Value = TransportationExcelEnum.IsActive.GetEnumDescription();
            worksheet.Cells[1, 6].Value = TransportationExcelEnum.CompanyNameFa.GetEnumDescription();

            foreach (var item in result)
            {
                currentRow++;
                worksheet.Cells[currentRow, 1].Value = currentRow - 1;
                worksheet.Cells[currentRow, 2].Value = item.TransportationName;
                worksheet.Cells[currentRow, 3].Value = item.TransportationCode;
                worksheet.Cells[currentRow, 4].Value = item.IsPassenger;
                worksheet.Cells[currentRow, 5].Value = item.IsActive == true ? "فعال" : "غیرفعال";
                worksheet.Cells[currentRow, 6].Value = item.CompanyNameFa;

                ExcelStyles.SetCellStyle(
                    worksheet,
                    currentRow - 1,
                    currentRow,
                    worksheet.Dimension.Start.Column,
                    worksheet.Dimension.End.Column);
            }
        }

        ExcelStyles.SetSummaryCellStyle(
            worksheet.Cells[2, 8, 4, 10],
            worksheet,
            result.Count(c => c.IsActive),
            result.Count,
            7);

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }
}
