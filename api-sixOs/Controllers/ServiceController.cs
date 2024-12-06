using Microsoft.AspNetCore.Mvc;
using api_sixOs.Models;
using api_sixOs.Data;
using System.Linq;

namespace api_sixOs.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly ApiContext _context;

        public ServiceController(ApiContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // Create
        [HttpPost]
        public IActionResult Create([FromBody] List<Service> services)
        {
            if (services == null || !services.Any() || services.Any(s => string.IsNullOrEmpty(s.ServiceName) || string.IsNullOrEmpty(s.ServiceGroup) ))
            {
                return BadRequest("Invalid service data. All fdsdadsdields must be provided for each service.");
            }

            // Thêm tất cả các dịch vụ vào cơ sở dữ liệu
            _context.Service.AddRange(services);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetDetail), new { id = services.First().Id }, services);
        }


        // Read (Get all services)
        [HttpGet]
        public IActionResult Read()
        {
            var services = _context.Service.ToList();
            return Ok(services);
        }

        // Update
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Service service)
        {
            var existingService = _context.Service.FirstOrDefault(p => p.Id == id);
            if (existingService == null)
            {
                return NotFound($"Service with Id {id} not found.");
            }

            // Only update non-null values from the provided service object
            if (!string.IsNullOrEmpty(service.ServiceName)) existingService.ServiceName = service.ServiceName;
            if (!string.IsNullOrEmpty(service.ServiceGroup)) existingService.ServiceGroup = service.ServiceGroup;
            if (!string.IsNullOrEmpty(service.ServiceUnit)) existingService.ServiceUnit = service.ServiceUnit;

            _context.SaveChanges();

            return Ok(existingService);
        }

        // Delete
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var service = _context.Service.FirstOrDefault(p => p.Id == id);
            if (service == null)
            {
                return NotFound($"Service with Id {id} not found.");
            }

            _context.Service.Remove(service);
            _context.SaveChanges();

            return Ok($"Service with Id {id} deleted successfully.");
        }

        // Get Detail by id
        [HttpGet("{id}")]
        public IActionResult GetDetail(int id)
        {
            var service = _context.Service.FirstOrDefault(p => p.Id == id);
            if (service == null)
            {
                return NotFound($"Service with Id {id} not found.");
            }

            return Ok(service);
        }
    }
}
