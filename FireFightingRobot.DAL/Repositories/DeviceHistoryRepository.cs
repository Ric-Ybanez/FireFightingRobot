using AutoMapper;
using FireFightingRobot.Domain;
using FireFightingRobot.Domain.Interfaces;
using FireFightingRobot.Framework;

namespace FireFightingRobot.DAL.Repositories
{
    public class DeviceHistoryRepository : Repository<Entities.DeviceHistory>, IDeviceHistoryRepository
    {
        private readonly IMapper _mapper;
        public DeviceHistoryRepository(DataContext dataContext, IMapper mapper)
           : base(dataContext)
        {
            _mapper = mapper;
        }
        public Result<Lazy<int>> Add(DeviceHistory history) =>
         Try
         (
             () =>
             {
                 var entity = _mapper.Map<Entities.DeviceHistory>(history);
                 Items.Add(entity);
                 return new Lazy<int>(() => entity.DeviceHistoryId);
             },
             "Error adding the device history"
         );

        public class AutoMapperProfile : Profile
        {
            public AutoMapperProfile()
            {
                CreateMap<DeviceHistory, Entities.DeviceHistory>();

                CreateMap<Entities.DeviceHistory, DeviceHistory>();
            }
        }
    }
}
