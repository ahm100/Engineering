using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsRequestGoodsSupplyProduct.GetsRequestGoodsSupplyProductNewExcelEnum;
using Financial.Application.Extensions;

namespace Engineering.Application.Extensions.Excels.Exporters;

public class RGSupplyExcels
{
    public static byte[] GenerateRGSHeaderTable(
        GetsRequestGoodsSupplyProductNewExporterHeaderModel? headerData,
         string? companyName,
         byte[]? logoUrl,
        List<GetsRequestGoodsSupplyProductNewExporterModel>? rGSData)
    {
        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("جدول درخواست های تامین");

            GenerateHeader(worksheet, companyName, logoUrl, headerData);
            GenerateDataHeader(worksheet);

            var index = 1;
            if (rGSData != null && rGSData.Count > 0)
                foreach (var item in rGSData)
                {
                    GenerateRGSData(worksheet, item, index++);
                }

            PageSetup(worksheet);

            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                return stream.ToArray();
            }
        }
    }

    private static void GenerateHeader(IXLWorksheet worksheet,
         string? companyName,
         byte[]? logoUrl,
        GetsRequestGoodsSupplyProductNewExporterHeaderModel? headerData)
    {
        //Header
        ExcelHelpers.SetCell(worksheet, 1, 6, 1, 19, null);

        //A1 TO C3
        AddCellValue(worksheet, 1, 1, 3, 1, $"تاریخ: {DateTime.Now.ToShortPersianDateString()}");
        AddCellValue(worksheet, 1, 2, 3, 2, $"شماره: {headerData?.RGSRequestNumber}");
        AddCellValue(worksheet, 1, 3, 3, 3, $"شماره ثبت:");

        //G1 TO N3
        AddCellValue(worksheet, 7, 1, 14, 1, $"{companyName}");
        AddCellValue(worksheet, 7, 2, 14, 2, $"درخواست تامین");
        AddCellValue(worksheet, 7, 3, 14, 3, $"Supply Requests");

        //logo
        InsertLogo(worksheet, logoUrl);

        //Header Detail
        AddCellValue(worksheet, 17, 4, 18, 4, $"نام مرکز هزینه");
        AddCellValue(worksheet, 17, 5, 18, 5, $"نام پروژه");
        AddCellValue(worksheet, 15, 4, 16, 4, $"{headerData?.CostCenterName}");
        AddCellValue(worksheet, 15, 5, 16, 5, $"{headerData?.ProjectName}");


        AddCellValue(worksheet, 5, 4, 6, 4, $"درخواست کننده");
        AddCellValue(worksheet, 5, 5, 6, 5, $"تاریخ درخواست");
        AddCellValue(worksheet, 3, 4, 4, 4, $"{headerData?.Creator}");
        AddCellValue(worksheet, 3, 5, 4, 5, $"{headerData?.RequestedDateShamsi}");

        AddHeaderStyle(worksheet);
    }

    private static void InsertLogo(IXLWorksheet worksheet, byte[]? logoBytes)
    {
        if (logoBytes != null)
        {
            using var stream = new MemoryStream(logoBytes);

            worksheet.AddPicture(stream)
                .MoveTo(worksheet.Cell(1, 16), 0, 15) // 👈 این 15 یعنی margin-top
                .WithSize(110, 80);
        }
    }

    private static void AddHeaderStyle(IXLWorksheet worksheet)
    {
        worksheet.Range("G1:N1").Style.Font.FontSize = 18;
        worksheet.Range("G2:N2").Style.Font.FontSize = 16;
        worksheet.Range("G3:N3").Style.Font.FontSize = 16;
        worksheet.Range("G1:N1").Style.Font.Bold = true;
        worksheet.Range("G2:N2").Style.Font.Bold = true;
        worksheet.Range("G3:N3").Style.Font.Bold = true;
        worksheet.Range("E4:F4").Style.Font.Bold = true;
        worksheet.Range("E5:F5").Style.Font.Bold = true;
        worksheet.Range("I4:J4").Style.Font.Bold = true;
        worksheet.Range("I5:J5").Style.Font.Bold = true;
        worksheet.Range("Q4:R4").Style.Font.Bold = true;
        worksheet.Range("Q5:R5").Style.Font.Bold = true;
        worksheet.Range("Q6:R6").Style.Font.Bold = true;
        worksheet.Range("M4:N4").Style.Font.Bold = true;
        worksheet.Range("M5:N5").Style.Font.Bold = true;
        worksheet.Range($"C{6}:P{6}").Style.Alignment.WrapText = true;
        worksheet.Range($"C{6}:P{6}").Style.Font.FontSize = 10;
        worksheet.Range($"C{6}:P{6}").Style.Font.Bold = false;
        worksheet.Range($"C{6}:P{6}").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

        for (int col = 1; col <= 19; col++)
        {
            worksheet.Cell(3, col).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
        }

        for (int row = 1; row <= 7; row++)
        {
            worksheet.Row(row).Height = 30;
        }
    }

    private static void AddCellValue(
        IXLWorksheet worksheet,
        int fromCol,
        int fromRow,
        int toCol,
        int toRow,
        string value)
    {
        var range = worksheet.Range($"{ExcelHelpers.GetColRef(fromRow, fromCol)}:{ExcelHelpers.GetColRef(toRow, toCol)}");
        range.Merge().Value = value;
    }

    private static void PageSetup(IXLWorksheet worksheet)
    {
        worksheet.PageSetup.Margins.Top = 0;
        worksheet.PageSetup.Margins.Header = 0;
        worksheet.PageSetup.Margins.Footer = 0;
        worksheet.PageSetup.Margins.Left = 0;
        worksheet.PageSetup.Margins.Right = 0;
        worksheet.PageSetup.Margins.Bottom = 0;
        worksheet.PageSetup.PageOrientation = XLPageOrientation.Landscape;
        worksheet.PageSetup.FitToPages(1, 1000);
    }

    private static void AddDataHeaderStyle(IXLWorksheet worksheet)
    {
        worksheet.Cell(7, 18).Style.Border.LeftBorder = XLBorderStyleValues.Thick;
        worksheet.Cell(7, 16).Style.Border.LeftBorder = XLBorderStyleValues.Thick;
        worksheet.Cell(7, 13).Style.Border.LeftBorder = XLBorderStyleValues.Thick;
        worksheet.Cell(7, 10).Style.Border.LeftBorder = XLBorderStyleValues.Thick;
        worksheet.Cell(7, 7).Style.Border.LeftBorder = XLBorderStyleValues.Thick;
        worksheet.Cell(7, 5).Style.Border.LeftBorder = XLBorderStyleValues.Thick;
        worksheet.Cell(7, 3).Style.Border.LeftBorder = XLBorderStyleValues.Thick;
        worksheet.Cell(7, 1).Style.Border.LeftBorder = XLBorderStyleValues.Thick;

        worksheet.Range($"A{7}:T{7}").Style.Font.FontSize = 10;
        worksheet.Range($"A{7}:T{7}").Style.Font.Bold = true; ;
        worksheet.Range($"R{7}:S{7}").Style.Alignment.WrapText = true;
    }

    private static void GenerateRGSData(IXLWorksheet worksheet,
        GetsRequestGoodsSupplyProductNewExporterModel req,
        int index)
    {
        //Header
        ExcelHelpers.SetCell(worksheet, 7 + index, 7 + index, 1, 19, null);

        AddCellValue(worksheet, 18, 7 + index, 19, 7 + index, $"{req.RequestNumber}");
        AddCellValue(worksheet, 16, 7 + index, 17, 7 + index, $"{req.ProductCode}");
        AddCellValue(worksheet, 13, 7 + index, 15, 7 + index, $"{req.ProductName}");
        AddCellValue(worksheet, 10, 7 + index, 12, 7 + index, $"{req.Count}");
        AddCellValue(worksheet, 7, 7 + index, 9, 7 + index, $"{req.MeasureName}");
        AddCellValue(worksheet, 5, 7 + index, 6, 7 + index, $"{req.Creator}");
        AddCellValue(worksheet, 1, 7 + index, 4, 7 + index, $"{req.RequestedDateShamsi}");


        AddDataStyle(worksheet, index);
    }

    private static void GenerateDataHeader(IXLWorksheet worksheet)
    {
        //Header
        ExcelHelpers.SetCell(worksheet, 7, 7, 1, 19, null);

        AddCellValue(worksheet, 18, 7, 19, 7, $"شماره درخواست تامین");
        AddCellValue(worksheet, 16, 7, 17, 7, $"کد فنی کالا");
        AddCellValue(worksheet, 13, 7, 15, 7, $"عنوان کالا");
        AddCellValue(worksheet, 10, 7, 12, 7, $"تعداد");
        AddCellValue(worksheet, 7, 7, 9, 7, $"واحد");
        AddCellValue(worksheet, 5, 7, 6, 7, $"ایجاد کننده");
        AddCellValue(worksheet, 1, 7, 4, 7, $"تاریخ ایجاد درخواست");

        AddDataHeaderStyle(worksheet);
    }

    private static void AddDataStyle(IXLWorksheet worksheet, int index)
    {
        worksheet.Cell(7 + index, 18).Style.Border.LeftBorder = XLBorderStyleValues.Thick;
        worksheet.Cell(7 + index, 16).Style.Border.LeftBorder = XLBorderStyleValues.Thick;
        worksheet.Cell(7 + index, 13).Style.Border.LeftBorder = XLBorderStyleValues.Thick;
        worksheet.Cell(7 + index, 10).Style.Border.LeftBorder = XLBorderStyleValues.Thick;
        worksheet.Cell(7 + index, 7).Style.Border.LeftBorder = XLBorderStyleValues.Thick;
        worksheet.Cell(7 + index, 5).Style.Border.LeftBorder = XLBorderStyleValues.Thick;
        worksheet.Cell(7 + index, 2).Style.Border.LeftBorder = XLBorderStyleValues.Thick;
        worksheet.Range($"R{7 + index}:S{7 + index}").Style.Alignment.WrapText = true;
        worksheet.Range($"M{7 + index}:O{7 + index}").Style.Alignment.WrapText = true;
        worksheet.Range($"E{7 + index}:F{7 + index}").Style.Alignment.WrapText = true;
        worksheet.Range($"C{7 + index}:D{7 + index}").Style.Alignment.WrapText = true;
        worksheet.Range($"A{7 + index}:B{7 + index}").Style.Alignment.WrapText = true;
        worksheet.Range($"A{7 + index}:S{7 + index}").Style.Font.FontSize = 8;
        worksheet.Range($"A{7 + index}:S{7 + index}").Style.Font.Bold = true;
        worksheet.Row(7 + index).Height = 30;

        worksheet.Range($"A{7 + index}:B{11 + index}").Style.Alignment.WrapText = true;

        worksheet.Range($"K{7 + index}:L{7 + index}").Style.Alignment.WrapText = true;
        worksheet.Range($"I{7 + index}:J{7 + index}").Style.Alignment.WrapText = true;
        worksheet.Range($"P{7 + index}:Q{7 + index}").Style.Alignment.WrapText = true;
    }
}
