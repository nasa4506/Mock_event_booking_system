using EventBookingAPI.Data;
using EventBookingAPI.DTOs;
using EventBookingAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace EventBookingAPI.Controllers;

[ApiController]
[Route("api/events")]
public class EventsController : ControllerBase
{
    private readonly EventBookingContext _context;

    public EventsController(EventBookingContext context)
    {
        _context = context;
    }

    // GET /api/events
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EventListDto>>> GetAllEvents()
    {
        var events = await _context.Events
            .Select(e => new EventListDto
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                EventDate = e.EventDate,
                Capacity = e.Capacity,
                BookedSeats = e.Bookings.Count,
                AvailableSeats = e.Capacity - e.Bookings.Count,
                Price = e.Price,
                Location = e.Location,
                ImageUrl = e.ImageUrl,
                Tag = e.Tag
            })
            .ToListAsync();

        return Ok(events);
    }

    // GET /api/events/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<EventDetailDto>> GetEventById(Guid id)
    {
        var eventDetail = await _context.Events
            .Where(e => e.Id == id)
            .Select(e => new EventDetailDto
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                EventDate = e.EventDate,
                Capacity = e.Capacity,
                BookedSeats = e.Bookings.Count,
                AvailableSeats = e.Capacity - e.Bookings.Count,
                Price = e.Price,
                Location = e.Location,
                ImageUrl = e.ImageUrl,
                Tag = e.Tag
            })
            .FirstOrDefaultAsync();

        if (eventDetail == null)
            return NotFound(new { message = "Event not found." });

        return Ok(eventDetail);
    }

    // POST /api/events/{eventId}/register
    [Authorize]
    [HttpPost("{eventId}/register")]
    public async Task<IActionResult> RegisterForEvent(Guid eventId)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // 1. Check if event exists
            var eventEntity = await _context.Events
                .FirstOrDefaultAsync(e => e.Id == eventId);

            if (eventEntity == null)
                return NotFound(new { message = "Event not found." });

            // 2. Check capacity
            var bookedCount = await _context.Bookings
                .CountAsync(b => b.EventId == eventId);

            if (bookedCount >= eventEntity.Capacity)
                return BadRequest(new { message = "Event is fully booked. No seats available." });

            // 3. Extract User ID from Claims
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdString, out Guid currentUserId))
            {
                return Unauthorized(new { message = "Invalid user token." });
            }

            // 4. Check for duplicate booking
            var existingBooking = await _context.Bookings
                .AnyAsync(b => b.UserId == currentUserId && b.EventId == eventId);

            if (existingBooking)
                return Conflict(new { message = "User is already registered for this event." });

            // 5. Create booking
            var booking = new Booking
            {
                Id = Guid.NewGuid(),
                UserId = currentUserId,
                EventId = eventId,
                BookingTime = DateTime.UtcNow
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return CreatedAtAction(nameof(GetEventById), new { id = eventId }, new
            {
                bookingId = booking.Id,
                userId = booking.UserId,
                eventId = booking.EventId,
                bookingTime = booking.BookingTime
            });
        }
        catch (DbUpdateException)
        {
            await transaction.RollbackAsync();
            return Conflict(new { message = "Duplicate booking or data conflict." });
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            return StatusCode(500, new { message = "An error occurred while processing the booking." });
        }
    }
}

