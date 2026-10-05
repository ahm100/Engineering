using Engineering.Application.Services.FixAssetMachineries.Models.GetsFixAssetMachineryExcelEnum;
using Engineering.Application.Services.FixAssetMachineries.Models.GetsFixAssetMachineryExcelExporter;
using Engineering.Application.Services.FixAssetMachineries.Models.GetsFixAssetMachineryNotWorkExcelEnum;
using Engineering.Application.Services.FixAssetMachineries.Models.GetsFixAssetMachineryNotWorkExcelExporter;

namespace Engineering.Application.Extensions.Excels.Exporters;

public class FixAssetMachineryExcels
{
    public static byte[] FixAssetMachineryToExcel(ICollection<GetsFixAssetMachineryExcelExporterModel> result,
        ICollection<GetFixAssetMachineriesNotWorkExcelExporterModel>? notWorks,
        List<GetFixAssetMachineriesRateExcelExporterModel>? rates,
        List<FixAssetMachineryExcelEnum>? excelFilters)
    {
        using var workbook = new ExcelPackage();
        var worksheet1 = workbook.Workbook.Worksheets.Add("موجودی ماشین آلات");
        var worksheet2 = workbook.Workbook.Worksheets.Add("عدم فعالیت موجودی ماشین آلات");
        var worksheet3 = workbook.Workbook.Worksheets.Add("قیمت گزاری های موجودی ماشین آلات");
        var currentRow1 = 1;
        var currentRow2 = 1;
        var currentRow3 = 1;
        worksheet1.View.RightToLeft = true;
        worksheet2.View.RightToLeft = true;
        worksheet3.View.RightToLeft = true;

        if (excelFilters != null && excelFilters.Count > 0)
        {
            var columns = new Dictionary<FixAssetMachineryExcelEnum, int>();
            for (int counter = 0; counter < excelFilters.Count; counter++)
            {
                worksheet1.Cells[1, counter + 1].Value = excelFilters[counter].GetEnumDescription();
                columns[excelFilters[counter]] = counter + 1;
            }

            foreach (var item in result)
            {
                currentRow1++;
                if (columns.ContainsKey(FixAssetMachineryExcelEnum.Id))
                    worksheet1.Cells[currentRow1, columns[FixAssetMachineryExcelEnum.Id]].Value = item.Id;
                if (columns.ContainsKey(FixAssetMachineryExcelEnum.MachineryName))
                    worksheet1.Cells[currentRow1, columns[FixAssetMachineryExcelEnum.MachineryName]].Value = item.MachineryName;
                if (columns.ContainsKey(FixAssetMachineryExcelEnum.MachineryCode))
                    worksheet1.Cells[currentRow1, columns[FixAssetMachineryExcelEnum.MachineryCode]].Value = item.MachineryCode;
                if (columns.ContainsKey(FixAssetMachineryExcelEnum.TypeDesctiption))
                    worksheet1.Cells[currentRow1, columns[FixAssetMachineryExcelEnum.TypeDesctiption]].Value = item.TypeDesctiption;
                if (columns.ContainsKey(FixAssetMachineryExcelEnum.DriverFullname))
                    worksheet1.Cells[currentRow1, columns[FixAssetMachineryExcelEnum.DriverFullname]].Value = item.DriverFullname;
                if (columns.ContainsKey(FixAssetMachineryExcelEnum.StartDateShamsi))
                    worksheet1.Cells[currentRow1, columns[FixAssetMachineryExcelEnum.StartDateShamsi]].Value = item.StartDateShamsi;
                if (columns.ContainsKey(FixAssetMachineryExcelEnum.EndDateShamsi))
                    worksheet1.Cells[currentRow1, columns[FixAssetMachineryExcelEnum.EndDateShamsi]].Value = item.EndDateShamsi;
                if (columns.ContainsKey(FixAssetMachineryExcelEnum.Description))
                    worksheet1.Cells[currentRow1, columns[FixAssetMachineryExcelEnum.Description]].Value = item.Description;
                if (columns.ContainsKey(FixAssetMachineryExcelEnum.MachinerySpecification))
                    worksheet1.Cells[currentRow1, columns[FixAssetMachineryExcelEnum.MachinerySpecification]].Value = item.MachinerySpecification;
                if (columns.ContainsKey(FixAssetMachineryExcelEnum.NumberPlates))
                    worksheet1.Cells[currentRow1, columns[FixAssetMachineryExcelEnum.NumberPlates]].Value = item.NumberPlates;
                if (columns.ContainsKey(FixAssetMachineryExcelEnum.MachineryPrice))
                    worksheet1.Cells[currentRow1, columns[FixAssetMachineryExcelEnum.MachineryPrice]].Value = item.MachineryPrice;
                if (columns.ContainsKey(FixAssetMachineryExcelEnum.HourlyRate))
                    worksheet1.Cells[currentRow1, columns[FixAssetMachineryExcelEnum.HourlyRate]].Value = item.HourlyRate;
                if (columns.ContainsKey(FixAssetMachineryExcelEnum.DailyRate))
                    worksheet1.Cells[currentRow1, columns[FixAssetMachineryExcelEnum.DailyRate]].Value = item.DailyRate;
                if (columns.ContainsKey(FixAssetMachineryExcelEnum.ServiceRate))
                    worksheet1.Cells[currentRow1, columns[FixAssetMachineryExcelEnum.ServiceRate]].Value = item.ServiceRate;
                if (columns.ContainsKey(FixAssetMachineryExcelEnum.Contractor))
                    worksheet1.Cells[currentRow1, columns[FixAssetMachineryExcelEnum.Contractor]].Value = item.Contractor;
                if (columns.ContainsKey(FixAssetMachineryExcelEnum.IsActive))
                    worksheet1.Cells[currentRow1, columns[FixAssetMachineryExcelEnum.IsActive]].Value = item.IsActive;
                if (columns.ContainsKey(FixAssetMachineryExcelEnum.CompanyName))
                    worksheet1.Cells[currentRow1, columns[FixAssetMachineryExcelEnum.CompanyName]].Value = item.CompanyName;
            }
        }
        else
        {
            worksheet1.Cells[1, 1].Value = FixAssetMachineryExcelEnum.Id.GetEnumDescription();
            worksheet1.Cells[1, 2].Value = FixAssetMachineryExcelEnum.MachineryName.GetEnumDescription();
            worksheet1.Cells[1, 3].Value = FixAssetMachineryExcelEnum.MachineryCode.GetEnumDescription();
            worksheet1.Cells[1, 4].Value = FixAssetMachineryExcelEnum.TypeDesctiption.GetEnumDescription();
            worksheet1.Cells[1, 5].Value = FixAssetMachineryExcelEnum.DriverFullname.GetEnumDescription();
            worksheet1.Cells[1, 6].Value = FixAssetMachineryExcelEnum.StartDateShamsi.GetEnumDescription();
            worksheet1.Cells[1, 7].Value = FixAssetMachineryExcelEnum.EndDateShamsi.GetEnumDescription();
            worksheet1.Cells[1, 8].Value = FixAssetMachineryExcelEnum.Description.GetEnumDescription();
            worksheet1.Cells[1, 9].Value = FixAssetMachineryExcelEnum.MachinerySpecification.GetEnumDescription();
            worksheet1.Cells[1, 10].Value = FixAssetMachineryExcelEnum.NumberPlates.GetEnumDescription();
            worksheet1.Cells[1, 11].Value = FixAssetMachineryExcelEnum.MachineryPrice.GetEnumDescription();
            worksheet1.Cells[1, 12].Value = FixAssetMachineryExcelEnum.HourlyRate.GetEnumDescription();
            worksheet1.Cells[1, 13].Value = FixAssetMachineryExcelEnum.DailyRate.GetEnumDescription();
            worksheet1.Cells[1, 14].Value = FixAssetMachineryExcelEnum.ServiceRate.GetEnumDescription();
            worksheet1.Cells[1, 15].Value = FixAssetMachineryExcelEnum.Contractor.GetEnumDescription();
            worksheet1.Cells[1, 16].Value = FixAssetMachineryExcelEnum.IsActive.GetEnumDescription();
            worksheet1.Cells[1, 17].Value = FixAssetMachineryExcelEnum.CompanyName.GetEnumDescription();

            foreach (var item in result)
            {
                currentRow1++;
                worksheet1.Cells[currentRow1, 1].Value = item.Id;
                worksheet1.Cells[currentRow1, 2].Value = item.MachineryName;
                worksheet1.Cells[currentRow1, 3].Value = item.MachineryCode;
                worksheet1.Cells[currentRow1, 4].Value = item.TypeDesctiption;
                worksheet1.Cells[currentRow1, 5].Value = item.DriverFullname;
                worksheet1.Cells[currentRow1, 6].Value = item.StartDateShamsi;
                worksheet1.Cells[currentRow1, 7].Value = item.EndDateShamsi;
                worksheet1.Cells[currentRow1, 8].Value = item.Description;
                worksheet1.Cells[currentRow1, 9].Value = item.MachinerySpecification;
                worksheet1.Cells[currentRow1, 10].Value = item.NumberPlates;
                worksheet1.Cells[currentRow1, 11].Value = item.MachineryPrice;
                worksheet1.Cells[currentRow1, 12].Value = item.HourlyRate;
                worksheet1.Cells[currentRow1, 13].Value = item.DailyRate;
                worksheet1.Cells[currentRow1, 14].Value = item.ServiceRate;
                worksheet1.Cells[currentRow1, 15].Value = item.Contractor;
                worksheet1.Cells[currentRow1, 16].Value = item.IsActive;
                worksheet1.Cells[currentRow1, 17].Value = item.CompanyName;
            }
        }

        worksheet2.Cells[currentRow2, 1].Value = "شناسه عدم فعالیت";
        worksheet2.Cells[currentRow2, 2].Value = "شناسه موجودی ماشین آلات";
        worksheet2.Cells[currentRow2, 3].Value = "تاریخ شروع";
        worksheet2.Cells[currentRow2, 4].Value = "تاریخ پایان";
        worksheet2.Cells[currentRow2, 5].Value = "ساعت شروع";
        worksheet2.Cells[currentRow2, 6].Value = "ساعت پایان";
        worksheet2.Cells[currentRow2, 7].Value = "توضیحات";
        if (notWorks is not null && notWorks.Count > 0)
            foreach (var item in notWorks)
            {
                currentRow2++;
                worksheet2.Cells[currentRow2, 1].Value = item.Id;
                worksheet2.Cells[currentRow2, 2].Value = item.FixAssetMachineryId;
                worksheet2.Cells[currentRow2, 3].Value = item.FromDateShamsi;
                worksheet2.Cells[currentRow2, 4].Value = item.ToDateShamsi;
                worksheet2.Cells[currentRow2, 5].Value = item.FromTime;
                worksheet2.Cells[currentRow2, 6].Value = item.ToTime;
                worksheet2.Cells[currentRow2, 7].Value = item.Description;
            }

        worksheet3.Cells[currentRow3, 1].Value = "شناسه قیمت گزاری";
        worksheet3.Cells[currentRow3, 2].Value = "شناسه موجودی ماشین آلات";
        worksheet3.Cells[currentRow3, 3].Value = "تاریخ شروع";
        worksheet3.Cells[currentRow3, 4].Value = "تاریخ پایان";
        worksheet3.Cells[currentRow3, 5].Value = "ساعت شروع";
        worksheet3.Cells[currentRow3, 6].Value = "ساعت پایان";
        worksheet3.Cells[currentRow3, 7].Value = "نرخ ساعتی";
        worksheet3.Cells[currentRow3, 8].Value = "نرخ روزانه";
        worksheet3.Cells[currentRow3, 9].Value = "نرخ سرویسی";
        if (rates is not null && rates.Count > 0)
            foreach (var item in rates)
            {
                currentRow3++;
                worksheet3.Cells[currentRow3, 1].Value = item.Id;
                worksheet3.Cells[currentRow3, 2].Value = item.FixAssetMachineryId;
                worksheet3.Cells[currentRow3, 3].Value = item.FromDateShamsi;
                worksheet3.Cells[currentRow3, 4].Value = item.ToDateShamsi;
                worksheet3.Cells[currentRow3, 5].Value = item.FromTime;
                worksheet3.Cells[currentRow3, 6].Value = item.ToTime;
                worksheet3.Cells[currentRow3, 7].Value = item.HourlyRate;
                worksheet3.Cells[currentRow3, 8].Value = item.DailyRate;
                worksheet3.Cells[currentRow3, 9].Value = item.ServiceRate;
            }

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        var content = stream.ToArray();
        return content;
    }

    public static byte[] FixAssetMachineryNotworksToExcel(ICollection<GetsFixAssetMachineryNotWorkExcelExporterModel> result,
        List<FixAssetMachineryNotworkExcelEnum>? excelFilters)
    {
        using var workbook = new ExcelPackage();
        var worksheet = ExcelHelpers.CreateWorksheet(workbook, "لیست عدم فعالیت های موجودی ماشین آلات");

        List<FixAssetMachineryNotworkExcelEnum>? newFilters = [];
        if (excelFilters is null || excelFilters.Count <= 0)
        {
            newFilters = Enum.GetValues(typeof(FixAssetMachineryNotworkExcelEnum))
                .Cast<FixAssetMachineryNotworkExcelEnum>()
                .Select(x => x)
                .ToList();
        }
        else
        {
            newFilters = excelFilters;
        }

        var defaultHeaders = ExcelHelpers.GetDefaultHeaders<FixAssetMachineryNotworkExcelEnum>();
        var columns = ExcelHelpers.SetupHeaders<FixAssetMachineryNotworkExcelEnum>(
            worksheet,
            newFilters,
            defaultHeaders);

        var currentRow = 1;
        foreach (var item in result)
        {
            currentRow++;
            ExcelHelpers.FillRow<FixAssetMachineryNotworkExcelEnum, GetsFixAssetMachineryNotWorkExcelExporterModel>(
                worksheet,
                item,
                currentRow,
                columns);
        }

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }
}