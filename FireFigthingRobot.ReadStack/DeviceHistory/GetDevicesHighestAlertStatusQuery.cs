using AutoMapper;
using FireFightingRobot.Framework;
using MediatR;
using FireFigthingRobot.ReadStack.DeviceHistory.Dtos;
using Microsoft.EntityFrameworkCore;

namespace FireFigthingRobot.ReadStack.DeviceHistory;

public class GetDevicesHighestAlertStatusQuery : IRequest<Result<DeviceHistoryAlertDto>>
{
    public sealed class GetDevicesHighestAlertStatusQueryHandler :
        ReadStackCommandHandlerBase<GetDevicesHighestAlertStatusQuery, Result<DeviceHistoryAlertDto>>
    {
        public GetDevicesHighestAlertStatusQueryHandler(ReadContext readContext, IMapper mapper) : base(readContext, mapper)
        {
        }

        protected override Result<DeviceHistoryAlertDto> Handle(GetDevicesHighestAlertStatusQuery request) =>
            RunQuery(
                () =>
                {
                    var histories = ReadContext.DeviceHistories
                        .Include(t => t.Device)
                        .GroupBy(g => g.DeviceId)
                        .Select(histories => histories.OrderByDescending(o => o.CreatedDate).FirstOrDefault())
                        .ToList();

                   var highestAlert =  histories.OrderByDescending(o => o.HeatIndex)
                                        .ThenByDescending(t => t.Smoke)
                                        .ThenByDescending(t => t.Temperature)
                                        .ThenByDescending(t => t.Humidity)
                                        .First();

                    return new DeviceHistoryAlertDto
                    {
                        DeviceId = highestAlert.DeviceId,
                        DeviceKey = highestAlert.Device.DeviceKey,
                        AlertLevel = CalculateAlertLevel(highestAlert),
                        AsOfDate = DateTime.Now
                    };
                }
                , "Error trying to retrieve recent alert device level");

        private string CalculateAlertLevel(FireFightingRobot.DAL.Entities.DeviceHistory history) 
        {
            return "OK";
        }
    }


}