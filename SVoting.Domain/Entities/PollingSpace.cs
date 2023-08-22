namespace SVoting.Domain.Entities;

public class PollingSpace
{
	public Guid Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string Industry { get; set; } = string.Empty;
	public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
	public string? UserId { get; set; }
	public bool Active { get; set; } = true;

	public ICollection<Poll> Polls { get; set; } = default!;

	public PollingSpace(string name, string industry)
	{
		Name = name;
		Industry = industry;
	}
}

