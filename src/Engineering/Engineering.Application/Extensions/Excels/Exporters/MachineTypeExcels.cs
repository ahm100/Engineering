using Engineering.Application.Services.MachineTypes.Models.GetsMachineTypeExcelEnum;
using Engineering.Application.Services.MachineTypes.Models.GetsMachineTypeExcelExporter;
using OfficeOpenXml.Style;
using ExcelStyles = Engineering.Application.Extensions.Excels.Helpers.ExcelStyles;

namespace Engineering.Application.Extensions.Excels.Exporters;

public class MachineTypeExcels
{
    public static byte[] MachineTypeToExcel(
        ICollection<GetsMachineTypeExcelExporterModel> result,
        List<MachineTypeExcelEnum>? excelFilters)
    {
        using var workbook = new ExcelPackage();
        var worksheet = workbook.Workbook.Worksheets.Add("نوع ماشین");
        var currentRow = 1;
        worksheet.View.RightToLeft = true;

        if (excelFilters != null && excelFilters.Count > 0)
        {
            var columns = new Dictionary<MachineTypeExcelEnum, int>();
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
                if (columns.ContainsKey(MachineTypeExcelEnum.Row))
                    worksheet.Cells[currentRow, columns[MachineTypeExcelEnum.Row]].Value = currentRow - 1;
                if (columns.ContainsKey(MachineTypeExcelEnum.MachineTypeName))
                    worksheet.Cells[currentRow, columns[MachineTypeExcelEnum.MachineTypeName]].Value = item.MachineTypeName;
                if (columns.ContainsKey(MachineTypeExcelEnum.MachineTypeCode))
                    worksheet.Cells[currentRow, columns[MachineTypeExcelEnum.MachineTypeCode]].Value = item.MachineTypeCode;
                if (columns.ContainsKey(MachineTypeExcelEnum.FromWeight))
                    worksheet.Cells[currentRow, columns[MachineTypeExcelEnum.FromWeight]].Value = item.FromWeight;
                if (columns.ContainsKey(MachineTypeExcelEnum.UntilWeight))
                    worksheet.Cells[currentRow, columns[MachineTypeExcelEnum.UntilWeight]].Value = item.UntilWeight;
                if (columns.ContainsKey(MachineTypeExcelEnum.CabinTypeName))
                    worksheet.Cells[currentRow, columns[MachineTypeExcelEnum.CabinTypeName]].Value = item.CabinTypeName;
                if (columns.ContainsKey(MachineTypeExcelEnum.CabinTypeCode))
                    worksheet.Cells[currentRow, columns[MachineTypeExcelEnum.CabinTypeCode]].Value = item.CabinTypeCode;
                if (columns.ContainsKey(MachineTypeExcelEnum.IsActive))
                    worksheet.Cells[currentRow, columns[MachineTypeExcelEnum.IsActive]].Value = item.IsActive == true ? "فعال" : "غیرفعال";
                if (columns.ContainsKey(MachineTypeExcelEnum.CompanyNameFa))
                    worksheet.Cells[currentRow, columns[MachineTypeExcelEnum.CompanyNameFa]].Value = item.CompanyNameFa;

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
                worksheet.Cells[1, 1, 1, 9],

                ExcelBorderStyle.Medium);

            worksheet.Cells[1, 1].Value = MachineTypeExcelEnum.Row.GetEnumDescription();
            worksheet.Cells[1, 2].Value = MachineTypeExcelEnum.MachineTypeName.GetEnumDescription();
            worksheet.Cells[1, 3].Value = MachineTypeExcelEnum.MachineTypeCode.GetEnumDescription();
            worksheet.Cells[1, 4].Value = MachineTypeExcelEnum.FromWeight.GetEnumDescription();
            worksheet.Cells[1, 5].Value = MachineTypeExcelEnum.UntilWeight.GetEnumDescription();
            worksheet.Cells[1, 6].Value = MachineTypeExcelEnum.CabinTypeName.GetEnumDescription();
            worksheet.Cells[1, 7].Value = MachineTypeExcelEnum.CabinTypeCode.GetEnumDescription();
            worksheet.Cells[1, 8].Value = MachineTypeExcelEnum.IsActive.GetEnumDescription();
            worksheet.Cells[1, 9].Value = MachineTypeExcelEnum.CompanyNameFa.GetEnumDescription();

            foreach (var item in result)
            {
                currentRow++;
                worksheet.Cells[currentRow, 1].Value = currentRow - 1;
                worksheet.Cells[currentRow, 2].Value = item.MachineTypeName;
                worksheet.Cells[currentRow, 3].Value = item.MachineTypeCode;
                worksheet.Cells[currentRow, 4].Value = item.FromWeight;
                worksheet.Cells[currentRow, 5].Value = item.UntilWeight;
                worksheet.Cells[currentRow, 6].Value = item.CabinTypeName;
                worksheet.Cells[currentRow, 7].Value = item.CabinTypeCode;
                worksheet.Cells[currentRow, 8].Value = item.IsActive == true ? "فعال" : "غیرفعال";
                worksheet.Cells[currentRow, 9].Value = item.CompanyNameFa;

                ExcelStyles.SetCellStyle(
                    worksheet,
                    currentRow - 1,
                    currentRow,
                    worksheet.Dimension.Start.Column,
                    worksheet.Dimension.End.Column);
            }
        }

        ExcelStyles.SetSummaryCellStyle(
            worksheet.Cells[2, 11, 4, 13],
            worksheet,
            result.Count(c => c.IsActive),
            result.Count,
            10);

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }
}
