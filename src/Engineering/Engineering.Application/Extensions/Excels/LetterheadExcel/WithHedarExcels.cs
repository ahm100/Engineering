using Engineering.Application.Services.BillOfLadings.Contracts.GetsBillOfLadingExcelExporter;
using OfficeOpenXml.Style;

namespace Engineering.Application.Extensions.Excels.LetterheadExcel;

public class LetterheadExcels
{
    public static byte[] BillOfLadingLetterheadExcelToExcel(ICollection<GetsBillOfLadingExcelExporterModel> result)
    {
        using var workbook = new ExcelPackage();
        var worksheet = workbook.Workbook.Worksheets.Add("بارنامه");
        worksheet.View.RightToLeft = true;

        // تنظیم جهت متن‌ها به راست به چپ
        worksheet.Cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Justify;
        worksheet.Cells.Style.ReadingOrder = ExcelReadingOrder.RightToLeft;

        // تنظیم و استایل دهی به هدر اصلی
        SetHeaderStyle(worksheet.Cells["A1:F1"], "نمایش اطلاعات بارنامه", 25, true, System.Drawing.Color.Bisque);
        // تنظیم و استایل دهی به هدر دوم
        SetHeaderStyle(worksheet.Cells["A2:C2"], "اینجارو واسه هادی خوشگل کردم", 20, true, System.Drawing.Color.Bisque);
        // تنظیم و استایل دهی به هدر سوم
        SetHeaderStyle(worksheet.Cells["D2:F2"], "اینجارو واسه دل خودم خوشگل کردم", 20, true, System.Drawing.Color.GreenYellow);

        //// قرار دادن فرمول
        //worksheet.Cells["D2"].Formula = "SUM(A2:C2)";
        worksheet.Cells["A3"].Value = "نام بارنامه";
        worksheet.Cells["B3"].Value = "کد بارنامه";
        worksheet.Cells["C3"].Value = "وضعیت";
        worksheet.Cells["D3"].Value = "نام شرکت";
        SetHeaderStyle(worksheet.Cells["A3:D3"], "", 12, false, System.Drawing.Color.LightGray);

        var data = result.ToList();
        // افزودن داده‌ها و تنظیم استایل آن‌ها
        for (int i = 0; i < data.Count; i++)
        {
            worksheet.Cells[i + 4, 1].Value = data[i].BillOfLadingName;
            worksheet.Cells[i + 4, 2].Value = data[i].BillOfLadingCode;
            worksheet.Cells[i + 4, 3].Value = data[i].IsActive;

            // تنظیم بوردر برای داده‌ها
            using (var dataRange = worksheet.Cells[i + 4, 1, i + 4, 4])
            {
                dataRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                dataRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                dataRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                dataRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
            }
        }

        worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
        worksheet.Cells[worksheet.Dimension.Address].Style.WrapText = true;

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        var content = stream.ToArray();
        return content;
    }

    static void SetHeaderStyle(ExcelRange range, string? value, float fontSize, bool isMerge, System.Drawing.Color bgColor)
    {
        range.Merge = isMerge;
        if (!string.IsNullOrEmpty(value))
            range.Value = value;
        range.Style.Font.Bold = true;
        range.Style.Font.Size = fontSize;
        range.Style.Border.Top.Style = ExcelBorderStyle.Thick;
        range.Style.Border.Left.Style = ExcelBorderStyle.Thick;
        range.Style.Border.Right.Style = ExcelBorderStyle.Thick;
        range.Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
        range.Style.Fill.BackgroundColor.SetColor(bgColor);
    }
}
