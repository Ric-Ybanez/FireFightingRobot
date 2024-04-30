using FireFightingRobot.Domain.Interfaces;
using FireFightingRobot.Framework;
using MediatR;

namespace FireFightingRobot.Commands.Device;
public class CreateDeviceHistoryCommand : IRequest<Result<int>>
{
    public string DeviceKey { get; set; }
    public double Temperature { get; set; }
    public double Humidity { get; set; }
    public double Smoke { get; set; }
    public string FireDetected { get; set; }

    public class Handler : BaseCommandHandler<CreateDeviceHistoryCommand, Result<int>>
    {

        private readonly IDeviceHistoryRepository _repository;
        private readonly IDeviceRepository _deviceRepo;
        private readonly IUnitOfWork _unitOfWork;
        public Handler(IDeviceHistoryRepository repository, IDeviceRepository deviceRepo, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _deviceRepo = deviceRepo;
            _unitOfWork = unitOfWork;
        }

        protected override Result<int> Handle(CreateDeviceHistoryCommand request)
        {
            var device = _deviceRepo.GetByKey(request.DeviceKey);
            if (device.Failure)
                return Result.Fail<int>(device.Error);

            var deviceHistory = Domain.DeviceHistory.Create(device.Value, request.Temperature, request.Smoke, request.Humidity, request.FireDetected);

            if (deviceHistory.Failure)
                return Result.Fail<int>(deviceHistory.Error);

            var createHistory = _repository.Add(deviceHistory.Value);
            if (createHistory.Failure)
                return Result.Fail<int>(createHistory.Error);

            var commit = _unitOfWork.Commit();
            if (commit.Failure)
                return Result.Fail<int>(commit.Error);

            return Result.OK(createHistory.Value.Value);
        }
    }
}