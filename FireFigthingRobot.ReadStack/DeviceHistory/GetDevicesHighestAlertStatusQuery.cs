using AutoMapper;
using FireFightingRobot.Framework;
using MediatR;
using FireFigthingRobot.ReadStack.DeviceHistory.Dtos;
using Microsoft.EntityFrameworkCore;
using Serilog;
using FireFightingRobot.Framework.Enums;

namespace FireFigthingRobot.ReadStack.DeviceHistory;

public class GetDevicesHighestAlertStatusQuery : IRequest<Result<DeviceHistoryAlertDto>>
{
    public sealed class GetDevicesHighestAlertStatusQueryHandler :
        ReadStackCommandHandlerBase<GetDevicesHighestAlertStatusQuery, Result<DeviceHistoryAlertDto>>
    {
        public GetDevicesHighestAlertStatusQueryHandler(ReadContext readContext, IMapper mapper, ILogger logger) : base(readContext, mapper, logger)
        {
        }

        protected override Result<DeviceHistoryAlertDto> Handle(GetDevicesHighestAlertStatusQuery request) =>
            RunQuery(
                () =>
                {
                    var latestHistory = ReadContext.DeviceHistories
                        .Include(t => t.Device)
                        .OrderByDescending(o => o.CreatedDate).FirstOrDefault();

                    var histories = ReadContext.DeviceHistories
                        .Include(t => t.Device)
                        .Where(t=> t.CreatedDate >= DateTime.Now.AddDays(1))
                        .GroupBy(g => g.DeviceId)
                        .Select(histories => histories.OrderByDescending(o => o.CreatedDate).FirstOrDefault())
                        .ToList();

                   var highestAlert =  histories
                                        .OrderByDescending(o=> o.AlertLevel)
                                        .FirstOrDefault() ?? latestHistory;

                    return new DeviceHistoryAlertDto
                    {
                        DeviceId = highestAlert?.DeviceId ?? 0,
                        DeviceKey = highestAlert?.Device.DeviceKey ?? string.Empty,
                        AlertLevel = highestAlert?.AlertLevel ?? AlertLevel.Ok.Value,
                        FireDetected = highestAlert?.FireDetected ?? string.Empty,
                        CreatedDate = highestAlert?.CreatedDate ?? DateTime.Now,
                        AsOfDate = DateTime.Now
                    };
                }
                , "Error trying to retrieve recent alert device level");
    }
}