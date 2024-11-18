using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recordings.Infrastructure.Models;

[Table("Customers")]
public class CustomerDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }

    public List<VideoDbModel>? Videos { get; set; } = new List<VideoDbModel>();
}
