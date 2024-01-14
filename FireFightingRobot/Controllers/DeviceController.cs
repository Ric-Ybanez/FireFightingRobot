using FireFightingRobot.Commands.Device;
using FireFightingRobot.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using FireFightingRobot.Framework.Extensions;

namespace FireFightingRobot.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DeviceController
{
    IMediator _mediator;
    public DeviceController(IMediator mediator) 
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<IActionResult> RegisterDevice(RegisterDeviceVm vm)
    {
        var result = await _mediator.Send(new RegisterDeviceCommand 
        {
            DeviceName = vm.DeviceName
        });
        return result.ToActionResult();
    }
}
