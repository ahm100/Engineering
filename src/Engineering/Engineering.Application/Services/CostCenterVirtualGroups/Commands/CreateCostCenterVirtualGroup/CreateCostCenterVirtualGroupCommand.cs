using Engineering.Domain.Entities.CostCenters;

namespace Engineering.Application.Services.CostCenterVirtualGroups.Commands.CreateCostCenterVirtualGroup;

public record CreateCostCenterVirtualGroupCommand(string Title,
                                                  string Link,
                                                  string Identifier,
                                                  string? Description,
                                                  bool SendToday,
                                                  TimeSpan? TodayTime,
                                                  bool SendYesterday,
                                                  TimeSpan? YesterdayTime,
                                                  CostCenter CostCenter) : ICommand<CostCenterVirtualGroup>;
