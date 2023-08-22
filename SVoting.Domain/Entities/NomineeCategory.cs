namespace SVoting.Domain.Entities;

public class NomineeCategory
{
	public Guid Id { get; set; }

	public Guid NomineeId { get; set; }
	public Nominee? Nominee { get; set; }

	public Guid PollCategoryId { get; set; }
	public PollCategory? PollCategory { get; set; }
}

