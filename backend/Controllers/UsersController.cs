using EventBookingAPI.Data;
using EventBookingAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace EventBookingAPI.Controllers;

[ApiController]
[Route("api/users/{userId}/bookings")]
public class UsersController : ControllerBase
{
    private readonly EventBookingContext _context;

    public UsersController(EventBookingContext context)
    {
        _context = context;
    }

    // GET /api/users/{userId}/bookings
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookingResponseDto>>> GetUserBookings(Guid userId)
    {
        var currentUserIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (currentUserIdStr == null || currentUserIdStr != userId.ToString())
        {
            return Forbid();
        }

        var userExists = await _context.Users.AnyAsync(u => u.Id == userId);
        if (!userExists)
            return NotFound(new { message = "User not found." });

        var bookings = await _context.Bookings
            .Where(b => b.UserId == userId)
            .Include(b => b.Event)
            .Select(b => new BookingResponseDto
            {
                Id = b.Id,
                EventId = b.EventId,
                EventTitle = b.Event.Title,
                EventDate = b.Event.EventDate,
                BookingTime = b.BookingTime,
                Price = b.Event.Price,
                Location = b.Event.Location,
                ImageUrl = b.Event.ImageUrl,
                Tag = b.Event.Tag
            })
            .ToListAsync();

        return Ok(bookings);
    }
}

