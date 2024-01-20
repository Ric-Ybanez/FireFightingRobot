using FireFightingRobot.Commands.Device;
using FireFightingRobot.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using FireFightingRobot.Framework.Extensions;
using FireFigthingRobot.ReadStack.DeviceHistory;

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

    [HttpGet]
    [Route("recent")]
    public async Task<IActionResult> GetRecentHistories()
    {
        var result = await _mediator.Send(new GetDeviceRecentHistoriesQuery());
        return result.ToActionResult();
    }

    [HttpGet]
    [Route("status")]
    public async Task<IActionResult> GetStatusAlert()
    {
        var result = await _mediator.Send(new GetDevicesHighestAlertStatusQuery());
        return result.ToActionResult();
    }
}
