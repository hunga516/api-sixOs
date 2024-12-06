using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api_sixOs.Models;
using api_sixOs.Data;

namespace api_sixOs.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly ApiContext _context;

        public BookingController(ApiContext context)
        {
            _context = context;
        }

        //Create
        [HttpPost]
        public JsonResult Create([FromForm] Booking booking)
        {
            _context.Booking.Add(booking);

            _context.SaveChanges();

            return new JsonResult(Ok(booking));
        }

        //Read
        [HttpGet]
        public JsonResult Read()
        {
            var booking = _context.Booking.ToList();

            return new JsonResult(Ok(booking));
        }
    }
}
