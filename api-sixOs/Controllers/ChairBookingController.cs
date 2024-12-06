using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api_sixOs.Models;
using api_sixOs.Data;
using System.Linq;

namespace api_sixOs.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ChairBookingController : ControllerBase
    {
        private readonly ApiContext _context;

        public ChairBookingController(ApiContext context)
        {
            _context = context;
        }

        // Tạo lịch đặt mới
        [HttpPost]
        public IActionResult Create([FromBody] ChairBookings booking)
        {
            if (booking == null || booking.StartTime >= booking.EndTime)
            {
                return BadRequest("Thông tin đặt lịch không hợp lệ.");
            }

            var conflictingBookings = _context.ChairBookings
                .Where(b => b.ChairNumber == booking.ChairNumber
                            && b.DateBooking == booking.DateBooking
                            && !(booking.EndTime <= b.StartTime || booking.StartTime >= b.EndTime))
                .ToList();

            if (conflictingBookings.Any())
            {
                var conflict = conflictingBookings.First();
                return Conflict($"Ghế {booking.ChairNumber} đã có lịch từ {conflict.StartTime} đến {conflict.EndTime}. Vui lòng chọn giờ khác.");
            }

            _context.ChairBookings.Add(booking);
            _context.SaveChanges();
            return Ok("Đặt lịch thành công.");
        }

        // Lấy lịch đặt theo ngày hoặc trạng thái các ghế
        [HttpGet]
        public IActionResult Read([FromQuery] string date, [FromQuery] bool getStatuses = false)
        {
            if (!DateOnly.TryParse(date, out var parsedDate))
            {
                return BadRequest("Ngày không hợp lệ.");
            }

            var chairBookings = _context.ChairBookings
                .Where(b => b.DateBooking == parsedDate)
                .ToList();

            if (getStatuses)
            {
                var chairStatuses = new List<ChairStatus>();

                for (int i = 1; i <= 15; i++)
                {
                    var bookingsOnChair = chairBookings.Where(b => b.ChairNumber == i).ToList();
                    var status = bookingsOnChair.Count == 0 ? "empty" : "available";
                    chairStatuses.Add(new ChairStatus { ChairNumber = i, Status = status });
                }

                return Ok(chairStatuses);
            }

            return Ok(chairBookings);
        }
    }
}
