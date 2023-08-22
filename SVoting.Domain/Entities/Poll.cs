namespace SVoting.Domain.Entities;

public class Poll
{
	public Guid Id { get; set; }
	public string Title { get; set; }
	public bool Published { get; set; } = false;
	public DateTime TimeStamp { get; set; } = DateTime.UtcNow;

	public Guid PollingSpaceId { get; set; }
	public PollingSpace? PollingSpace { get; set; }

	public ICollection<PollCategory> PollCategories { get; set; } = default!;

	public Poll(string title, Guid spaceId)
	{
		Title = title;
		PollingSpaceId = spaceId;
	}
}

