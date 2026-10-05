using Engineering.Application.Services.EmployerStatusStatements.Models.GetEmployerStatusStatementLetterheadExcel;

namespace Engineering.Application.Services.EmployerStatusStatements.Queries.GetEmployerStatusStatementLetterheadExcel;

public record GetEmployerStatusStatementLetterheadExcelQuery(
    long Id
    ) : IQuery<GetEmployerStatusStatementLetterheadExcelModel>;