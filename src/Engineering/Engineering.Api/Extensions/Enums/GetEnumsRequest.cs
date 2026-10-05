namespace Engineering.Api.Extensions.Enums;

public class GetEnumsRequest
{
    public List<int>? Codes { get; set; }
    public bool Anti { get; set; }
    public string? FilterData { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
}
