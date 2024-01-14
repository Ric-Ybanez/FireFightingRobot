using FireFightingRobot.Commands.Device;
using FireFightingRobot.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using FireFightingRobot.Framework.Extensions;

namespace FireFightingRobot.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DeviceHistoryController
{
    IMediator _mediator;
    public DeviceHistoryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<IActionResult> CreateDeviceHistory(AddDeviceHistoryVm vm)
    {
        var result = await _mediator.Send(new CreateDeviceHistoryCommand
        {
            DeviceKey = vm.DeviceKey,
            Humidity = vm.Humidity,
            Smoke = vm.Smoke,
            Temperature  = vm.Temperature
        });

        return result.ToActionResult();
    }
}
