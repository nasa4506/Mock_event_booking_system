using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventBookingAPI.Models;

[Table("events")]
public class Event
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("title")]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Column("description")]
    public string Description { get; set; } = string.Empty;

    [Column("event_date")]
    public DateTime EventDate { get; set; }

    [Column("capacity")]
    public int Capacity { get; set; }

    [Column("price")]
    public decimal Price { get; set; }

    [Column("location")]
    [MaxLength(200)]
    public string Location { get; set; } = string.Empty;

    [Column("image_url")]
    [MaxLength(500)]
    public string ImageUrl { get; set; } = string.Empty;

    [Column("tag")]
    [MaxLength(50)]
    public string Tag { get; set; } = string.Empty;

    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}

