using ApiRest.Models;
using ApiRest.Services.Customer;
using ApiRest.Services.Service;
using Microsoft.AspNetCore.Mvc;

namespace ApiRest.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServicesController : ControllerBase
{
    private readonly IServiceService _serviceService;

    public ServicesController(IServiceService serviceService)
    {
        _serviceService = serviceService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Service>>> GetServices()
    {
        var services = await _serviceService.GetServicesAsync();
        return Ok(services);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Service>> GetService(int id)
    {
        var service = await _serviceService.GetServiceByIdAsync(id);
        if (service == null)
        {
            return NotFound();
        }
        return Ok(service);
    }

    [HttpPost]
    public async Task<ActionResult<Service>> CreateService(Service service)
    {
        var createdService = await _serviceService.CreateServiceAsync(service);
        return CreatedAtAction(nameof(GetService), new { id = createdService.ServiceId }, createdService);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateService(int id, Service service)
    {
        try
        {
            await _serviceService.UpdateServiceAsync(id, service);
            return NoContent();
        }
        catch (ArgumentException)
        {
            return BadRequest();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteService(int id)
    {
        try
        {
            await _serviceService.DeleteServiceAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
