using Engineering.Application.Services.CostCenters.Models.GetsCostCenterExcelEnum;
using Engineering.Application.Services.CostCenters.Models.GetsCostCenterExcelExporter;
using OfficeOpenXml.Style;
using ExcelStyles = Engineering.Application.Extensions.Excels.Helpers.ExcelStyles;

namespace Engineering.Application.Extensions.Excels.Exporters;

public class CostCenterExcels
{
    public static byte[] CostCenterToExcel(
        ICollection<GetsCostCenterExcelExporterModel> result,
        List<CostCenterExcelEnum>? excelFilters)
    {
        using var workbook = new ExcelPackage();
        var worksheet = workbook.Workbook.Worksheets.Add("مرکز هزینه");
        var currentRow = 1;
        worksheet.View.RightToLeft = true;

        if (excelFilters != null && excelFilters.Count > 0)
        {
            var columns = new Dictionary<CostCenterExcelEnum, int>();
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
                if (columns.ContainsKey(CostCenterExcelEnum.Row))
                    worksheet.Cells[currentRow, columns[CostCenterExcelEnum.Row]].Value = currentRow - 1;
                if (columns.ContainsKey(CostCenterExcelEnum.CostCenterTypeTitle))
                    worksheet.Cells[currentRow, columns[CostCenterExcelEnum.CostCenterTypeTitle]].Value = item.CostCenterTypeTitle;
                if (columns.ContainsKey(CostCenterExcelEnum.CostCenterName))
                    worksheet.Cells[currentRow, columns[CostCenterExcelEnum.CostCenterName]].Value = item.CostCenterName;
                if (columns.ContainsKey(CostCenterExcelEnum.CostCenterCode))
                    worksheet.Cells[currentRow, columns[CostCenterExcelEnum.CostCenterCode]].Value = item.CostCenterCode;
                if (columns.ContainsKey(CostCenterExcelEnum.NoOperationDays))
                    worksheet.Cells[currentRow, columns[CostCenterExcelEnum.NoOperationDays]].Value = item.NoOperationDays;
                if (columns.ContainsKey(CostCenterExcelEnum.WarehouseName))
                    worksheet.Cells[currentRow, columns[CostCenterExcelEnum.WarehouseName]].Value = item.WarehouseName;
                if (columns.ContainsKey(CostCenterExcelEnum.WarehouseManagement))
                    worksheet.Cells[currentRow, columns[CostCenterExcelEnum.WarehouseManagement]].Value = item.WarehouseManagement;
                if (columns.ContainsKey(CostCenterExcelEnum.CityName))
                    worksheet.Cells[currentRow, columns[CostCenterExcelEnum.CityName]].Value = item.CityName;
                if (columns.ContainsKey(CostCenterExcelEnum.Address))
                    worksheet.Cells[currentRow, columns[CostCenterExcelEnum.Address]].Value = item.Address;
                if (columns.ContainsKey(CostCenterExcelEnum.PostalCode))
                    worksheet.Cells[currentRow, columns[CostCenterExcelEnum.PostalCode]].Value = item.PostalCode;
                if (columns.ContainsKey(CostCenterExcelEnum.Description))
                    worksheet.Cells[currentRow, columns[CostCenterExcelEnum.Description]].Value = item.Description;
                if (columns.ContainsKey(CostCenterExcelEnum.WeatherState))
                    worksheet.Cells[currentRow, columns[CostCenterExcelEnum.WeatherState]].Value = item.WeatherState;
                if (columns.ContainsKey(CostCenterExcelEnum.IsActive))
                    worksheet.Cells[currentRow, columns[CostCenterExcelEnum.IsActive]].Value = item.IsActive == true ? "فعال" : "غیرفعال";
                if (columns.ContainsKey(CostCenterExcelEnum.CompanyNameFa))
                    worksheet.Cells[currentRow, columns[CostCenterExcelEnum.CompanyNameFa]].Value = item.CompanyNameFa;

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
                worksheet.Cells[1, 1, 1, 14],
                ExcelBorderStyle.Medium);

            worksheet.Cells[1, 1].Value = CostCenterExcelEnum.Row.GetEnumDescription();
            worksheet.Cells[1, 2].Value = CostCenterExcelEnum.CostCenterTypeTitle.GetEnumDescription();
            worksheet.Cells[1, 3].Value = CostCenterExcelEnum.CostCenterName.GetEnumDescription();
            worksheet.Cells[1, 4].Value = CostCenterExcelEnum.CostCenterCode.GetEnumDescription();
            worksheet.Cells[1, 5].Value = CostCenterExcelEnum.NoOperationDays.GetEnumDescription();
            worksheet.Cells[1, 6].Value = CostCenterExcelEnum.WarehouseName.GetEnumDescription();
            worksheet.Cells[1, 7].Value = CostCenterExcelEnum.WarehouseManagement.GetEnumDescription();
            worksheet.Cells[1, 8].Value = CostCenterExcelEnum.CityName.GetEnumDescription();
            worksheet.Cells[1, 9].Value = CostCenterExcelEnum.Address.GetEnumDescription();
            worksheet.Cells[1, 10].Value = CostCenterExcelEnum.PostalCode.GetEnumDescription();
            worksheet.Cells[1, 11].Value = CostCenterExcelEnum.Description.GetEnumDescription();
            worksheet.Cells[1, 12].Value = CostCenterExcelEnum.WeatherState.GetEnumDescription();
            worksheet.Cells[1, 13].Value = CostCenterExcelEnum.IsActive.GetEnumDescription();
            worksheet.Cells[1, 14].Value = CostCenterExcelEnum.CompanyNameFa.GetEnumDescription();

            foreach (var item in result)
            {
                currentRow++;
                worksheet.Cells[currentRow, 1].Value = currentRow - 1;
                worksheet.Cells[currentRow, 2].Value = item.CostCenterTypeTitle;
                worksheet.Cells[currentRow, 3].Value = item.CostCenterName;
                worksheet.Cells[currentRow, 4].Value = item.CostCenterCode;
                worksheet.Cells[currentRow, 5].Value = item.NoOperationDays;
                worksheet.Cells[currentRow, 6].Value = item.WarehouseName;
                worksheet.Cells[currentRow, 7].Value = item.WarehouseManagement;
                worksheet.Cells[currentRow, 8].Value = item.CityName;
                worksheet.Cells[currentRow, 9].Value = item.Address;
                worksheet.Cells[currentRow, 10].Value = item.PostalCode;
                worksheet.Cells[currentRow, 11].Value = item.Description;
                worksheet.Cells[currentRow, 12].Value = item.WeatherState;
                worksheet.Cells[currentRow, 13].Value = item.IsActive == true ? "فعال" : "غیرفعال";
                worksheet.Cells[currentRow, 14].Value = item.CompanyNameFa;

                ExcelStyles.SetCellStyle(
                    worksheet,
                    currentRow - 1,
                    currentRow,
                    worksheet.Dimension.Start.Column,
                    worksheet.Dimension.End.Column);
            }
        }

        ExcelStyles.SetSummaryCellStyle(
            worksheet.Cells[2, 16, 4, 17],
            worksheet,
            result.Count(c => c.IsActive),
            result.Count,
            15);

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }
}