using AutoMapper;
using AutoMapper.QueryableExtensions;
using FireFightingRobot.Domain;
using FireFightingRobot.Domain.Interfaces;
using FireFightingRobot.Framework;

namespace FireFightingRobot.DAL.Repositories
{
    public class DeviceRepository : Repository<Entities.Device>, IDeviceRepository  
    {
        private readonly IMapper _mapper;
        public DeviceRepository(DataContext dataContext, IMapper mapper)
           : base(dataContext)
        {
            _mapper = mapper;
        }
        public Result<Lazy<int>> Add(Device device) =>
         Try
         (
             () =>
             {
                 var entity = new Entities.Device
                 {
                     DeviceKey = device.DeviceKey,
                     DeviceName = device.DeviceName
                 };
                 Items.Add(entity);
                 return new Lazy<int>(() => entity.DeviceId);
             },
             "Error adding the device"
         );

        public Result<Device> GetByName(string name) =>
           RunQuery
           (
               () => Items.Where(i => i.DeviceName == name)
                           .ProjectTo<Device>(_mapper.ConfigurationProvider)
                           .SingleOrDefault(),
               "Error retrieving the device by name"
           );

        public Result<Device> GetByKey(string key) =>
         RunQuery
         (
             () => Items.Where(i => i.DeviceKey == key)
                         .ProjectTo<Device>(_mapper.ConfigurationProvider)
                         .SingleOrDefault(),
             "Error retrieving the device by key"
         );

        public class AutoMapperProfile : Profile
        {
            public AutoMapperProfile()
            {
                CreateMap<Device, Entities.Device>();

                CreateMap<Entities.Device, Device>();
            }
        }
    }
}
