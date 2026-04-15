namespace EventBookingAPI.DTOs;

public class EventListDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime EventDate { get; set; }
    public int Capacity { get; set; }
    public int BookedSeats { get; set; }
    public int AvailableSeats { get; set; }
    public decimal Price { get; set; }
    public string Location { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string Tag { get; set; } = string.Empty;
}

public class EventDetailDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime EventDate { get; set; }
    public int Capacity { get; set; }
    public int BookedSeats { get; set; }
    public int AvailableSeats { get; set; }
    public decimal Price { get; set; }
    public string Location { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string Tag { get; set; } = string.Empty;
}

public class RegisterRequestDto
{
    public Guid UserId { get; set; }
}

public class BookingResponseDto
{
    public Guid Id { get; set; }
    public Guid EventId { get; set; }
    public string EventTitle { get; set; } = string.Empty;
    public DateTime EventDate { get; set; }
    public DateTime BookingTime { get; set; }
    public decimal Price { get; set; }
    public string Location { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string Tag { get; set; } = string.Empty;
}

