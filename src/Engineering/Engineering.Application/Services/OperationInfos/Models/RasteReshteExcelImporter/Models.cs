namespace Engineering.Application.Services.OperationInfos.Models.RasteReshteExcelImporter;

public class RasteReshteCategoryExcelModel
{
    public string Title { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
}

public class RasteReshteBranchExcelModel
{
    public string Title { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string CategoryCode { get; set; } = string.Empty;
}

public class RasteReshteSeasonExcelModel
{
    public string Title { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string BranchCode { get; set; } = string.Empty;
    public string CategoryCode { get; set; } = string.Empty;
}