using Engineering.Application.Services.RequestContractors.Models.GetFilteredRequestContractors;
using Engineering.Application.Services.RequestContractors.Models.GetsRequestContractorExcelEnums;

namespace Engineering.Application.Extensions.Excels.Exporters;

public class RequestContractorExcels
{
    public static byte[] RequestContractorToExcel(
        ICollection<GetFilteredRequestContractorsModel> result,
        List<GetsRequestContractorExcelEnum>? excelFilters)
    {
        using var workbook = new ExcelPackage();
        var worksheet = workbook.Workbook.Worksheets.Add("درخواست های پیمانکار");
        var currentRow = 1;
        worksheet.View.RightToLeft = true;

        if (excelFilters != null && excelFilters.Count > 0)
        {
            var columns = new Dictionary<GetsRequestContractorExcelEnum, int>();
            for (int counter = 0; counter < excelFilters.Count; counter++)
            {
                worksheet.Cells[1, counter + 1].Value = excelFilters[counter].GetEnumDescription();
                columns[excelFilters[counter]] = counter + 1;
            }

            foreach (var item in result)
            {
                currentRow++;
                if (columns.ContainsKey(GetsRequestContractorExcelEnum.RequestContractorId))
                    worksheet.Cells[currentRow, columns[GetsRequestContractorExcelEnum.RequestContractorId]].Value = item.RequestContractorId;
                if (columns.ContainsKey(GetsRequestContractorExcelEnum.StatusDescription))
                    worksheet.Cells[currentRow, columns[GetsRequestContractorExcelEnum.StatusDescription]].Value = item.StatusDescription;
                if (columns.ContainsKey(GetsRequestContractorExcelEnum.RequestNumber))
                    worksheet.Cells[currentRow, columns[GetsRequestContractorExcelEnum.RequestNumber]].Value = item.RequestNumber;
                if (columns.ContainsKey(GetsRequestContractorExcelEnum.CostCenterName))
                    worksheet.Cells[currentRow, columns[GetsRequestContractorExcelEnum.CostCenterName]].Value = item.CostCenterName;
                if (columns.ContainsKey(GetsRequestContractorExcelEnum.ProjectName))
                    worksheet.Cells[currentRow, columns[GetsRequestContractorExcelEnum.ProjectName]].Value = item.ProjectName;
                if (columns.ContainsKey(GetsRequestContractorExcelEnum.OperationInfoName))
                    worksheet.Cells[currentRow, columns[GetsRequestContractorExcelEnum.OperationInfoName]].Value = item.OperationInfoName;
                if (columns.ContainsKey(GetsRequestContractorExcelEnum.OperationInfoCode))
                    worksheet.Cells[currentRow, columns[GetsRequestContractorExcelEnum.OperationInfoCode]].Value = item.OperationInfoCode;
                if (columns.ContainsKey(GetsRequestContractorExcelEnum.PublicName))
                    worksheet.Cells[currentRow, columns[GetsRequestContractorExcelEnum.PublicName]].Value = item.PublicName;
                if (columns.ContainsKey(GetsRequestContractorExcelEnum.PublicCode))
                    worksheet.Cells[currentRow, columns[GetsRequestContractorExcelEnum.PublicCode]].Value = item.PublicCode;
                if (columns.ContainsKey(GetsRequestContractorExcelEnum.ServiceInfoName))
                    worksheet.Cells[currentRow, columns[GetsRequestContractorExcelEnum.ServiceInfoName]].Value = item.ServiceInfoName;
                if (columns.ContainsKey(GetsRequestContractorExcelEnum.ServiceInfoCode))
                    worksheet.Cells[currentRow, columns[GetsRequestContractorExcelEnum.ServiceInfoCode]].Value = item.ServiceInfoCode;
                if (columns.ContainsKey(GetsRequestContractorExcelEnum.Volume))
                    worksheet.Cells[currentRow, columns[GetsRequestContractorExcelEnum.Volume]].Value = item.Volume;
                if (columns.ContainsKey(GetsRequestContractorExcelEnum.Description))
                    worksheet.Cells[currentRow, columns[GetsRequestContractorExcelEnum.Description]].Value = item.Description;
                if (columns.ContainsKey(GetsRequestContractorExcelEnum.DescriptionStatus))
                    worksheet.Cells[currentRow, columns[GetsRequestContractorExcelEnum.DescriptionStatus]].Value = item.DescriptionStatus;
                if (columns.ContainsKey(GetsRequestContractorExcelEnum.Creator))
                    worksheet.Cells[currentRow, columns[GetsRequestContractorExcelEnum.Creator]].Value = item.Creator;
                if (columns.ContainsKey(GetsRequestContractorExcelEnum.Created))
                    worksheet.Cells[currentRow, columns[GetsRequestContractorExcelEnum.Created]].Value = TimeCalculator.TimeCalculator.ConvertToShamsi(item.Created);
                if (columns.ContainsKey(GetsRequestContractorExcelEnum.ContractorName))
                    worksheet.Cells[currentRow, columns[GetsRequestContractorExcelEnum.ContractorName]].Value = item.ContractorName;
                if (columns.ContainsKey(GetsRequestContractorExcelEnum.ContractorNickName))
                    worksheet.Cells[currentRow, columns[GetsRequestContractorExcelEnum.ContractorNickName]].Value = item.ContractorNickName;
                if (columns.ContainsKey(GetsRequestContractorExcelEnum.TotalAmount))
                    worksheet.Cells[currentRow, columns[GetsRequestContractorExcelEnum.TotalAmount]].Value = item.TotalAmount;
                if (columns.ContainsKey(GetsRequestContractorExcelEnum.Amount))
                    worksheet.Cells[currentRow, columns[GetsRequestContractorExcelEnum.Amount]].Value = item.Amount;
                if (columns.ContainsKey(GetsRequestContractorExcelEnum.Discount))
                    worksheet.Cells[currentRow, columns[GetsRequestContractorExcelEnum.Discount]].Value = item.Discount;
                if (columns.ContainsKey(GetsRequestContractorExcelEnum.Tax))
                    worksheet.Cells[currentRow, columns[GetsRequestContractorExcelEnum.Tax]].Value = item.Tax;
                if (columns.ContainsKey(GetsRequestContractorExcelEnum.CurrencyName))
                    worksheet.Cells[currentRow, columns[GetsRequestContractorExcelEnum.CurrencyName]].Value = item.CurrencyName;
                if (columns.ContainsKey(GetsRequestContractorExcelEnum.TypeDescription))
                    worksheet.Cells[currentRow, columns[GetsRequestContractorExcelEnum.TypeDescription]].Value = item.TypeDescription;
                if (columns.ContainsKey(GetsRequestContractorExcelEnum.FromDate))
                    worksheet.Cells[currentRow, columns[GetsRequestContractorExcelEnum.FromDate]].Value = TimeCalculator.TimeCalculator.ConvertToShamsi(item.FromDate);
                if (columns.ContainsKey(GetsRequestContractorExcelEnum.ToDate))
                    worksheet.Cells[currentRow, columns[GetsRequestContractorExcelEnum.ToDate]].Value = TimeCalculator.TimeCalculator.ConvertToShamsi(item.ToDate);
                if (columns.ContainsKey(GetsRequestContractorExcelEnum.ConfirmUserName))
                    worksheet.Cells[currentRow, columns[GetsRequestContractorExcelEnum.ConfirmUserName]].Value = item.ConfirmUserName;
            }
        }
        else
        {
            worksheet.Cells[currentRow, 1].Value = GetsRequestContractorExcelEnum.RequestContractorId.GetEnumDescription();
            worksheet.Cells[currentRow, 2].Value = GetsRequestContractorExcelEnum.StatusDescription.GetEnumDescription();
            worksheet.Cells[currentRow, 3].Value = GetsRequestContractorExcelEnum.RequestNumber.GetEnumDescription();
            worksheet.Cells[currentRow, 4].Value = GetsRequestContractorExcelEnum.CostCenterName.GetEnumDescription();
            worksheet.Cells[currentRow, 5].Value = GetsRequestContractorExcelEnum.ProjectName.GetEnumDescription();
            worksheet.Cells[currentRow, 6].Value = GetsRequestContractorExcelEnum.OperationInfoName.GetEnumDescription();
            worksheet.Cells[currentRow, 7].Value = GetsRequestContractorExcelEnum.OperationInfoCode.GetEnumDescription();
            worksheet.Cells[currentRow, 8].Value = GetsRequestContractorExcelEnum.PublicName.GetEnumDescription();
            worksheet.Cells[currentRow, 9].Value = GetsRequestContractorExcelEnum.PublicCode.GetEnumDescription();
            worksheet.Cells[currentRow, 10].Value = GetsRequestContractorExcelEnum.ServiceInfoName.GetEnumDescription();
            worksheet.Cells[currentRow, 11].Value = GetsRequestContractorExcelEnum.ServiceInfoCode.GetEnumDescription();
            worksheet.Cells[currentRow, 12].Value = GetsRequestContractorExcelEnum.Volume.GetEnumDescription();
            worksheet.Cells[currentRow, 13].Value = GetsRequestContractorExcelEnum.Description.GetEnumDescription();
            worksheet.Cells[currentRow, 14].Value = GetsRequestContractorExcelEnum.DescriptionStatus.GetEnumDescription();
            worksheet.Cells[currentRow, 15].Value = GetsRequestContractorExcelEnum.Creator.GetEnumDescription();
            worksheet.Cells[currentRow, 16].Value = GetsRequestContractorExcelEnum.Created.GetEnumDescription();
            worksheet.Cells[currentRow, 17].Value = GetsRequestContractorExcelEnum.ContractorName.GetEnumDescription();
            worksheet.Cells[currentRow, 18].Value = GetsRequestContractorExcelEnum.ContractorNickName.GetEnumDescription();
            worksheet.Cells[currentRow, 19].Value = GetsRequestContractorExcelEnum.TotalAmount.GetEnumDescription();
            worksheet.Cells[currentRow, 20].Value = GetsRequestContractorExcelEnum.Amount.GetEnumDescription();
            worksheet.Cells[currentRow, 21].Value = GetsRequestContractorExcelEnum.Discount.GetEnumDescription();
            worksheet.Cells[currentRow, 22].Value = GetsRequestContractorExcelEnum.Tax.GetEnumDescription();
            worksheet.Cells[currentRow, 23].Value = GetsRequestContractorExcelEnum.CurrencyName.GetEnumDescription();
            worksheet.Cells[currentRow, 24].Value = GetsRequestContractorExcelEnum.TypeDescription.GetEnumDescription();
            worksheet.Cells[currentRow, 25].Value = GetsRequestContractorExcelEnum.FromDate.GetEnumDescription();
            worksheet.Cells[currentRow, 26].Value = GetsRequestContractorExcelEnum.ToDate.GetEnumDescription();
            worksheet.Cells[currentRow, 27].Value = GetsRequestContractorExcelEnum.ConfirmUserName.GetEnumDescription();

            foreach (var item in result)
            {
                currentRow++;
                worksheet.Cells[currentRow, 1].Value = item.RequestContractorId;
                worksheet.Cells[currentRow, 2].Value = item.StatusDescription;
                worksheet.Cells[currentRow, 3].Value = item.RequestNumber;
                worksheet.Cells[currentRow, 4].Value = item.CostCenterName;
                worksheet.Cells[currentRow, 5].Value = item.ProjectName;
                worksheet.Cells[currentRow, 6].Value = item.OperationInfoName;
                worksheet.Cells[currentRow, 7].Value = item.OperationInfoCode;
                worksheet.Cells[currentRow, 8].Value = item.PublicName;
                worksheet.Cells[currentRow, 9].Value = item.PublicCode;
                worksheet.Cells[currentRow, 10].Value = item.ServiceInfoName;
                worksheet.Cells[currentRow, 11].Value = item.ServiceInfoCode;
                worksheet.Cells[currentRow, 12].Value = item.Volume;
                worksheet.Cells[currentRow, 13].Value = item.Description;
                worksheet.Cells[currentRow, 14].Value = item.DescriptionStatus;
                worksheet.Cells[currentRow, 15].Value = item.Creator;
                worksheet.Cells[currentRow, 16].Value = TimeCalculator.TimeCalculator.ConvertToShamsi(item.Created);
                worksheet.Cells[currentRow, 17].Value = item.ContractorName;
                worksheet.Cells[currentRow, 18].Value = item.ContractorNickName;
                worksheet.Cells[currentRow, 19].Value = item.TotalAmount;
                worksheet.Cells[currentRow, 20].Value = item.Amount;
                worksheet.Cells[currentRow, 21].Value = item.Discount;
                worksheet.Cells[currentRow, 22].Value = item.Tax;
                worksheet.Cells[currentRow, 23].Value = item.CurrencyName;
                worksheet.Cells[currentRow, 24].Value = item.TypeDescription;
                worksheet.Cells[currentRow, 25].Value = TimeCalculator.TimeCalculator.ConvertToShamsi(item.FromDate);
                worksheet.Cells[currentRow, 26].Value = TimeCalculator.TimeCalculator.ConvertToShamsi(item.ToDate);
                worksheet.Cells[currentRow, 27].Value = item.ConfirmUserName;
            }
        }

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        var content = stream.ToArray();
        return content;
    }
}