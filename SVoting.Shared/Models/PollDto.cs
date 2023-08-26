namespace SVoting.Shared.Models;

public class PollDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool Published { get; set; } = false;
    public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
    public Guid PollingSpaceId { get; set; }

    public ICollection<CategoryDto>? Categories { get; set; } = new List<CategoryDto>();
}

