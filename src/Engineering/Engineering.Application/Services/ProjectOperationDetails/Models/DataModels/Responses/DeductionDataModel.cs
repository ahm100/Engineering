namespace Engineering.Application.Services.ProjectOperationDetails.Models.DataModels.Responses;

public record DeductionDataModel
{
    public long? Id { get; set; }
    public decimal Length { get; set; }
    public decimal Width { get; set; }
    public decimal Height { get; set; }
    public decimal Weight { get; set; }
    public decimal Number { get; set; }
    public decimal FinalAmount => Length * Width * Height * Weight * Number;
}