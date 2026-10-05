using MathNet.Numerics;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Engineering.Api.Helpers.ExcelTools;

public class ExcelStyles
{
    public static void SetCellStyle(
        ExcelWorksheet worksheet,
        int number,
        int row,
        int startColumn,
        int endColumn)
    {
        using (var dataRange = worksheet.Cells[row, startColumn, row, endColumn])
        {
            //dataRange.AutoFitColumns();
            //dataRange.Style.WrapText = false;
            //dataRange.Style.ReadingOrder = ExcelReadingOrder.RightToLeft;
            //dataRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            //dataRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
            //dataRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            //dataRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            //dataRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
            //dataRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

            if (number.IsOdd())
            {
                //var lightBlue = System.Drawing.ColorTranslator.FromHtml("#ADD8E6");
                //dataRange.Style.Fill.BackgroundColor.SetColor(lightBlue);
            }
            else
            {
                //var white = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                //dataRange.Style.Fill.BackgroundColor.SetColor(white);
            }
        }
    }

    public static void SetHeaderStyle(
        ExcelRange? range,
        ExcelBorderStyle? borderStyle)
    {
        //range.Style.Fill.PatternType = ExcelFillStyle.Solid;
        //var lightGray = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");
        //range.Style.Fill.BackgroundColor.SetColor(lightGray);

        //range.AutoFitColumns();
        //range.Style.WrapText = false;
        //range.Style.ReadingOrder = ExcelReadingOrder.RightToLeft;
        //range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //range.Style.Font.Bold = true;
        //range.Style.Font.Size = 12;
        //range.Style.Border.Top.Style = borderStyle;
        //range.Style.Border.Left.Style = borderStyle;
        //range.Style.Border.Right.Style = borderStyle;
        //range.Style.Border.Bottom.Style = borderStyle;
        //range.Style.Fill.PatternType = ExcelFillStyle.Solid;
    }

    public static void SetSummaryCellStyle(
        ExcelRange range,
        ExcelWorksheet worksheet,
        int activeCount,
        int count,
        int column)
    {
        //range.AutoFitColumns();
        //range.Style.WrapText = false;
        //range.Style.ReadingOrder = ExcelReadingOrder.RightToLeft;
        //range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //range.Style.Font.Bold = true;
        //range.Style.Font.Size = 12;
        //range.Style.Border.Top.Style = ExcelBorderStyle.Medium;
        //range.Style.Border.Left.Style = ExcelBorderStyle.Medium;
        //range.Style.Border.Right.Style = ExcelBorderStyle.Medium;
        //range.Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
        //range.Style.Fill.PatternType = ExcelFillStyle.Solid;
        //var beige = System.Drawing.ColorTranslator.FromHtml("#F5F5DC");
        //range.Style.Fill.BackgroundColor.SetColor(beige);
        AddSummaryData(worksheet, activeCount, count, column);
    }

    public static void SetOtherDataSummaryCellStyle(
        ExcelWorksheet worksheet,
        decimal volume,
        decimal price,
        decimal totalPrice,
        int column)
    {
        AddSummaryOtherData(worksheet, volume, price, totalPrice, column);
    }

    private static void AddSummaryData(
        ExcelWorksheet worksheet,
        int activeCount,
        int count,
        int column)
    {
        worksheet.Cells[2, column + 2].Value = "تعداد فعال";
        worksheet.Cells[2, column + 3].Value = activeCount;
        worksheet.Cells[3, column + 2].Value = "تعداد غیرفعال";
        worksheet.Cells[3, column + 3].Value = count - activeCount;
        worksheet.Cells[4, column + 2].Value = "مجموع";
        worksheet.Cells[4, column + 3].Value = count;
    }

    private static void AddSummaryOtherData(
        ExcelWorksheet worksheet,
        decimal volume,
        decimal price,
        decimal totalPrice,
        int column)
    {
        worksheet.Cells[2, column + 2].Value = "کل حجم";
        worksheet.Cells[2, column + 3].Value = volume;
        worksheet.Cells[3, column + 2].Value = "کل قیمت های واحد";
        worksheet.Cells[3, column + 3].Value = price;
        worksheet.Cells[4, column + 2].Value = "مجموع قیمت کل";
        worksheet.Cells[4, column + 3].Value = totalPrice;
    }

    public static void SetSummaryCellData(
        ExcelRange range,
        ExcelWorksheet worksheet,
        int column)
    {

        //range.AutoFitColumns();
        //range.Style.WrapText = false;
        //range.Style.ReadingOrder = ExcelReadingOrder.RightToLeft;
        //range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //range.Style.Font.Bold = true;
        //range.Style.Font.Size = 12;
        //range.Style.Border.Top.Style = ExcelBorderStyle.Medium;
        //range.Style.Border.Left.Style = ExcelBorderStyle.Medium;
        //range.Style.Border.Right.Style = ExcelBorderStyle.Medium;
        //range.Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
        //range.Style.Fill.PatternType = ExcelFillStyle.Solid;
        //var beige = System.Drawing.ColorTranslator.FromHtml("#F5F5DC");
        //range.Style.Fill.BackgroundColor.SetColor(beige);
    }
}
