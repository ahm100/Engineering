using Engineering.Domain.Entities.CostCenters;

namespace Engineering.Application.Services.CostCenterVirtualGroups.Commands.UpdateCostCenterVirtualGroup;

public record UpdateCostCenterVirtualGroupCommand(long CostCenterVirtualGroupId,
                                                  string Title,
                                                  string Link,
                                                  string Identifier,
                                                  string? Description,
                                                  bool SendToday,
                                                  TimeSpan? TodayTime,
                                                  bool SendYesterday,
                                                  TimeSpan? YesterdayTime) : ICommand<CostCenterVirtualGroup>;
