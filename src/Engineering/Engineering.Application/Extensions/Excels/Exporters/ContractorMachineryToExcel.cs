using Engineering.Application.Services.ContractorMachineries.Models.GetsContractorMachineryExcelEnum;
using Engineering.Application.Services.ContractorMachineries.Models.GetsContractorMachineryExcelExporter;

namespace Engineering.Application.Extensions.Excels.Exporters;

public class ContractorMachineryExcels
{
    public static byte[] ContractorMachineryToExcel(ICollection<GetsContractorMachineryExcelExporterModel> result,
        List<ContractorMachineryExcelEnum>? excelFilters)
    {
        using var workbook = new XLWorkbook();

        var worksheet1 = workbook.Worksheets.Add("ماشین آلات پیمانکاری");
        var currentRow1 = 1;

        if (excelFilters != null && excelFilters.Count > 0)
        {
            var counter1 = 0;
            var id = 0;
            var contractor = 0;
            var contractorNickName = 0;
            var machineryGroupName = 0;
            var machineryGroupCode = 0;
            var machineryName = 0;
            var machineryCode = 0;
            var unitDesctiption = 0;
            var machineryPrice = 0;
            var currency = 0;
            var machineryIdentifier = 0;
            var numberPlates = 0;
            var isActive = 0;
            var description = 0;
            var creator = 0;
            var createdShamsi = 0;

            foreach (var item in excelFilters)
            {
                counter1++;
                switch (item)
                {
                    case ContractorMachineryExcelEnum.Id:
                        worksheet1.Cell(currentRow1, counter1).Value = ContractorMachineryExcelEnum.Id.GetEnumDescription();
                        id = counter1;
                        ; break;
                    case ContractorMachineryExcelEnum.Contractor:
                        worksheet1.Cell(currentRow1, counter1).Value = ContractorMachineryExcelEnum.Contractor.GetEnumDescription();
                        contractor = counter1;
                        ; break;
                    case ContractorMachineryExcelEnum.ContractorNickName:
                        worksheet1.Cell(currentRow1, counter1).Value = ContractorMachineryExcelEnum.ContractorNickName.GetEnumDescription();
                        contractorNickName = counter1;
                        ; break;
                    case ContractorMachineryExcelEnum.MachineryGroupName:
                        worksheet1.Cell(currentRow1, counter1).Value = ContractorMachineryExcelEnum.MachineryGroupName.GetEnumDescription();
                        machineryGroupName = counter1;
                        ; break;
                    case ContractorMachineryExcelEnum.MachineryGroupCode:
                        worksheet1.Cell(currentRow1, counter1).Value = ContractorMachineryExcelEnum.MachineryGroupCode.GetEnumDescription();
                        machineryGroupCode = counter1;
                        ; break;
                    case ContractorMachineryExcelEnum.MachineryName:
                        worksheet1.Cell(currentRow1, counter1).Value = ContractorMachineryExcelEnum.MachineryName.GetEnumDescription();
                        machineryName = counter1;
                        ; break;
                    case ContractorMachineryExcelEnum.MachineryCode:
                        worksheet1.Cell(currentRow1, counter1).Value = ContractorMachineryExcelEnum.MachineryCode.GetEnumDescription();
                        machineryCode = counter1;
                        ; break;
                    case ContractorMachineryExcelEnum.UnitDesctiption:
                        worksheet1.Cell(currentRow1, counter1).Value = ContractorMachineryExcelEnum.UnitDesctiption.GetEnumDescription();
                        unitDesctiption = counter1;
                        ; break;
                    case ContractorMachineryExcelEnum.MachineryPrice:
                        worksheet1.Cell(currentRow1, counter1).Value = ContractorMachineryExcelEnum.MachineryPrice.GetEnumDescription();
                        machineryPrice = counter1;
                        ; break;
                    case ContractorMachineryExcelEnum.Currency:
                        worksheet1.Cell(currentRow1, counter1).Value = ContractorMachineryExcelEnum.Currency.GetEnumDescription();
                        currency = counter1;
                        ; break;
                    case ContractorMachineryExcelEnum.MachineryIdentifier:
                        worksheet1.Cell(currentRow1, counter1).Value = ContractorMachineryExcelEnum.MachineryIdentifier.GetEnumDescription();
                        machineryIdentifier = counter1;
                        ; break;
                    case ContractorMachineryExcelEnum.NumberPlates:
                        worksheet1.Cell(currentRow1, counter1).Value = ContractorMachineryExcelEnum.NumberPlates.GetEnumDescription();
                        numberPlates = counter1;
                        ; break;
                    case ContractorMachineryExcelEnum.IsActive:
                        worksheet1.Cell(currentRow1, counter1).Value = ContractorMachineryExcelEnum.IsActive.GetEnumDescription();
                        isActive = counter1;
                        ; break;
                    case ContractorMachineryExcelEnum.Description:
                        worksheet1.Cell(currentRow1, counter1).Value = ContractorMachineryExcelEnum.Description.GetEnumDescription();
                        description = counter1;
                        ; break;
                    case ContractorMachineryExcelEnum.Creator:
                        worksheet1.Cell(currentRow1, counter1).Value = ContractorMachineryExcelEnum.Creator.GetEnumDescription();
                        creator = counter1;
                        ; break;
                    case ContractorMachineryExcelEnum.CreatedShamsi:
                        worksheet1.Cell(currentRow1, counter1).Value = ContractorMachineryExcelEnum.CreatedShamsi.GetEnumDescription();
                        createdShamsi = counter1;
                        ; break;
                }
            }

            foreach (var item in result)
            {
                currentRow1++;
                if (id > 0)
                    worksheet1.Cell(currentRow1, id).Value = item.Id;
                if (contractor > 0)
                    worksheet1.Cell(currentRow1, contractor).Value = item.Contractor;
                if (contractorNickName > 0)
                    worksheet1.Cell(currentRow1, contractorNickName).Value = item.ContractorNickName;
                if (machineryGroupName > 0)
                    worksheet1.Cell(currentRow1, machineryGroupName).Value = item.MachineryGroupName;
                if (machineryGroupCode > 0)
                    worksheet1.Cell(currentRow1, machineryGroupCode).Value = item.MachineryGroupCode;
                if (machineryName > 0)
                    worksheet1.Cell(currentRow1, machineryName).Value = item.MachineryName;
                if (machineryCode > 0)
                    worksheet1.Cell(currentRow1, machineryCode).Value = item.MachineryCode;
                if (unitDesctiption > 0)
                    worksheet1.Cell(currentRow1, unitDesctiption).Value = item.UnitDesctiption;
                if (machineryPrice > 0)
                    worksheet1.Cell(currentRow1, machineryPrice).Value = item.MachineryPrice;
                if (currency > 0)
                    worksheet1.Cell(currentRow1, currency).Value = item.Currency;
                if (machineryIdentifier > 0)
                    worksheet1.Cell(currentRow1, machineryIdentifier).Value = item.MachineryIdentifier;
                if (numberPlates > 0)
                    worksheet1.Cell(currentRow1, numberPlates).Value = item.NumberPlates;
                if (isActive > 0)
                    worksheet1.Cell(currentRow1, isActive).Value = item.IsActive == null ? false : item.IsActive.Value;
                if (description > 0)
                    worksheet1.Cell(currentRow1, description).Value = item.Description;
                if (creator > 0)
                    worksheet1.Cell(currentRow1, creator).Value = item.Creator;
                if (createdShamsi > 0)
                    worksheet1.Cell(currentRow1, createdShamsi).Value = item.CreatedShamsi;
            }
        }
        else
        {
            worksheet1.Cell(currentRow1, 1).Value = ContractorMachineryExcelEnum.Id.GetEnumDescription();
            worksheet1.Cell(currentRow1, 2).Value = ContractorMachineryExcelEnum.Contractor.GetEnumDescription();
            worksheet1.Cell(currentRow1, 3).Value = ContractorMachineryExcelEnum.ContractorNickName.GetEnumDescription();
            worksheet1.Cell(currentRow1, 4).Value = ContractorMachineryExcelEnum.MachineryGroupName.GetEnumDescription();
            worksheet1.Cell(currentRow1, 5).Value = ContractorMachineryExcelEnum.MachineryGroupCode.GetEnumDescription();
            worksheet1.Cell(currentRow1, 6).Value = ContractorMachineryExcelEnum.MachineryName.GetEnumDescription();
            worksheet1.Cell(currentRow1, 7).Value = ContractorMachineryExcelEnum.MachineryCode.GetEnumDescription();
            worksheet1.Cell(currentRow1, 8).Value = ContractorMachineryExcelEnum.UnitDesctiption.GetEnumDescription();
            worksheet1.Cell(currentRow1, 9).Value = ContractorMachineryExcelEnum.MachineryPrice.GetEnumDescription();
            worksheet1.Cell(currentRow1, 10).Value = ContractorMachineryExcelEnum.Currency.GetEnumDescription();
            worksheet1.Cell(currentRow1, 11).Value = ContractorMachineryExcelEnum.MachineryIdentifier.GetEnumDescription();
            worksheet1.Cell(currentRow1, 12).Value = ContractorMachineryExcelEnum.NumberPlates.GetEnumDescription();
            worksheet1.Cell(currentRow1, 13).Value = ContractorMachineryExcelEnum.IsActive.GetEnumDescription();
            worksheet1.Cell(currentRow1, 14).Value = ContractorMachineryExcelEnum.Description.GetEnumDescription();
            worksheet1.Cell(currentRow1, 15).Value = ContractorMachineryExcelEnum.Creator.GetEnumDescription();
            worksheet1.Cell(currentRow1, 16).Value = ContractorMachineryExcelEnum.CreatedShamsi.GetEnumDescription();

            foreach (var item in result)
            {
                currentRow1++;
                worksheet1.Cell(currentRow1, 1).Value = item.Id;
                worksheet1.Cell(currentRow1, 2).Value = item.Contractor;
                worksheet1.Cell(currentRow1, 3).Value = item.ContractorNickName;
                worksheet1.Cell(currentRow1, 4).Value = item.MachineryGroupName;
                worksheet1.Cell(currentRow1, 5).Value = item.MachineryGroupCode;
                worksheet1.Cell(currentRow1, 6).Value = item.MachineryName;
                worksheet1.Cell(currentRow1, 7).Value = item.MachineryCode;
                worksheet1.Cell(currentRow1, 8).Value = item.UnitDesctiption;
                worksheet1.Cell(currentRow1, 9).Value = item.MachineryPrice;
                worksheet1.Cell(currentRow1, 10).Value = item.Currency;
                worksheet1.Cell(currentRow1, 11).Value = item.MachineryIdentifier;
                worksheet1.Cell(currentRow1, 12).Value = item.NumberPlates;
                worksheet1.Cell(currentRow1, 13).Value = item.IsActive == null ? false : item.IsActive.Value;
                worksheet1.Cell(currentRow1, 14).Value = item.Description;
                worksheet1.Cell(currentRow1, 15).Value = item.Creator;
                worksheet1.Cell(currentRow1, 16).Value = item.CreatedShamsi;
            }
        }

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        var content = stream.ToArray();
        return content;
    }
}