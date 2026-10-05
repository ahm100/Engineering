using ClosedXML.Excel;
using Engineering.Api.Helpers.ExcelTools;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetDetailByRGSIdReport.Xslx;

namespace Engineering.Api.Controllers.RequestGoodsSupplies.Reports;

public class RGSupplyEnExcels
{
    public static byte[] GenerateRGSHeaderTable(
        GetRGSNewExporterHeaderModel? headerData,
         string? companyName,
         byte[]? logoUrl,
        List<GetRequestGoodsSupplyDetailNewExporterModel>? rGSData)
    {
        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Request Goods Supply");

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
        GetRGSNewExporterHeaderModel? headerData)
    {
        //Header
        ExcelHelpers.SetCell(worksheet, 1, 6, 1, 19, null);

        //A1 TO C3
        AddCellValue(worksheet, 1, 1, 3, 1, $"Date: {DateTime.Now}");
        AddCellValue(worksheet, 1, 3, 3, 3, $"Registration Number:");

        //G1 TO N3
        AddCellValue(worksheet, 7, 1, 14, 1, $"{companyName}");
        AddCellValue(worksheet, 7, 2, 14, 2, $"Request Goods Supply");

        //logo
        InsertLogo(worksheet, logoUrl);

        //Header Detail
        AddCellValue(worksheet, 14, 4, 16, 4, $"Requesting Organization");
        AddCellValue(worksheet, 14, 5, 16, 5, $"Unit");
        AddCellValue(worksheet, 17, 4, 18, 4, $"{headerData?.RequestingOrganization}");
        AddCellValue(worksheet, 17, 5, 18, 5, $"{headerData?.ProjectName}");


        AddCellValue(worksheet, 3, 4, 4, 4, $"Purchase Location");
        AddCellValue(worksheet, 3, 5, 4, 5, $"Purchase Reason");
        AddCellValue(worksheet, 5, 4, 6, 4, $"{headerData?.PurchaseLocation}");
        AddCellValue(worksheet, 5, 5, 6, 5, $"{headerData?.PurchaseReason}");

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
        worksheet.Range("A7:S8").Style.Font.Bold = true;
        worksheet.Range("A7:S8").Style.Font.FontSize = 10;
        worksheet.Range("A7:S8").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        worksheet.Range("A7:S8").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
        worksheet.Range("A7:S8").Style.Alignment.WrapText = true;

        worksheet.Range("J7:J8")
            .Style.Border.LeftBorder = XLBorderStyleValues.Thick;

        worksheet.Range("A8:S8")
            .Style.Border.TopBorder = XLBorderStyleValues.Thick;

        worksheet.Range("E7:E8")
            .Style.Border.LeftBorder = XLBorderStyleValues.Thick;

        worksheet.Range("G7:G8")
            .Style.Border.LeftBorder = XLBorderStyleValues.Thick;

        worksheet.Range("C7:C8")
            .Style.Border.LeftBorder = XLBorderStyleValues.Thick;

        worksheet.Range("O7:O8")
            .Style.Border.LeftBorder = XLBorderStyleValues.Thick;

        worksheet.Range("R7:R8")
            .Style.Border.LeftBorder = XLBorderStyleValues.Thick;

        worksheet.Row(7).Height = 25;
        worksheet.Row(8).Height = 30;
    }

    private static void GenerateRGSData(IXLWorksheet worksheet,
        GetRequestGoodsSupplyDetailNewExporterModel req,
        int index)
    {
        //Header
        var row = 8 + index;
        ExcelHelpers.SetCell(worksheet, row, row, 1, 19, null);

        AddCellValue(worksheet, 18, row, 19, row, $"{req.Index}");
        AddCellValue(worksheet, 15, row, 17, row, $"{req.ReferenceCode}");
        AddCellValue(worksheet, 10, row, 14, row, $"{req.Reference}");
        AddCellValue(worksheet, 7, row, 9, row, $"{req.CostCenterName}");
        AddCellValue(worksheet, 5, row, 6, row, $"{req.ProjectName}");
        AddCellValue(worksheet, 3, row, 4, row, $"{req.Count}");
        AddCellValue(worksheet, 1, row, 2, row, $"{req.Creator}");

        AddDataStyle(worksheet, index);
    }

    private static void GenerateDataHeader(IXLWorksheet worksheet)
    {
        // First header row (row 7)
        ExcelHelpers.SetCell(worksheet, 7, 8, 1, 19, null);

        // Right side
        AddCellValue(worksheet, 18, 8, 19, 8, "Row");

        // Right side
        AddCellValue(worksheet, 15, 8, 17, 8, "Technical Code");

        // Material Information
        AddCellValue(worksheet, 10, 7, 17, 7, "Basic Material Information");

        // Requirement
        AddCellValue(worksheet, 1, 7, 9, 7, "Requirements");

        // Second header row (row 8)

        // اطلاعات اولیه مواد
        AddCellValue(worksheet, 10, 8, 14, 8, "Item Name");

        // نیازمندی
        AddCellValue(worksheet, 7, 8, 9, 8, "Cost Center");
        AddCellValue(worksheet, 5, 8, 6, 8, "Unit");
        AddCellValue(worksheet, 3, 8, 4, 8, "Count");
        AddCellValue(worksheet, 1, 8, 2, 8, "Creator");

        AddDataHeaderStyle(worksheet);
    }

    private static void AddDataStyle(IXLWorksheet worksheet, int index)
    {
        worksheet.Cell(8 + index, 18).Style.Border.LeftBorder = XLBorderStyleValues.Thick;
        worksheet.Cell(8 + index, 17).Style.Border.LeftBorder = XLBorderStyleValues.Thick;
        worksheet.Cell(8 + index, 15).Style.Border.LeftBorder = XLBorderStyleValues.Thick;
        worksheet.Cell(8 + index, 10).Style.Border.LeftBorder = XLBorderStyleValues.Thick;
        worksheet.Cell(8 + index, 7).Style.Border.LeftBorder = XLBorderStyleValues.Thick;
        worksheet.Cell(8 + index, 5).Style.Border.LeftBorder = XLBorderStyleValues.Thick;
        worksheet.Cell(8 + index, 3).Style.Border.LeftBorder = XLBorderStyleValues.Thick;
        worksheet.Range($"R{8 + index}:S{8 + index}").Style.Alignment.WrapText = true;
        worksheet.Range($"M{8 + index}:O{8 + index}").Style.Alignment.WrapText = true;
        worksheet.Range($"E{8 + index}:F{8 + index}").Style.Alignment.WrapText = true;
        worksheet.Range($"C{8 + index}:D{8 + index}").Style.Alignment.WrapText = true;
        worksheet.Range($"A{8 + index}:B{8 + index}").Style.Alignment.WrapText = true;
        worksheet.Range($"A{8 + index}:S{8 + index}").Style.Font.FontSize = 8;
        worksheet.Range($"A{8 + index}:S{8 + index}").Style.Font.Bold = true;
        worksheet.Row(8 + index).Height = 30;

        worksheet.Range($"A{8 + index}:B{11 + index}").Style.Alignment.WrapText = true;

        worksheet.Range($"K{8 + index}:L{8 + index}").Style.Alignment.WrapText = true;
        worksheet.Range($"I{8 + index}:J{8 + index}").Style.Alignment.WrapText = true;
        worksheet.Range($"P{8 + index}:Q{8 + index}").Style.Alignment.WrapText = true;
    }
}
