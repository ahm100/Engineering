using Engineering.Application.Services.ContractorContracts.Contracts.GetConfirmedCCDailyServices;
using Engineering.Application.Services.ContractorContracts.Contracts.GetConfirmedCCDailyServices.Enum;
using Engineering.Application.Services.ContractorContracts.Contracts.GetDraftedFixCCs;
using Engineering.Application.Services.ContractorContracts.Contracts.GetDraftedFixCCs.Enum;
using Engineering.Application.Services.ContractorContracts.Contracts.GetDraftedServiceCCs;
using Engineering.Application.Services.ContractorContracts.Contracts.GetDraftedServiceCCs.Enum;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsContractorContractDetailPrice.Enum;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsContractorContractDetailPrice.Exporter;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsContractorContractReports.Enum;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsContractorContractReports.Exporter;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsContractorContractServiceReport;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsContractorContractServiceReport.Enum;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsFilteredContractorContractHeader.Enum;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsFilteredContractorContractHeader.Exporter;
using Engineering.Application.Services.ContractorContracts.Contracts.GetSuggestedServicePrice;
using Engineering.Application.Services.ContractorContracts.Contracts.GetSuggestedServicePrice.Enum;

namespace Engineering.Application.Extensions.Excels.Exporters;

public class ContractorContractExcels
{
    public static byte[] ContractorContractToExcel(ICollection<GetsContractorContractExcelExporterModel> result,
        List<ContractorContractExcelEnum>? excelFilters)
    {
        using var workbook = new XLWorkbook();

        var worksheet = workbook.Worksheets.Add("قرارداد پیمانکار");
        var currentRow = 1;

        if (excelFilters != null && excelFilters.Count > 0)
        {
            var counter = 0;
            var id = 0;
            var startDate = 0;
            var endDate = 0;
            var statusDescription = 0;
            var typeDescription = 0;
            var contractorId = 0;
            var contractor = 0;
            var creatorId = 0;
            var creator = 0;
            var price = 0;
            var currencyId = 0;
            var currency = 0;
            var workDonePercent = 0;
            var workDeliveryPercent = 0;
            var workCompletionPercent = 0;
            var description = 0;
            var companyId = 0;
            var companyNameFa = 0;

            foreach (var item in excelFilters)
            {
                counter++;
                switch (item)
                {
                    case ContractorContractExcelEnum.Id:
                        worksheet.Cell(currentRow, counter).Value = ContractorContractExcelEnum.Id.GetEnumDescription();
                        id = counter;
                        ;
                        break;
                    case ContractorContractExcelEnum.StartDate:
                        worksheet.Cell(currentRow, counter).Value =
                            ContractorContractExcelEnum.StartDate.GetEnumDescription();
                        startDate = counter;
                        ;
                        break;
                    case ContractorContractExcelEnum.EndDate:
                        worksheet.Cell(currentRow, counter).Value =
                            ContractorContractExcelEnum.EndDate.GetEnumDescription();
                        endDate = counter;
                        ;
                        break;
                    case ContractorContractExcelEnum.StatusDescription:
                        worksheet.Cell(currentRow, counter).Value =
                            ContractorContractExcelEnum.StatusDescription.GetEnumDescription();
                        statusDescription = counter;
                        ;
                        break;
                    case ContractorContractExcelEnum.ContractorContractType:
                        worksheet.Cell(currentRow, counter).Value =
                            ContractorContractExcelEnum.ContractorContractType.GetEnumDescription();
                        typeDescription = counter;
                        ;
                        break;
                    case ContractorContractExcelEnum.ContractorId:
                        worksheet.Cell(currentRow, counter).Value =
                            ContractorContractExcelEnum.ContractorId.GetEnumDescription();
                        contractorId = counter;
                        ;
                        break;
                    case ContractorContractExcelEnum.Contractor:
                        worksheet.Cell(currentRow, counter).Value =
                            ContractorContractExcelEnum.Contractor.GetEnumDescription();
                        contractor = counter;
                        ;
                        break;
                    case ContractorContractExcelEnum.CreatorId:
                        worksheet.Cell(currentRow, counter).Value =
                            ContractorContractExcelEnum.CreatorId.GetEnumDescription();
                        creatorId = counter;
                        ;
                        break;
                    case ContractorContractExcelEnum.Creator:
                        worksheet.Cell(currentRow, counter).Value =
                            ContractorContractExcelEnum.Creator.GetEnumDescription();
                        creator = counter;
                        ;
                        break;
                    case ContractorContractExcelEnum.Price:
                        worksheet.Cell(currentRow, counter).Value =
                            ContractorContractExcelEnum.Price.GetEnumDescription();
                        price = counter;
                        ;
                        break;
                    case ContractorContractExcelEnum.CurrencyId:
                        worksheet.Cell(currentRow, counter).Value =
                            ContractorContractExcelEnum.CurrencyId.GetEnumDescription();
                        currencyId = counter;
                        ;
                        break;
                    case ContractorContractExcelEnum.Currency:
                        worksheet.Cell(currentRow, counter).Value =
                            ContractorContractExcelEnum.Currency.GetEnumDescription();
                        currency = counter;
                        ;
                        break;
                    case ContractorContractExcelEnum.WorkDonePercent:
                        worksheet.Cell(currentRow, counter).Value =
                            ContractorContractExcelEnum.WorkDonePercent.GetEnumDescription();
                        workDonePercent = counter;
                        ;
                        break;
                    case ContractorContractExcelEnum.WorkDeliveryPercent:
                        worksheet.Cell(currentRow, counter).Value =
                            ContractorContractExcelEnum.WorkDeliveryPercent.GetEnumDescription();
                        workDeliveryPercent = counter;
                        ;
                        break;
                    case ContractorContractExcelEnum.WorkCompletionPercent:
                        worksheet.Cell(currentRow, counter).Value =
                            ContractorContractExcelEnum.WorkCompletionPercent.GetEnumDescription();
                        workCompletionPercent = counter;
                        ;
                        break;
                    case ContractorContractExcelEnum.Description:
                        worksheet.Cell(currentRow, counter).Value =
                            ContractorContractExcelEnum.Description.GetEnumDescription();
                        description = counter;
                        ;
                        break;
                    case ContractorContractExcelEnum.CompanyId:
                        worksheet.Cell(currentRow, counter).Value =
                            ContractorContractExcelEnum.CompanyId.GetEnumDescription();
                        companyId = counter;
                        ;
                        break;
                    case ContractorContractExcelEnum.CompanyNameFa:
                        worksheet.Cell(currentRow, counter).Value =
                            ContractorContractExcelEnum.CompanyNameFa.GetEnumDescription();
                        companyNameFa = counter;
                        ;
                        break;
                }
            }

            foreach (var item in result)
            {
                currentRow++;
                if (id > 0)
                    worksheet.Cell(currentRow, id).Value = item.Id;
                if (startDate > 0)
                    worksheet.Cell(currentRow, startDate).Value = item.StartDate;
                if (endDate > 0)
                    worksheet.Cell(currentRow, endDate).Value = item.EndDate;
                if (statusDescription > 0)
                    worksheet.Cell(currentRow, statusDescription).Value = item.StatusDescription;
                if (typeDescription > 0)
                    worksheet.Cell(currentRow, typeDescription).Value = item.ContractorContractType;
                if (contractorId > 0)
                    worksheet.Cell(currentRow, contractorId).Value = item.ContractorId;
                if (contractor > 0)
                    worksheet.Cell(currentRow, contractor).Value = item.Contractor;
                if (creatorId > 0)
                    worksheet.Cell(currentRow, creatorId).Value = item.CreatorId;
                if (creator > 0)
                    worksheet.Cell(currentRow, creator).Value = item.Creator;
                if (price > 0)
                    worksheet.Cell(currentRow, price).Value = item.Price;
                if (currencyId > 0)
                    worksheet.Cell(currentRow, currencyId).Value = item.CurrencyId;
                if (currency > 0)
                    worksheet.Cell(currentRow, currency).Value = item.Currency;
                if (workDonePercent > 0)
                    worksheet.Cell(currentRow, workDonePercent).Value = item.WorkDonePercent;
                if (workDeliveryPercent > 0)
                    worksheet.Cell(currentRow, workDeliveryPercent).Value = item.WorkDeliveryPercent;
                if (workCompletionPercent > 0)
                    worksheet.Cell(currentRow, workCompletionPercent).Value = item.WorkCompletionPercent;
                if (description > 0)
                    worksheet.Cell(currentRow, description).Value = item.Description;
                if (companyId > 0)
                    worksheet.Cell(currentRow, companyId).Value = item.CompanyId;
                if (companyNameFa > 0)
                    worksheet.Cell(currentRow, companyNameFa).Value = item.CompanyNameFa;
            }
        }
        else
        {
            worksheet.Cell(currentRow, 1).Value = ContractorContractExcelEnum.Id.GetEnumDescription();
            worksheet.Cell(currentRow, 2).Value = ContractorContractExcelEnum.StartDate.GetEnumDescription();
            worksheet.Cell(currentRow, 3).Value = ContractorContractExcelEnum.EndDate.GetEnumDescription();
            worksheet.Cell(currentRow, 4).Value = ContractorContractExcelEnum.StatusDescription.GetEnumDescription();
            worksheet.Cell(currentRow, 5).Value =
                ContractorContractExcelEnum.ContractorContractType.GetEnumDescription();
            worksheet.Cell(currentRow, 6).Value = ContractorContractExcelEnum.ContractorId.GetEnumDescription();
            worksheet.Cell(currentRow, 7).Value = ContractorContractExcelEnum.Contractor.GetEnumDescription();
            worksheet.Cell(currentRow, 8).Value = ContractorContractExcelEnum.CreatorId.GetEnumDescription();
            worksheet.Cell(currentRow, 9).Value = ContractorContractExcelEnum.Creator.GetEnumDescription();
            worksheet.Cell(currentRow, 10).Value = ContractorContractExcelEnum.Price.GetEnumDescription();
            worksheet.Cell(currentRow, 11).Value = ContractorContractExcelEnum.CurrencyId.GetEnumDescription();
            worksheet.Cell(currentRow, 12).Value = ContractorContractExcelEnum.Currency.GetEnumDescription();
            worksheet.Cell(currentRow, 13).Value = ContractorContractExcelEnum.WorkDonePercent.GetEnumDescription();
            worksheet.Cell(currentRow, 14).Value = ContractorContractExcelEnum.WorkDeliveryPercent.GetEnumDescription();
            worksheet.Cell(currentRow, 15).Value =
                ContractorContractExcelEnum.WorkCompletionPercent.GetEnumDescription();
            worksheet.Cell(currentRow, 16).Value = ContractorContractExcelEnum.Description.GetEnumDescription();
            worksheet.Cell(currentRow, 17).Value = ContractorContractExcelEnum.CompanyId.GetEnumDescription();
            worksheet.Cell(currentRow, 18).Value = ContractorContractExcelEnum.CompanyNameFa.GetEnumDescription();

            foreach (var item in result)
            {
                currentRow++;
                worksheet.Cell(currentRow, 1).Value = item.Id;
                worksheet.Cell(currentRow, 2).Value = item.StartDate;
                worksheet.Cell(currentRow, 3).Value = item.EndDate;
                worksheet.Cell(currentRow, 4).Value = item.StatusDescription;
                worksheet.Cell(currentRow, 5).Value = item.ContractorContractType;
                worksheet.Cell(currentRow, 6).Value = item.ContractorId;
                worksheet.Cell(currentRow, 7).Value = item.Contractor;
                worksheet.Cell(currentRow, 8).Value = item.CreatorId;
                worksheet.Cell(currentRow, 9).Value = item.Creator;
                worksheet.Cell(currentRow, 10).Value = item.Price;
                worksheet.Cell(currentRow, 11).Value = item.CurrencyId;
                worksheet.Cell(currentRow, 12).Value = item.Currency;
                worksheet.Cell(currentRow, 13).Value = item.WorkDeliveryPercent;
                worksheet.Cell(currentRow, 14).Value = item.WorkDeliveryPercent;
                worksheet.Cell(currentRow, 15).Value = item.WorkCompletionPercent;
                worksheet.Cell(currentRow, 16).Value = item.Description;
                worksheet.Cell(currentRow, 17).Value = item.CompanyId;
                worksheet.Cell(currentRow, 18).Value = item.CompanyNameFa;
            }
        }

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        var content = stream.ToArray();
        return content;
    }

    public static byte[] ContractorContractReportsToExcel(
        ICollection<GetsContractorContractReportsExcelExporterModel> result,
        List<ContractorContractReportsExcelEnum>? excelFilters)
    {
        using var workbook = new XLWorkbook();

        var worksheet = workbook.Worksheets.Add("گزارش قرارداد پیمانکاری");
        var currentRow = 1;

        if (excelFilters != null && excelFilters.Count > 0)
        {
            var counter = 0;
            var contractorId = 0;
            var contractor = 0;
            var id = 0;
            var contractorContractTypeId = 0;
            var contractorContractTypeName = 0;
            var contractorContractTypeCode = 0;
            var statusDescription = 0;
            var currencyId = 0;
            var currency = 0;
            var startDate = 0;
            var endDate = 0;
            var totalAmount = 0;
            var percentageDoingJobWell = 0;
            var doingJobWellAmount = 0;
            var percentageAdvancePayment = 0;
            var advancePaymentAmount = 0;
            var dailyLatenessPenalty = 0;
            var workDonePercent = 0;
            var workDeliveryPercent = 0;
            var workCompletionPercent = 0;
            var description = 0;
            var creatorId = 0;
            var creator = 0;
            var created = 0;

            foreach (var item in excelFilters)
            {
                counter++;
                switch (item)
                {
                    case ContractorContractReportsExcelEnum.ContractorId:
                        worksheet.Cell(currentRow, counter).Value =
                            ContractorContractReportsExcelEnum.ContractorId.GetEnumDescription();
                        contractorId = counter;
                        break;
                    case ContractorContractReportsExcelEnum.Contractor:
                        worksheet.Cell(currentRow, counter).Value =
                            ContractorContractReportsExcelEnum.Contractor.GetEnumDescription();
                        contractor = counter;
                        break;
                    case ContractorContractReportsExcelEnum.Id:
                        worksheet.Cell(currentRow, counter).Value =
                            ContractorContractReportsExcelEnum.Id.GetEnumDescription();
                        id = counter;
                        break;
                    case ContractorContractReportsExcelEnum.ContractorContractTypeId:
                        worksheet.Cell(currentRow, counter).Value = ContractorContractReportsExcelEnum
                            .ContractorContractTypeId.GetEnumDescription();
                        contractorContractTypeId = counter;
                        break;
                    case ContractorContractReportsExcelEnum.ContractorContractTypeName:
                        worksheet.Cell(currentRow, counter).Value = ContractorContractReportsExcelEnum
                            .ContractorContractTypeName.GetEnumDescription();
                        contractorContractTypeName = counter;
                        break;
                    case ContractorContractReportsExcelEnum.ContractorContractTypeCode:
                        worksheet.Cell(currentRow, counter).Value = ContractorContractReportsExcelEnum
                            .ContractorContractTypeCode.GetEnumDescription();
                        contractorContractTypeCode = counter;
                        break;
                    case ContractorContractReportsExcelEnum.StatusDescription:
                        worksheet.Cell(currentRow, counter).Value =
                            ContractorContractReportsExcelEnum.StatusDescription.GetEnumDescription();
                        statusDescription = counter;
                        break;
                    case ContractorContractReportsExcelEnum.CurrencyId:
                        worksheet.Cell(currentRow, counter).Value =
                            ContractorContractReportsExcelEnum.CurrencyId.GetEnumDescription();
                        currencyId = counter;
                        break;
                    case ContractorContractReportsExcelEnum.Currency:
                        worksheet.Cell(currentRow, counter).Value =
                            ContractorContractReportsExcelEnum.Currency.GetEnumDescription();
                        currency = counter;
                        break;
                    case ContractorContractReportsExcelEnum.StartDate:
                        worksheet.Cell(currentRow, counter).Value =
                            ContractorContractReportsExcelEnum.StartDate.GetEnumDescription();
                        startDate = counter;
                        break;
                    case ContractorContractReportsExcelEnum.EndDate:
                        worksheet.Cell(currentRow, counter).Value =
                            ContractorContractReportsExcelEnum.EndDate.GetEnumDescription();
                        endDate = counter;
                        break;
                    case ContractorContractReportsExcelEnum.TotalAmount:
                        worksheet.Cell(currentRow, counter).Value =
                            ContractorContractReportsExcelEnum.TotalAmount.GetEnumDescription();
                        totalAmount = counter;
                        break;
                    case ContractorContractReportsExcelEnum.PercentageDoingJobWell:
                        worksheet.Cell(currentRow, counter).Value = ContractorContractReportsExcelEnum
                            .PercentageDoingJobWell.GetEnumDescription();
                        percentageDoingJobWell = counter;
                        break;
                    case ContractorContractReportsExcelEnum.DoingJobWellAmount:
                        worksheet.Cell(currentRow, counter).Value =
                            ContractorContractReportsExcelEnum.DoingJobWellAmount.GetEnumDescription();
                        doingJobWellAmount = counter;
                        break;
                    case ContractorContractReportsExcelEnum.PercentageAdvancePayment:
                        worksheet.Cell(currentRow, counter).Value = ContractorContractReportsExcelEnum
                            .PercentageAdvancePayment.GetEnumDescription();
                        percentageAdvancePayment = counter;
                        break;
                    case ContractorContractReportsExcelEnum.AdvancePaymentAmount:
                        worksheet.Cell(currentRow, counter).Value = ContractorContractReportsExcelEnum
                            .AdvancePaymentAmount.GetEnumDescription();
                        advancePaymentAmount = counter;
                        break;
                    case ContractorContractReportsExcelEnum.DailyLatenessPenalty:
                        worksheet.Cell(currentRow, counter).Value = ContractorContractReportsExcelEnum
                            .DailyLatenessPenalty.GetEnumDescription();
                        dailyLatenessPenalty = counter;
                        break;
                    case ContractorContractReportsExcelEnum.WorkDonePercent:
                        worksheet.Cell(currentRow, counter).Value =
                            ContractorContractReportsExcelEnum.WorkDonePercent.GetEnumDescription();
                        workDonePercent = counter;
                        break;
                    case ContractorContractReportsExcelEnum.WorkDeliveryPercent:
                        worksheet.Cell(currentRow, counter).Value =
                            ContractorContractReportsExcelEnum.WorkDeliveryPercent.GetEnumDescription();
                        workDeliveryPercent = counter;
                        break;
                    case ContractorContractReportsExcelEnum.WorkCompletionPercent:
                        worksheet.Cell(currentRow, counter).Value = ContractorContractReportsExcelEnum
                            .WorkCompletionPercent.GetEnumDescription();
                        workCompletionPercent = counter;
                        break;
                    case ContractorContractReportsExcelEnum.Description:
                        worksheet.Cell(currentRow, counter).Value =
                            ContractorContractReportsExcelEnum.Description.GetEnumDescription();
                        description = counter;
                        break;
                    case ContractorContractReportsExcelEnum.CreatorId:
                        worksheet.Cell(currentRow, counter).Value =
                            ContractorContractReportsExcelEnum.CreatorId.GetEnumDescription();
                        creatorId = counter;
                        break;
                    case ContractorContractReportsExcelEnum.Creator:
                        worksheet.Cell(currentRow, counter).Value =
                            ContractorContractReportsExcelEnum.Creator.GetEnumDescription();
                        creator = counter;
                        break;
                    case ContractorContractReportsExcelEnum.Created:
                        worksheet.Cell(currentRow, counter).Value =
                            ContractorContractReportsExcelEnum.Created.GetEnumDescription();
                        created = counter;
                        break;
                }
            }

            foreach (var item in result)
            {
                currentRow++;
                if (contractorId > 0)
                    worksheet.Cell(currentRow, contractorId).Value = item.ContractorId;
                if (contractor > 0)
                    worksheet.Cell(currentRow, contractor).Value = item.Contractor;
                if (id > 0)
                    worksheet.Cell(currentRow, id).Value = item.Id;
                if (contractorContractTypeId > 0)
                    worksheet.Cell(currentRow, contractorContractTypeId).Value = item.ContractorContractTypeId;
                if (contractorContractTypeName > 0)
                    worksheet.Cell(currentRow, contractorContractTypeName).Value = item.ContractorContractTypeName;
                if (contractorContractTypeCode > 0)
                    worksheet.Cell(currentRow, contractorContractTypeCode).Value = item.ContractorContractTypeCode;
                if (statusDescription > 0)
                    worksheet.Cell(currentRow, statusDescription).Value = item.StatusDescription;
                if (currencyId > 0)
                    worksheet.Cell(currentRow, currencyId).Value = item.CurrencyId;
                if (currency > 0)
                    worksheet.Cell(currentRow, currency).Value = item.Currency;
                if (startDate > 0)
                    worksheet.Cell(currentRow, startDate).Value = item.StartDate;
                if (endDate > 0)
                    worksheet.Cell(currentRow, endDate).Value = item.EndDate;
                if (totalAmount > 0)
                    worksheet.Cell(currentRow, totalAmount).Value = item.TotalAmount;
                if (percentageDoingJobWell > 0)
                    worksheet.Cell(currentRow, percentageDoingJobWell).Value = item.PercentageDoingJobWell;
                if (doingJobWellAmount > 0)
                    worksheet.Cell(currentRow, doingJobWellAmount).Value = item.DoingJobWellAmount;
                if (percentageAdvancePayment > 0)
                    worksheet.Cell(currentRow, percentageAdvancePayment).Value = item.PercentageAdvancePayment;
                if (advancePaymentAmount > 0)
                    worksheet.Cell(currentRow, advancePaymentAmount).Value = item.AdvancePaymentAmount;
                if (dailyLatenessPenalty > 0)
                    worksheet.Cell(currentRow, dailyLatenessPenalty).Value = item.DailyLatenessPenalty;
                if (workDonePercent > 0)
                    worksheet.Cell(currentRow, workDonePercent).Value = item.WorkDonePercent;
                if (workDeliveryPercent > 0)
                    worksheet.Cell(currentRow, workDeliveryPercent).Value = item.WorkDeliveryPercent;
                if (workCompletionPercent > 0)
                    worksheet.Cell(currentRow, workCompletionPercent).Value = item.WorkCompletionPercent;
                if (description > 0)
                    worksheet.Cell(currentRow, description).Value = item.Description;
                if (creatorId > 0)
                    worksheet.Cell(currentRow, creatorId).Value = item.CreatorId;
                if (creator > 0)
                    worksheet.Cell(currentRow, creator).Value = item.Creator;
                if (created > 0)
                    worksheet.Cell(currentRow, created).Value = item.Created;
            }
        }
        else
        {
            worksheet.Cell(currentRow, 1).Value = ContractorContractReportsExcelEnum.ContractorId.GetEnumDescription();
            worksheet.Cell(currentRow, 2).Value = ContractorContractReportsExcelEnum.Contractor.GetEnumDescription();
            worksheet.Cell(currentRow, 3).Value = ContractorContractReportsExcelEnum.Id.GetEnumDescription();
            worksheet.Cell(currentRow, 4).Value =
                ContractorContractReportsExcelEnum.ContractorContractTypeId.GetEnumDescription();
            worksheet.Cell(currentRow, 5).Value =
                ContractorContractReportsExcelEnum.ContractorContractTypeName.GetEnumDescription();
            worksheet.Cell(currentRow, 6).Value =
                ContractorContractReportsExcelEnum.ContractorContractTypeCode.GetEnumDescription();
            worksheet.Cell(currentRow, 7).Value =
                ContractorContractReportsExcelEnum.StatusDescription.GetEnumDescription();
            worksheet.Cell(currentRow, 8).Value = ContractorContractReportsExcelEnum.CurrencyId.GetEnumDescription();
            worksheet.Cell(currentRow, 9).Value = ContractorContractReportsExcelEnum.Currency.GetEnumDescription();
            worksheet.Cell(currentRow, 10).Value = ContractorContractReportsExcelEnum.StartDate.GetEnumDescription();
            worksheet.Cell(currentRow, 11).Value = ContractorContractReportsExcelEnum.EndDate.GetEnumDescription();
            worksheet.Cell(currentRow, 12).Value = ContractorContractReportsExcelEnum.TotalAmount.GetEnumDescription();
            worksheet.Cell(currentRow, 13).Value =
                ContractorContractReportsExcelEnum.PercentageDoingJobWell.GetEnumDescription();
            worksheet.Cell(currentRow, 14).Value =
                ContractorContractReportsExcelEnum.DoingJobWellAmount.GetEnumDescription();
            worksheet.Cell(currentRow, 15).Value =
                ContractorContractReportsExcelEnum.PercentageAdvancePayment.GetEnumDescription();
            worksheet.Cell(currentRow, 16).Value =
                ContractorContractReportsExcelEnum.AdvancePaymentAmount.GetEnumDescription();
            worksheet.Cell(currentRow, 17).Value =
                ContractorContractReportsExcelEnum.DailyLatenessPenalty.GetEnumDescription();
            worksheet.Cell(currentRow, 18).Value =
                ContractorContractReportsExcelEnum.WorkDonePercent.GetEnumDescription();
            worksheet.Cell(currentRow, 19).Value =
                ContractorContractReportsExcelEnum.WorkDeliveryPercent.GetEnumDescription();
            worksheet.Cell(currentRow, 20).Value =
                ContractorContractReportsExcelEnum.WorkCompletionPercent.GetEnumDescription();
            worksheet.Cell(currentRow, 21).Value = ContractorContractReportsExcelEnum.Description.GetEnumDescription();
            worksheet.Cell(currentRow, 22).Value = ContractorContractReportsExcelEnum.CreatorId.GetEnumDescription();
            worksheet.Cell(currentRow, 23).Value = ContractorContractReportsExcelEnum.Creator.GetEnumDescription();
            worksheet.Cell(currentRow, 24).Value = ContractorContractReportsExcelEnum.Created.GetEnumDescription();

            foreach (var item in result)
            {
                currentRow++;
                worksheet.Cell(currentRow, 1).Value = item.ContractorId;
                worksheet.Cell(currentRow, 2).Value = item.Contractor;
                worksheet.Cell(currentRow, 3).Value = item.Id;
                worksheet.Cell(currentRow, 4).Value = item.ContractorContractTypeId;
                worksheet.Cell(currentRow, 5).Value = item.ContractorContractTypeName;
                worksheet.Cell(currentRow, 6).Value = item.ContractorContractTypeCode;
                worksheet.Cell(currentRow, 7).Value = item.StatusDescription;
                worksheet.Cell(currentRow, 8).Value = item.CurrencyId;
                worksheet.Cell(currentRow, 9).Value = item.Currency;
                worksheet.Cell(currentRow, 10).Value = item.StartDate;
                worksheet.Cell(currentRow, 11).Value = item.EndDate;
                worksheet.Cell(currentRow, 12).Value = item.TotalAmount;
                worksheet.Cell(currentRow, 13).Value = item.PercentageDoingJobWell;
                worksheet.Cell(currentRow, 14).Value = item.DoingJobWellAmount;
                worksheet.Cell(currentRow, 15).Value = item.PercentageAdvancePayment;
                worksheet.Cell(currentRow, 16).Value = item.AdvancePaymentAmount;
                worksheet.Cell(currentRow, 17).Value = item.DailyLatenessPenalty;
                worksheet.Cell(currentRow, 18).Value = item.WorkDonePercent;
                worksheet.Cell(currentRow, 19).Value = item.WorkDeliveryPercent;
                worksheet.Cell(currentRow, 20).Value = item.WorkCompletionPercent;
                worksheet.Cell(currentRow, 21).Value = item.Description;
                worksheet.Cell(currentRow, 22).Value = item.CreatorId;
                worksheet.Cell(currentRow, 23).Value = item.Creator;
                worksheet.Cell(currentRow, 24).Value = item.Created;
            }
        }

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        var content = stream.ToArray();
        return content;
    }

    public static byte[] ContractorContractDetailReportsToExcel(
        ICollection<GetsContractorContractDetailReportsExcelExporterModel> result,
        List<ContractorContractDetailReportsExcelEnum>? excelFilters)
    {
        using var workbook = new XLWorkbook();

        var worksheet = workbook.Worksheets.Add("گزارش جزییات قرارداد پیمانکاری");
        var currentRow = 1;

        if (excelFilters != null && excelFilters.Count > 0)
        {
            var counter = 0;
            var contractorContractId = 0;
            var id = 0;
            var workLoad = 0;
            var startDate = 0;
            var endDate = 0;
            var unitAmount = 0;
            var totalAmount = 0;
            var projectOperationId = 0;
            var operationInfoId = 0;
            var operationInfoName = 0;
            var operationInfoCode = 0;
            var projectId = 0;
            var projectName = 0;
            var projectCode = 0;
            var costCenterId = 0;
            var costCenterName = 0;
            var costCenterCode = 0;
            var serviceInfoId = 0;
            var serviceInfoName = 0;
            var serviceInfoCode = 0;
            var serviceInfoUnitOfMeasurementId = 0;
            var serviceInfoUnitOfMeasurement = 0;
            var projectOperationDetailId = 0;
            var operationLocationId = 0;
            var privateName = 0;
            var privateCode = 0;
            var publicName = 0;
            var publicCode = 0;
            var statusDescription = 0;
            var creatorId = 0;
            var creator = 0;
            var created = 0;

            foreach (var item in excelFilters)
            {
                counter++;
                switch (item)
                {
                    case ContractorContractDetailReportsExcelEnum.ContractorContractId:
                        worksheet.Cell(currentRow, counter).Value = ContractorContractDetailReportsExcelEnum
                            .ContractorContractId.GetEnumDescription();
                        contractorContractId = counter;
                        break;
                    case ContractorContractDetailReportsExcelEnum.Id:
                        worksheet.Cell(currentRow, counter).Value =
                            ContractorContractDetailReportsExcelEnum.Id.GetEnumDescription();
                        id = counter;
                        break;
                    case ContractorContractDetailReportsExcelEnum.WorkLoad:
                        worksheet.Cell(currentRow, counter).Value =
                            ContractorContractDetailReportsExcelEnum.WorkLoad.GetEnumDescription();
                        workLoad = counter;
                        break;
                    case ContractorContractDetailReportsExcelEnum.StartDate:
                        worksheet.Cell(currentRow, counter).Value =
                            ContractorContractDetailReportsExcelEnum.StartDate.GetEnumDescription();
                        startDate = counter;
                        break;
                    case ContractorContractDetailReportsExcelEnum.EndDate:
                        worksheet.Cell(currentRow, counter).Value =
                            ContractorContractDetailReportsExcelEnum.EndDate.GetEnumDescription();
                        endDate = counter;
                        break;
                    case ContractorContractDetailReportsExcelEnum.UnitAmount:
                        worksheet.Cell(currentRow, counter).Value =
                            ContractorContractDetailReportsExcelEnum.UnitAmount.GetEnumDescription();
                        unitAmount = counter;
                        break;
                    case ContractorContractDetailReportsExcelEnum.TotalAmount:
                        worksheet.Cell(currentRow, counter).Value =
                            ContractorContractDetailReportsExcelEnum.TotalAmount.GetEnumDescription();
                        totalAmount = counter;
                        break;
                    case ContractorContractDetailReportsExcelEnum.ProjectOperationId:
                        worksheet.Cell(currentRow, counter).Value = ContractorContractDetailReportsExcelEnum
                            .ProjectOperationId.GetEnumDescription();
                        projectOperationId = counter;
                        break;
                    case ContractorContractDetailReportsExcelEnum.OperationInfoId:
                        worksheet.Cell(currentRow, counter).Value = ContractorContractDetailReportsExcelEnum
                            .OperationInfoId.GetEnumDescription();
                        operationInfoId = counter;
                        break;
                    case ContractorContractDetailReportsExcelEnum.OperationInfoName:
                        worksheet.Cell(currentRow, counter).Value = ContractorContractDetailReportsExcelEnum
                            .OperationInfoName.GetEnumDescription();
                        operationInfoName = counter;
                        break;
                    case ContractorContractDetailReportsExcelEnum.OperationInfoCode:
                        worksheet.Cell(currentRow, counter).Value = ContractorContractDetailReportsExcelEnum
                            .OperationInfoCode.GetEnumDescription();
                        operationInfoCode = counter;
                        break;
                    case ContractorContractDetailReportsExcelEnum.ProjectId:
                        worksheet.Cell(currentRow, counter).Value =
                            ContractorContractDetailReportsExcelEnum.ProjectId.GetEnumDescription();
                        projectId = counter;
                        break;
                    case ContractorContractDetailReportsExcelEnum.ProjectName:
                        worksheet.Cell(currentRow, counter).Value =
                            ContractorContractDetailReportsExcelEnum.ProjectName.GetEnumDescription();
                        projectName = counter;
                        break;
                    case ContractorContractDetailReportsExcelEnum.ProjectCode:
                        worksheet.Cell(currentRow, counter).Value =
                            ContractorContractDetailReportsExcelEnum.ProjectCode.GetEnumDescription();
                        projectCode = counter;
                        break;
                    case ContractorContractDetailReportsExcelEnum.CostCenterId:
                        worksheet.Cell(currentRow, counter).Value =
                            ContractorContractDetailReportsExcelEnum.CostCenterId.GetEnumDescription();
                        costCenterId = counter;
                        break;
                    case ContractorContractDetailReportsExcelEnum.CostCenterName:
                        worksheet.Cell(currentRow, counter).Value = ContractorContractDetailReportsExcelEnum
                            .CostCenterName.GetEnumDescription();
                        costCenterName = counter;
                        break;
                    case ContractorContractDetailReportsExcelEnum.CostCenterCode:
                        worksheet.Cell(currentRow, counter).Value = ContractorContractDetailReportsExcelEnum
                            .CostCenterCode.GetEnumDescription();
                        costCenterCode = counter;
                        break;
                    case ContractorContractDetailReportsExcelEnum.ServiceInfoId:
                        worksheet.Cell(currentRow, counter).Value =
                            ContractorContractDetailReportsExcelEnum.ServiceInfoId.GetEnumDescription();
                        serviceInfoId = counter;
                        break;
                    case ContractorContractDetailReportsExcelEnum.ServiceInfoName:
                        worksheet.Cell(currentRow, counter).Value = ContractorContractDetailReportsExcelEnum
                            .ServiceInfoName.GetEnumDescription();
                        serviceInfoName = counter;
                        break;
                    case ContractorContractDetailReportsExcelEnum.ServiceInfoCode:
                        worksheet.Cell(currentRow, counter).Value = ContractorContractDetailReportsExcelEnum
                            .ServiceInfoCode.GetEnumDescription();
                        serviceInfoCode = counter;
                        break;
                    case ContractorContractDetailReportsExcelEnum.ServiceInfoUnitOfMeasurementId:
                        worksheet.Cell(currentRow, counter).Value = ContractorContractDetailReportsExcelEnum
                            .ServiceInfoUnitOfMeasurementId.GetEnumDescription();
                        serviceInfoUnitOfMeasurementId = counter;
                        break;
                    case ContractorContractDetailReportsExcelEnum.ServiceInfoUnitOfMeasurement:
                        worksheet.Cell(currentRow, counter).Value = ContractorContractDetailReportsExcelEnum
                            .ServiceInfoUnitOfMeasurement.GetEnumDescription();
                        serviceInfoUnitOfMeasurement = counter;
                        break;
                    case ContractorContractDetailReportsExcelEnum.ProjectOperationDetailId:
                        worksheet.Cell(currentRow, counter).Value = ContractorContractDetailReportsExcelEnum
                            .ProjectOperationDetailId.GetEnumDescription();
                        projectOperationDetailId = counter;
                        break;
                    case ContractorContractDetailReportsExcelEnum.OperationLocationId:
                        worksheet.Cell(currentRow, counter).Value = ContractorContractDetailReportsExcelEnum
                            .OperationLocationId.GetEnumDescription();
                        operationLocationId = counter;
                        break;
                    case ContractorContractDetailReportsExcelEnum.PrivateName:
                        worksheet.Cell(currentRow, counter).Value =
                            ContractorContractDetailReportsExcelEnum.PrivateName.GetEnumDescription();
                        privateName = counter;
                        break;
                    case ContractorContractDetailReportsExcelEnum.PrivateCode:
                        worksheet.Cell(currentRow, counter).Value =
                            ContractorContractDetailReportsExcelEnum.PrivateCode.GetEnumDescription();
                        privateCode = counter;
                        break;
                    case ContractorContractDetailReportsExcelEnum.PublicName:
                        worksheet.Cell(currentRow, counter).Value =
                            ContractorContractDetailReportsExcelEnum.PublicName.GetEnumDescription();
                        publicName = counter;
                        break;
                    case ContractorContractDetailReportsExcelEnum.PublicCode:
                        worksheet.Cell(currentRow, counter).Value =
                            ContractorContractDetailReportsExcelEnum.PublicCode.GetEnumDescription();
                        publicCode = counter;
                        break;
                    case ContractorContractDetailReportsExcelEnum.StatusDescription:
                        worksheet.Cell(currentRow, counter).Value = ContractorContractDetailReportsExcelEnum
                            .StatusDescription.GetEnumDescription();
                        statusDescription = counter;
                        break;
                    case ContractorContractDetailReportsExcelEnum.CreatorId:
                        worksheet.Cell(currentRow, counter).Value =
                            ContractorContractDetailReportsExcelEnum.CreatorId.GetEnumDescription();
                        creatorId = counter;
                        break;
                    case ContractorContractDetailReportsExcelEnum.Creator:
                        worksheet.Cell(currentRow, counter).Value =
                            ContractorContractDetailReportsExcelEnum.Creator.GetEnumDescription();
                        creator = counter;
                        break;
                    case ContractorContractDetailReportsExcelEnum.Created:
                        worksheet.Cell(currentRow, counter).Value =
                            ContractorContractDetailReportsExcelEnum.Created.GetEnumDescription();
                        created = counter;
                        break;
                }
            }

            foreach (var item in result)
            {
                currentRow++;
                if (contractorContractId > 0)
                    worksheet.Cell(currentRow, contractorContractId).Value = item.ContractorContractId;
                if (id > 0)
                    worksheet.Cell(currentRow, id).Value = item.Id;
                if (workLoad > 0)
                    worksheet.Cell(currentRow, workLoad).Value = item.WorkLoad;
                if (startDate > 0)
                    worksheet.Cell(currentRow, startDate).Value = item.StartDate;
                if (endDate > 0)
                    worksheet.Cell(currentRow, endDate).Value = item.EndDate;
                if (unitAmount > 0)
                    worksheet.Cell(currentRow, unitAmount).Value = item.UnitAmount;
                if (totalAmount > 0)
                    worksheet.Cell(currentRow, totalAmount).Value = item.TotalAmount;
                if (projectOperationId > 0)
                    worksheet.Cell(currentRow, projectOperationId).Value = item.ProjectOperationId;
                if (operationInfoId > 0)
                    worksheet.Cell(currentRow, operationInfoId).Value = item.OperationInfoId;
                if (operationInfoName > 0)
                    worksheet.Cell(currentRow, operationInfoName).Value = item.OperationInfoName;
                if (operationInfoCode > 0)
                    worksheet.Cell(currentRow, operationInfoCode).Value = item.OperationInfoCode;
                if (projectId > 0)
                    worksheet.Cell(currentRow, projectId).Value = item.ProjectId;
                if (projectName > 0)
                    worksheet.Cell(currentRow, projectName).Value = item.ProjectName;
                if (projectCode > 0)
                    worksheet.Cell(currentRow, projectCode).Value = item.ProjectCode;
                if (costCenterId > 0)
                    worksheet.Cell(currentRow, costCenterId).Value = item.CostCenterId;
                if (costCenterName > 0)
                    worksheet.Cell(currentRow, costCenterName).Value = item.CostCenterName;
                if (costCenterCode > 0)
                    worksheet.Cell(currentRow, costCenterCode).Value = item.CostCenterCode;
                if (serviceInfoId > 0)
                    worksheet.Cell(currentRow, serviceInfoId).Value = item.ServiceInfoId;
                if (serviceInfoName > 0)
                    worksheet.Cell(currentRow, serviceInfoName).Value = item.ServiceInfoName;
                if (serviceInfoCode > 0)
                    worksheet.Cell(currentRow, serviceInfoCode).Value = item.ServiceInfoCode;
                if (serviceInfoUnitOfMeasurementId > 0)
                    worksheet.Cell(currentRow, serviceInfoUnitOfMeasurementId).Value =
                        item.ServiceInfoUnitOfMeasurementId;
                if (serviceInfoUnitOfMeasurement > 0)
                    worksheet.Cell(currentRow, serviceInfoUnitOfMeasurement).Value = item.ServiceInfoUnitOfMeasurement;
                if (projectOperationDetailId > 0)
                    worksheet.Cell(currentRow, projectOperationDetailId).Value = item.ProjectOperationDetailId;
                if (operationLocationId > 0)
                    worksheet.Cell(currentRow, operationLocationId).Value = item.OperationLocationId;
                if (privateName > 0)
                    worksheet.Cell(currentRow, privateName).Value = item.PrivateName;
                if (privateCode > 0)
                    worksheet.Cell(currentRow, privateCode).Value = item.PrivateCode;
                if (publicName > 0)
                    worksheet.Cell(currentRow, publicName).Value = item.PublicName;
                if (publicCode > 0)
                    worksheet.Cell(currentRow, publicCode).Value = item.PublicCode;
                if (statusDescription > 0)
                    worksheet.Cell(currentRow, statusDescription).Value = item.StatusDescription;
                if (creatorId > 0)
                    worksheet.Cell(currentRow, creatorId).Value = item.CreatorId;
                if (creator > 0)
                    worksheet.Cell(currentRow, creator).Value = item.Creator;
                if (created > 0)
                    worksheet.Cell(currentRow, created).Value = item.Created;
            }
        }
        else
        {
            worksheet.Cell(currentRow, 1).Value =
                ContractorContractDetailReportsExcelEnum.ContractorContractId.GetEnumDescription();
            worksheet.Cell(currentRow, 2).Value = ContractorContractDetailReportsExcelEnum.Id.GetEnumDescription();
            worksheet.Cell(currentRow, 3).Value =
                ContractorContractDetailReportsExcelEnum.WorkLoad.GetEnumDescription();
            worksheet.Cell(currentRow, 4).Value =
                ContractorContractDetailReportsExcelEnum.StartDate.GetEnumDescription();
            worksheet.Cell(currentRow, 5).Value = ContractorContractDetailReportsExcelEnum.EndDate.GetEnumDescription();
            worksheet.Cell(currentRow, 6).Value =
                ContractorContractDetailReportsExcelEnum.UnitAmount.GetEnumDescription();
            worksheet.Cell(currentRow, 7).Value =
                ContractorContractDetailReportsExcelEnum.TotalAmount.GetEnumDescription();
            worksheet.Cell(currentRow, 8).Value =
                ContractorContractDetailReportsExcelEnum.ProjectOperationId.GetEnumDescription();
            worksheet.Cell(currentRow, 9).Value =
                ContractorContractDetailReportsExcelEnum.OperationInfoId.GetEnumDescription();
            worksheet.Cell(currentRow, 10).Value =
                ContractorContractDetailReportsExcelEnum.OperationInfoName.GetEnumDescription();
            worksheet.Cell(currentRow, 11).Value =
                ContractorContractDetailReportsExcelEnum.OperationInfoCode.GetEnumDescription();
            worksheet.Cell(currentRow, 12).Value =
                ContractorContractDetailReportsExcelEnum.ProjectId.GetEnumDescription();
            worksheet.Cell(currentRow, 13).Value =
                ContractorContractDetailReportsExcelEnum.ProjectName.GetEnumDescription();
            worksheet.Cell(currentRow, 14).Value =
                ContractorContractDetailReportsExcelEnum.ProjectCode.GetEnumDescription();
            worksheet.Cell(currentRow, 15).Value =
                ContractorContractDetailReportsExcelEnum.CostCenterId.GetEnumDescription();
            worksheet.Cell(currentRow, 16).Value =
                ContractorContractDetailReportsExcelEnum.CostCenterName.GetEnumDescription();
            worksheet.Cell(currentRow, 17).Value =
                ContractorContractDetailReportsExcelEnum.CostCenterCode.GetEnumDescription();
            worksheet.Cell(currentRow, 18).Value =
                ContractorContractDetailReportsExcelEnum.ServiceInfoId.GetEnumDescription();
            worksheet.Cell(currentRow, 19).Value =
                ContractorContractDetailReportsExcelEnum.ServiceInfoName.GetEnumDescription();
            worksheet.Cell(currentRow, 20).Value =
                ContractorContractDetailReportsExcelEnum.ServiceInfoCode.GetEnumDescription();
            worksheet.Cell(currentRow, 21).Value = ContractorContractDetailReportsExcelEnum
                .ServiceInfoUnitOfMeasurementId.GetEnumDescription();
            worksheet.Cell(currentRow, 22).Value = ContractorContractDetailReportsExcelEnum.ServiceInfoUnitOfMeasurement
                .GetEnumDescription();
            worksheet.Cell(currentRow, 23).Value =
                ContractorContractDetailReportsExcelEnum.ProjectOperationDetailId.GetEnumDescription();
            worksheet.Cell(currentRow, 24).Value =
                ContractorContractDetailReportsExcelEnum.OperationLocationId.GetEnumDescription();
            worksheet.Cell(currentRow, 25).Value =
                ContractorContractDetailReportsExcelEnum.PrivateName.GetEnumDescription();
            worksheet.Cell(currentRow, 26).Value =
                ContractorContractDetailReportsExcelEnum.PrivateCode.GetEnumDescription();
            worksheet.Cell(currentRow, 27).Value =
                ContractorContractDetailReportsExcelEnum.PublicName.GetEnumDescription();
            worksheet.Cell(currentRow, 28).Value =
                ContractorContractDetailReportsExcelEnum.PublicCode.GetEnumDescription();
            worksheet.Cell(currentRow, 29).Value =
                ContractorContractDetailReportsExcelEnum.StatusDescription.GetEnumDescription();
            worksheet.Cell(currentRow, 30).Value =
                ContractorContractDetailReportsExcelEnum.CreatorId.GetEnumDescription();
            worksheet.Cell(currentRow, 31).Value =
                ContractorContractDetailReportsExcelEnum.Creator.GetEnumDescription();
            worksheet.Cell(currentRow, 32).Value =
                ContractorContractDetailReportsExcelEnum.Created.GetEnumDescription();

            foreach (var item in result)
            {
                currentRow++;
                worksheet.Cell(currentRow, 1).Value = item.ContractorContractId;
                worksheet.Cell(currentRow, 2).Value = item.Id;
                worksheet.Cell(currentRow, 3).Value = item.WorkLoad;
                worksheet.Cell(currentRow, 4).Value = item.StartDate;
                worksheet.Cell(currentRow, 5).Value = item.EndDate;
                worksheet.Cell(currentRow, 6).Value = item.UnitAmount;
                worksheet.Cell(currentRow, 7).Value = item.TotalAmount;
                worksheet.Cell(currentRow, 8).Value = item.ProjectOperationId;
                worksheet.Cell(currentRow, 9).Value = item.OperationInfoId;
                worksheet.Cell(currentRow, 10).Value = item.OperationInfoName;
                worksheet.Cell(currentRow, 11).Value = item.OperationInfoCode;
                worksheet.Cell(currentRow, 12).Value = item.ProjectId;
                worksheet.Cell(currentRow, 13).Value = item.ProjectName;
                worksheet.Cell(currentRow, 14).Value = item.ProjectCode;
                worksheet.Cell(currentRow, 15).Value = item.CostCenterId;
                worksheet.Cell(currentRow, 16).Value = item.CostCenterName;
                worksheet.Cell(currentRow, 17).Value = item.CostCenterCode;
                worksheet.Cell(currentRow, 18).Value = item.ServiceInfoId;
                worksheet.Cell(currentRow, 19).Value = item.ServiceInfoName;
                worksheet.Cell(currentRow, 20).Value = item.ServiceInfoCode;
                worksheet.Cell(currentRow, 21).Value = item.ServiceInfoUnitOfMeasurementId;
                worksheet.Cell(currentRow, 22).Value = item.ServiceInfoUnitOfMeasurement;
                worksheet.Cell(currentRow, 23).Value = item.ProjectOperationDetailId;
                worksheet.Cell(currentRow, 24).Value = item.OperationLocationId;
                worksheet.Cell(currentRow, 25).Value = item.PrivateName;
                worksheet.Cell(currentRow, 26).Value = item.PrivateCode;
                worksheet.Cell(currentRow, 27).Value = item.PublicName;
                worksheet.Cell(currentRow, 28).Value = item.PublicCode;
                worksheet.Cell(currentRow, 29).Value = item.StatusDescription;
                worksheet.Cell(currentRow, 30).Value = item.CreatorId;
                worksheet.Cell(currentRow, 31).Value = item.Creator;
                worksheet.Cell(currentRow, 32).Value = item.Created;
            }
        }

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        var content = stream.ToArray();
        return content;
    }

    public static byte[] ContractorContractServiceReportsToExcel(
        GetsContractorContractServiceReportResponse? result,
        List<ContractorContractReportExcelEnum>? excelFilters)
    {
        using var workbook = new ExcelPackage();
        var worksheet = ExcelHelpers.CreateWorksheet(workbook, "گزارش خدمات دارای قرارداد پیمانکاری");

        var dataExcel = result!.Data.ToList();
        var totalExcel = result!.OtherData;

        List<ContractorContractReportExcelEnum>? newFilters = [];
        if (excelFilters is null || excelFilters.Count <= 0)
        {
            newFilters = Enum.GetValues(typeof(ContractorContractReportExcelEnum))
                .Cast<ContractorContractReportExcelEnum>()
                .Select(x => x)
                .ToList();
        }
        else
        {
            newFilters = excelFilters;
        }

        var defaultHeaders = ExcelHelpers.GetDefaultHeaders<ContractorContractReportExcelEnum>();
        var columns = ExcelHelpers.SetupHeaders<ContractorContractReportExcelEnum>(
            worksheet,
            newFilters,
            defaultHeaders);

        var currentRow = 1;

        foreach (var item in dataExcel)
        {
            currentRow++;
            ExcelHelpers.FillRow<ContractorContractReportExcelEnum, GetsContractorContractServiceReportModel>(
                worksheet,
                item,
                currentRow,
                columns);
        }

        ExcelHelpers.ApplyOtherDataSummaryStyles(
            worksheet,
            totalExcel!.TotalServiceVolume,
            totalExcel!.Price,
            totalExcel!.TotalPrice,
            EnumHelpers.GetEnumCount<ContractorContractReportExcelEnum>());

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }




    public static byte[] GetSuggestedServicePriceToExcel(
        List<GetSuggestedServicePriceModel>? data,
        List<SuggestedServicePriceEnum>? excelFilters)
    {
        using var workbook = new ExcelPackage();
        var worksheet = ExcelHelpers.CreateWorksheet(workbook, "گزارش قیمت های پیشنهادی");

        var dataExcel = data ?? new List<GetSuggestedServicePriceModel>();

        // Filters
        var newFilters = excelFilters != null && excelFilters.Count > 0
            ? excelFilters
            : Enum.GetValues(typeof(SuggestedServicePriceEnum))
                .Cast<SuggestedServicePriceEnum>()
                .ToList();

        var defaultHeaders = ExcelHelpers.GetDefaultHeaders<SuggestedServicePriceEnum>();
        var columns =
            ExcelHelpers.SetupHeaders<SuggestedServicePriceEnum>(worksheet, newFilters, defaultHeaders);

        var currentRow = 1;
        foreach (var item in dataExcel)
        {
            currentRow++;
            ExcelHelpers.FillRow<SuggestedServicePriceEnum, GetSuggestedServicePriceModel>(
                worksheet,
                item,
                currentRow,
                columns);
        }

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }

    public static byte[] GetDraftedFixCCsToExcel(
        List<GetDraftedFixCCsModel>? data,
        List<GetDraftedFixCCsEnum>? fixCCsFilters = null,
        List<GetDraftedFixDailiesEnum>? dailyFilters = null)
    {
        using var workbook = new ExcelPackage();
        var dataList = data ?? new List<GetDraftedFixCCsModel>();

        {
            var worksheet = ExcelHelpers.CreateWorksheet(workbook, "قراردادهای پیمانکار (FixCCs)");

            var newFilters = fixCCsFilters != null && fixCCsFilters.Count > 0
                ? fixCCsFilters
                : Enum.GetValues(typeof(GetDraftedFixCCsEnum)).Cast<GetDraftedFixCCsEnum>().ToList();

            var defaultHeaders = ExcelHelpers.GetDefaultHeaders<GetDraftedFixCCsEnum>();
            var columns = ExcelHelpers.SetupHeaders<GetDraftedFixCCsEnum>(worksheet, newFilters, defaultHeaders);

            var currentRow = 1;
            foreach (var item in dataList)
            {
                currentRow++;
                ExcelHelpers.FillRow<GetDraftedFixCCsEnum, GetDraftedFixCCsModel>(
                    worksheet, item, currentRow, columns);
            }
        }

        {
            var worksheet = ExcelHelpers.CreateWorksheet(workbook, "خدمات روزانه (FixCCs)");

            var newFilters = dailyFilters != null && dailyFilters.Count > 0
                ? dailyFilters
                : Enum.GetValues(typeof(GetDraftedFixDailiesEnum)).Cast<GetDraftedFixDailiesEnum>().ToList();

            var defaultHeaders = ExcelHelpers.GetDefaultHeaders<GetDraftedFixDailiesEnum>();
            var columns = ExcelHelpers.SetupHeaders<GetDraftedFixDailiesEnum>(worksheet, newFilters, defaultHeaders);

            var currentRow = 1;
            foreach (var parent in dataList)
            {
                if (parent.DailyServices == null || parent.DailyServices.Count == 0)
                    continue;

                foreach (var item in parent.DailyServices)
                {
                    currentRow++;
                    ExcelHelpers.FillRow<GetDraftedFixDailiesEnum, GetDraftedFixDailiesModel>(
                        worksheet, item, currentRow, columns);
                }
            }
        }

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }

    public static byte[] ConfirmedCCDailyServicesExporter(
        List<GetConfirmedCCDailyServicesModel>? data,
        List<GetConfirmedCCDailyServicesEnum>? excelFilters = null)
    {
        using var workbook = new ExcelPackage();
        var dataList = data ?? new List<GetConfirmedCCDailyServicesModel>();

        var worksheet = ExcelHelpers.CreateWorksheet(workbook, "قراردادهای پیمانکار (FixCCs)");

        var newFilters = excelFilters != null && excelFilters.Count > 0
            ? excelFilters
            : Enum.GetValues(typeof(GetConfirmedCCDailyServicesEnum)).Cast<GetConfirmedCCDailyServicesEnum>().ToList();

        var defaultHeaders = ExcelHelpers.GetDefaultHeaders<GetConfirmedCCDailyServicesEnum>();
        var columns = ExcelHelpers.SetupHeaders<GetConfirmedCCDailyServicesEnum>(worksheet, newFilters, defaultHeaders);

        var currentRow = 1;
        foreach (var item in dataList)
        {
            currentRow++;
            ExcelHelpers.FillRow<GetConfirmedCCDailyServicesEnum, GetConfirmedCCDailyServicesModel>(
                worksheet, item, currentRow, columns);
        }

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }

    public static byte[] GetDraftedServiceCCsToExcel(
        List<GetDraftedServiceCCsModel>? data,
        List<GetDraftedServiceCCsEnum>? serviceCCsFilters = null,
        List<GetDraftedServiceDailiesEnum>? dailyFilters = null)
    {
        using var workbook = new ExcelPackage();
        var dataList = data ?? new List<GetDraftedServiceCCsModel>();

        {
            var worksheet = ExcelHelpers.CreateWorksheet(workbook, "قراردادهای خدمات پیمانکار");

            var newFilters = serviceCCsFilters != null && serviceCCsFilters.Count > 0
                ? serviceCCsFilters
                : Enum.GetValues(typeof(GetDraftedServiceCCsEnum)).Cast<GetDraftedServiceCCsEnum>().ToList();

            var defaultHeaders = ExcelHelpers.GetDefaultHeaders<GetDraftedServiceCCsEnum>();
            var columns = ExcelHelpers.SetupHeaders<GetDraftedServiceCCsEnum>(worksheet, newFilters, defaultHeaders);

            var currentRow = 1;
            foreach (var item in dataList)
            {
                currentRow++;
                ExcelHelpers.FillRow<GetDraftedServiceCCsEnum, GetDraftedServiceCCsModel>(
                    worksheet, item, currentRow, columns);
            }
        }

        {
            var worksheet = ExcelHelpers.CreateWorksheet(workbook, "خدمات روزانه (ServiceCCs)");

            var newFilters = dailyFilters != null && dailyFilters.Count > 0
                ? dailyFilters
                : Enum.GetValues(typeof(GetDraftedServiceDailiesEnum)).Cast<GetDraftedServiceDailiesEnum>().ToList();

            var defaultHeaders = ExcelHelpers.GetDefaultHeaders<GetDraftedServiceDailiesEnum>();
            var columns = ExcelHelpers.SetupHeaders<GetDraftedServiceDailiesEnum>(worksheet, newFilters, defaultHeaders);

            var currentRow = 1;
            foreach (var parent in dataList)
            {
                if (parent.DailyServices == null || parent.DailyServices.Count == 0)
                    continue;

                foreach (var item in parent.DailyServices)
                {
                    currentRow++;
                    ExcelHelpers.FillRow<GetDraftedServiceDailiesEnum, GetDraftedServiceDailiesModel>(
                        worksheet, item, currentRow, columns);
                }
            }
        }

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }
}
