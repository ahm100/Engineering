namespace Engineering.Application.Services.RequestMachineryStatusStatements.Models.GetFilteredRequestMachineryStatusStatement;

public record GetFilteredRequestMachineryStatusStatementResponse(
    List<GetFilteredRequestMachineryStatusStatementModel> Data,
    int RowCount
    );
