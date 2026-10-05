namespace Engineering.Application.Services.CostCenterVirtualGroups.Models.CreateCostCenterVirtualGroup;

public record CreateCostCenterVirtualGroupRequest(string Title,
                                                  string Link,
                                                  string Identifier,
                                                  string? Description,
                                                  bool SendToday,
                                                  TimeSpan? TodayTime,
                                                  bool SendYesterday,
                                                  TimeSpan? YesterdayTime,
                                                  long CostCenterId) : IHttpRequest;
