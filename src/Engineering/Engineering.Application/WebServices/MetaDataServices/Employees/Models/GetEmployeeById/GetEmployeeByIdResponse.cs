namespace Engineering.Application.WebServices.MetaDataServices.Employees.Models.GetEmployeeById;

public class GetEmployeeByIdResponse
{
    [JsonProperty("value")]
    public GetEmployeeByIdModel? Value { get; set; }
}
