using FireFightingRobot.Domain.Interfaces;
using FireFightingRobot.Framework;
using MediatR;

namespace FireFightingRobot.Commands.Device;
public class RegisterDeviceCommand : IRequest<Result<string>>
{
    public string DeviceName { get; set; }

    public class Handler : BaseCommandHandler<RegisterDeviceCommand, Result<string>>
    {

        private readonly IDeviceRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public Handler(IDeviceRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        protected override Result<string> Handle(RegisterDeviceCommand request)
        {
            var checkDuplicate = _repository.GetByName(request.DeviceName);
            if (checkDuplicate.Failure)
                return Result.Fail<string>(checkDuplicate.Error);

            if (checkDuplicate.Value != null)
                return Result.Fail<string>("Device name already exist.");

            var device = Domain.Device.Create(request.DeviceName);

            if (device.Failure)
                return Result.Fail<string>(device.Error);

            var create = _repository.Add(device.Value);
            if (create.Failure)
                return Result.Fail<string>(create.Error);

            var commit = _unitOfWork.Commit();
            if (commit.Failure)
                return Result.Fail<string>(commit.Error);

            return Result.OK(device.Value.DeviceKey);
        }
    }
}