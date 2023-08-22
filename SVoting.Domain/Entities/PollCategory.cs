using System;
namespace SVoting.Domain.Entities;

public class PollCategory
{
	public Guid Id { get; set; }

	public Guid PollId { get; set; }
	public Poll? Poll { get; set; }

	public Guid CategoryId { get; set; }
	public PollingCategory? Category { get; set; }

	public ICollection<NomineeCategory> NomineeCategories { get; set; } = default!;
    public ICollection<Vote> Votes { get; set; } = default!;
}

