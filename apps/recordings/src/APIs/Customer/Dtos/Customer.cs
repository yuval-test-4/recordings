namespace Recordings.APIs.Dtos;

public class Customer
{
    public DateTime CreatedAt { get; set; }

    public string Id { get; set; }

    public DateTime UpdatedAt { get; set; }

    public List<string>? Videos { get; set; }
}
