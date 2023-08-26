namespace SVoting.Domain.Entities;

public class Vote
{
	public Guid Id { get; set; }
	public string VotingCode { get; set; } = string.Empty;

	public Guid PollCategoryId { get; set; }
	public PollCategory? PollCategory { get; set; }

	public Guid NomineeId { get; set; }
	public Nominee? Nominee { get; set; }

	public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
}

