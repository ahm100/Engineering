namespace Engineering.Application.Extensions.PlateHelper;

public static class PlateNumberHelper
{
    public static T? ToPlateModel<T>(this string? plateNumber) where T : IPlateNumberModel, new()
    {
        if (string.IsNullOrWhiteSpace(plateNumber))
            return default;

        var cleaned = new string(plateNumber.Where(c => !char.IsWhiteSpace(c) && c != '-').ToArray());
        if (cleaned.Length != 8) return default;

        return new T
        {
            Part1 = cleaned.Substring(0, 2),
            Letter = cleaned.Substring(2, 1),
            Part2 = cleaned.Substring(3, 3),
            Part3 = cleaned.Substring(6, 2)
        };
    }

    public interface IPlateNumberModel
    {
        string? Part1 { get; set; }
        string? Part2 { get; set; }
        string? Part3 { get; set; }
        string? Letter { get; set; }
    }
}
