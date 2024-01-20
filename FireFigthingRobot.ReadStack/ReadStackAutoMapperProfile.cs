using AutoMapper;
using FireFigthingRobot.ReadStack.DeviceHistory.Dtos;

namespace FireFigthingRobot.ReadStack;

public class ReadStackAutoMapperProfile : Profile
{
    public ReadStackAutoMapperProfile()
    {

        CreateMap<FireFightingRobot.DAL.Entities.DeviceHistory, DeviceHistoryDto >()
            .ForMember ( destination => destination.DeviceName, source => source.MapFrom(t => t.Device.DeviceName))
            .ForMember(destination => destination.DeviceKey, source => source.MapFrom(t => t.Device.DeviceKey))
            ;

    }
}
