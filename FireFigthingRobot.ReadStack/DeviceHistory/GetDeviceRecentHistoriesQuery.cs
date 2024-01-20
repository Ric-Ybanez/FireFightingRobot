using AutoMapper;
using FireFightingRobot.Framework;
using MediatR;
using FireFigthingRobot.ReadStack.DeviceHistory.Dtos;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace FireFigthingRobot.ReadStack.DeviceHistory;

public class GetDeviceRecentHistoriesQuery : IRequest<Result<List<DeviceHistoryDto>>>
{
    public sealed class GetDeviceRecentHistoriesQueryHandler :
        ReadStackCommandHandlerBase<GetDeviceRecentHistoriesQuery, Result<List<DeviceHistoryDto>>>
    {
        public GetDeviceRecentHistoriesQueryHandler(ReadContext readContext, IMapper mapper) : base(readContext, mapper)
        {
        }

        protected override Result<List<DeviceHistoryDto>> Handle(GetDeviceRecentHistoriesQuery request) =>
            RunQuery(
                () => 
                {
                    var histories = ReadContext.DeviceHistories
                        .Include(t => t.Device)
                        .GroupBy(g => g.DeviceId)
                        .Select(histories => histories.OrderByDescending(o=> o.CreatedDate).FirstOrDefault())
                        .ToList();

                    return Mapper.Map<List<DeviceHistoryDto>>(histories);
                }     
                ,"Error trying to retrieve recent device histories");
    }
}