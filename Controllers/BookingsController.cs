using CarBookingApi.Data;
using CarBookingApi.Dtos;
using CarBookingApi.Entities;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarBookingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly WafiDbContext _context;
        public BookingsController(WafiDbContext context)
        {
            _context = context;
        }

        // creation of new booking
        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] CreateUpdateBookingDto dto)
        {
            // checking for overlapping
            bool isConflict = await _context.Bookings.AnyAsync(b =>
            b.CarId == dto.CarId &&
            b.StartTime < dto.EndTime &&
            dto.StartTime < b.EndTime
            );
            if (isConflict)
            {
                return Conflict("Booking overlaps with an existing booking for this car.");
            }
            // now create the booking
            var booking = new Booking
            {
                CarId = dto.CarId,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                RepeatOption = dto.RepeatOption
            };
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return Ok(booking);
        }

        //Creating the GET API to Retrieve Bookings (Calendar Style)

        [HttpGet("calendar")]
        public async Task<IActionResult> GetBookings([FromQuery] BookingFilterDto filter)
        {
            var query = _context.Bookings
                .Include(b => b.Car)
                .AsQueryable();
            // applyig filters if it is present
            if (filter.CarId.HasValue)
            {
                query = query.Where(b => b.CarId == filter.CarId.Value);
            }

            if (filter.StartDate.HasValue)
            {
                query = query.Where(b => b.EndTime >= filter.StartDate.Value);
            }

            if (filter.EndDate.HasValue)
            {
                query = query.Where(b => b.StartTime <= filter.EndDate.Value);
            }
            var results = await query.ToListAsync();

            var calendarEntries = new List<BookingCalendarDto>();
            foreach (var booking in results)
            {
                DateTime currentStart = booking.StartTime;
                DateTime currentEnd = booking.EndTime;

                while (currentStart <= booking.EndTime)
                {
                    if (filter.StartDate.HasValue && currentEnd < filter.StartDate.Value)
                        break;
                    if (filter.EndDate.HasValue && currentStart > filter.EndDate.Value)
                        break;
                    calendarEntries.Add(new BookingCalendarDto
                    {
                        CarId = booking.CarId,
                        CarName = booking.Car.Name,
                        StartTime = currentStart,
                        EndTime = currentEnd
                    });
                    if (booking.RepeatOption == "Daily")
                    {
                        currentStart = currentStart.AddDays(1);
                        currentEnd = currentEnd.AddDays(1);
                    }
                    if (booking.RepeatOption == "Weekly")
                    {
                        currentStart = currentStart.AddDays(7);
                        currentEnd = currentEnd.AddDays(7);
                    }
                    else
                    {
                        break; // does not repeat
                    }

                }
            }

            return Ok(calendarEntries);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(int id, [FromBody] CreateUpdateBookingDto dto)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
                return NotFound("Booking Not Found");
            bool isConflict = await _context.Bookings.AnyAsync(b =>
            b.Id != id &&
            b.CarId == dto.CarId &&
            b.StartTime < dto.EndTime &&
            dto.EndTime < b.EndTime

            );
            if (isConflict)
            {
                return Conflict("Updated time overlaps with another booking.");

            }
            booking.CarId = dto.CarId;
            booking.StartTime = dto.StartTime;
            booking.EndTime = dto.EndTime;
            booking.RepeatOption = dto.RepeatOption;

            await _context.SaveChangesAsync();
            return Ok(booking);

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if(booking == null)
            {
                return NotFound("Booking Not Found");
            }
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return Ok(booking);
        }
    }

}
