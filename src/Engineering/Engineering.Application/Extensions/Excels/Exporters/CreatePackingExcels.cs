using Engineering.Application.Services.TelegramChats.Models.CommercialPackingTelegramMessage;
using System.ComponentModel;

namespace Engineering.Application.Extensions.Excels.Exporters;

public class CreatePackingExcels
{
    public static byte[] CreatePackingToExcel(ICollection<CreatePackingExcelExporterResponseModel> result,
        List<CreatePackingExcelEnum>? excelFilters)
    {
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("پکینگ");
        var currentRow = 1;

        if (excelFilters != null && excelFilters.Count > 0)
        {
            var counter = 0;
            var requestNumber = 0;
            var costCenterName = 0;
            var projectName = 0;
            var projectOperationName = 0;
            var requestDate = 0;
            var productName = 0;
            var productCode = 0;
            var brand = 0;
            var brandModel = 0;
            var measureUnitName = 0;
            var commercialRequestNumber = 0;
            var followupName = 0;
            var description = 0;
            var owner = 0;
            var supplierName = 0;
            var requestCount = 0;
            var quantity = 0;
            var thirdParty = 0;

            foreach (var item in excelFilters)
            {
                counter++;
                switch (item)
                {
                    case CreatePackingExcelEnum.RequestNumber:
                        worksheet.Cell(currentRow, counter).Value =
                            CreatePackingExcelEnum.RequestNumber.GetEnumDescription();
                        requestNumber = counter;
                        break;
                    case CreatePackingExcelEnum.CommercialRequestNumber:
                        worksheet.Cell(currentRow, counter).Value =
                            CreatePackingExcelEnum.CommercialRequestNumber.GetEnumDescription();
                        commercialRequestNumber = counter;
                        break;
                    case CreatePackingExcelEnum.Owner:
                        worksheet.Cell(currentRow, counter).Value =
                            CreatePackingExcelEnum.Owner.GetEnumDescription();
                        owner = counter;
                        break;

                    case CreatePackingExcelEnum.ProductName:
                        worksheet.Cell(currentRow, counter).Value =
                            CreatePackingExcelEnum.ProductName.GetEnumDescription();
                        productName = counter;
                        break;
                    case CreatePackingExcelEnum.ProductCode:
                        worksheet.Cell(currentRow, counter).Value =
                            CreatePackingExcelEnum.ProductCode.GetEnumDescription();
                        productCode = counter;
                        break;
                    case CreatePackingExcelEnum.Brand:
                        worksheet.Cell(currentRow, counter).Value =
                            CreatePackingExcelEnum.Brand.GetEnumDescription();
                        brand = counter;
                        break;
                    case CreatePackingExcelEnum.BrandModel:
                        worksheet.Cell(currentRow, counter).Value =
                            CreatePackingExcelEnum.BrandModel.GetEnumDescription();
                        brandModel = counter;
                        break;
                    case CreatePackingExcelEnum.MeasureUnitName:
                        worksheet.Cell(currentRow, counter).Value =
                            CreatePackingExcelEnum.MeasureUnitName.GetEnumDescription();
                        measureUnitName = counter;
                        break;
                    case CreatePackingExcelEnum.Quantity:
                        worksheet.Cell(currentRow, counter).Value =
                            CreatePackingExcelEnum.Quantity.GetEnumDescription();
                        quantity = counter;
                        break;
                    case CreatePackingExcelEnum.RequestCount:
                        worksheet.Cell(currentRow, counter).Value =
                            CreatePackingExcelEnum.RequestCount.GetEnumDescription();
                        requestCount = counter;
                        break;
                    case CreatePackingExcelEnum.CostCenterName:
                        worksheet.Cell(currentRow, counter).Value =
                            CreatePackingExcelEnum.CostCenterName.GetEnumDescription();
                        costCenterName = counter;
                        break;
                    case CreatePackingExcelEnum.ProjectName:
                        worksheet.Cell(currentRow, counter).Value =
                            CreatePackingExcelEnum.ProjectName.GetEnumDescription();
                        projectName = counter;
                        break;
                    case CreatePackingExcelEnum.ProjectOperationName:
                        worksheet.Cell(currentRow, counter).Value =
                            CreatePackingExcelEnum.ProjectOperationName.GetEnumDescription();
                        projectOperationName = counter;
                        break;
                    case CreatePackingExcelEnum.SupplierName:
                        worksheet.Cell(currentRow, counter).Value =
                            CreatePackingExcelEnum.SupplierName.GetEnumDescription();
                        supplierName = counter;
                        break;
                    case CreatePackingExcelEnum.ThirdParty:
                        worksheet.Cell(currentRow, counter).Value =
                            CreatePackingExcelEnum.ThirdParty.GetEnumDescription();
                        thirdParty = counter;
                        break;
                    case CreatePackingExcelEnum.FollowupName:
                        worksheet.Cell(currentRow, counter).Value =
                            CreatePackingExcelEnum.FollowupName.GetEnumDescription();
                        followupName = counter;
                        break;
                    case CreatePackingExcelEnum.Description:
                        worksheet.Cell(currentRow, counter).Value =
                            CreatePackingExcelEnum.Description.GetEnumDescription();
                        description = counter;
                        break;
                    case CreatePackingExcelEnum.RequestDate:
                        worksheet.Cell(currentRow, counter).Value =
                            CreatePackingExcelEnum.RequestDate.GetEnumDescription();
                        requestDate = counter;
                        break;
                }
            }

            foreach (var item in result)
            {
                currentRow++;
                if (requestNumber > 0)
                    worksheet.Cell(currentRow, requestNumber).Value = item.RequestNumber;
                if (commercialRequestNumber > 0)
                    worksheet.Cell(currentRow, commercialRequestNumber).Value = item.CommercialRequestNumber;
                if (owner > 0)
                    worksheet.Cell(currentRow, owner).Value = item.Owner;
                if (productName > 0)
                    worksheet.Cell(currentRow, productName).Value = item.ProductName;
                if (productCode > 0)
                    worksheet.Cell(currentRow, productCode).Value = item.ProductCode;
                if (brand > 0)
                    worksheet.Cell(currentRow, brand).Value = item.Brand;
                if (brandModel > 0)
                    worksheet.Cell(currentRow, brandModel).Value = item.BrandModel;
                if (measureUnitName > 0)
                    worksheet.Cell(currentRow, measureUnitName).Value = item.MeasureUnitName;
                if (quantity > 0)
                    worksheet.Cell(currentRow, quantity).Value = item.Quantity;
                if (requestCount > 0)
                    worksheet.Cell(currentRow, requestCount).Value = item.RequestCount;
                if (costCenterName > 0)
                    worksheet.Cell(currentRow, costCenterName).Value = item.CostCenterName;
                if (projectName > 0)
                    worksheet.Cell(currentRow, projectName).Value = item.ProjectName;
                if (projectOperationName > 0)
                    worksheet.Cell(currentRow, projectOperationName).Value = item.ProjectOperationName;
                if (supplierName > 0)
                    worksheet.Cell(currentRow, supplierName).Value = item.SupplierName;
                if (thirdParty > 0)
                    worksheet.Cell(currentRow, thirdParty).Value = item.ThirdParty;
                if (followupName > 0)
                    worksheet.Cell(currentRow, followupName).Value = item.FollowupName;
                if (description > 0)
                    worksheet.Cell(currentRow, description).Value = item.Description;
                if (requestDate > 0)
                    worksheet.Cell(currentRow, requestDate).Value = item.PersianRequestDate;
            }
        }
        else
        {

            worksheet.Cell(currentRow, 1).Value = CreatePackingExcelEnum.RequestNumber.GetEnumDescription();
            worksheet.Cell(currentRow, 2).Value = CreatePackingExcelEnum.CommercialRequestNumber.GetEnumDescription();
            worksheet.Cell(currentRow, 3).Value = CreatePackingExcelEnum.Owner.GetEnumDescription();
            worksheet.Cell(currentRow, 4).Value = CreatePackingExcelEnum.ProductName.GetEnumDescription();
            worksheet.Cell(currentRow, 5).Value = CreatePackingExcelEnum.ProductCode.GetEnumDescription();
            worksheet.Cell(currentRow, 6).Value = CreatePackingExcelEnum.Brand.GetEnumDescription();
            worksheet.Cell(currentRow, 7).Value = CreatePackingExcelEnum.BrandModel.GetEnumDescription();
            worksheet.Cell(currentRow, 8).Value = CreatePackingExcelEnum.MeasureUnitName.GetEnumDescription();
            worksheet.Cell(currentRow, 9).Value = CreatePackingExcelEnum.Quantity.GetEnumDescription();
            worksheet.Cell(currentRow, 10).Value = CreatePackingExcelEnum.RequestCount.GetEnumDescription();
            worksheet.Cell(currentRow, 11).Value = CreatePackingExcelEnum.CostCenterName.GetEnumDescription();
            worksheet.Cell(currentRow, 12).Value = CreatePackingExcelEnum.ProjectName.GetEnumDescription();
            worksheet.Cell(currentRow, 13).Value = CreatePackingExcelEnum.ProjectOperationName.GetEnumDescription();
            worksheet.Cell(currentRow, 14).Value = CreatePackingExcelEnum.SupplierName.GetEnumDescription();
            worksheet.Cell(currentRow, 15).Value = CreatePackingExcelEnum.ThirdParty.GetEnumDescription();
            worksheet.Cell(currentRow, 16).Value = CreatePackingExcelEnum.FollowupName.GetEnumDescription();
            worksheet.Cell(currentRow, 17).Value = CreatePackingExcelEnum.Description.GetEnumDescription();
            worksheet.Cell(currentRow, 18).Value = CreatePackingExcelEnum.RequestDate.GetEnumDescription();

            foreach (var item in result)
            {
                currentRow++;

                worksheet.Cell(currentRow, 1).Value = item.RequestNumber;
                worksheet.Cell(currentRow, 2).Value = item.CommercialRequestNumber;
                worksheet.Cell(currentRow, 3).Value = item.Owner;
                worksheet.Cell(currentRow, 4).Value = item.ProductName;
                worksheet.Cell(currentRow, 5).Value = item.ProductCode;
                worksheet.Cell(currentRow, 6).Value = item.Brand;
                worksheet.Cell(currentRow, 7).Value = item.BrandModel;
                worksheet.Cell(currentRow, 8).Value = item.MeasureUnitName;
                worksheet.Cell(currentRow, 9).Value = item.Quantity;
                worksheet.Cell(currentRow, 10).Value = item.RequestCount;
                worksheet.Cell(currentRow, 11).Value = item.CostCenterName;
                worksheet.Cell(currentRow, 12).Value = item.ProjectName;
                worksheet.Cell(currentRow, 13).Value = item.ProjectOperationName;
                worksheet.Cell(currentRow, 14).Value = item.SupplierName;
                worksheet.Cell(currentRow, 15).Value = item.ThirdParty;
                worksheet.Cell(currentRow, 16).Value = item.FollowupName;
                worksheet.Cell(currentRow, 17).Value = item.Description;
                worksheet.Cell(currentRow, 18).Value = item.PersianRequestDate;
            }
        }

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        var content = stream.ToArray();
        return content;
    }
}

public enum CreatePackingExcelEnum
{
    [Description("شماره درخواست")] RequestNumber = 1,
    [Description("شماره درخواست تامین")] CommercialRequestNumber = 2,
    [Description("درخواست دهنده")] Owner = 3,
    [Description("نام کالا")] ProductName = 4,
    [Description("کد کالا")] ProductCode = 5,
    [Description("برند")] Brand = 6,
    [Description("مدل")] BrandModel = 7,
    [Description("واحد")] MeasureUnitName = 8,
    [Description("تعداد مربوط به مرکز هزینه")] Quantity = 9,
    [Description("تعداد خرید شده")] RequestCount = 10,
    [Description("مرکز هزینه")] CostCenterName = 11,
    [Description("پروژه")] ProjectName = 12,
    [Description("شرح عملیات")] ProjectOperationName = 13,
    [Description("رابط شرکت")] SupplierName = 14,
    [Description("فروشگاه")] ThirdParty = 15,
    [Description("مسئول خرید")] FollowupName = 16,
    [Description("توضیحات")] Description = 17,
    [Description("تاریخ درخواست")] RequestDate = 18,
}