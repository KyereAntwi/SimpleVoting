using System;
namespace SVoting.Domain.Entities;

public class PollingCategory
{
	public Guid Id { get; set; }
	public string Identifyer { get; set; } = string.Empty;

    public ICollection<PollCategory> PollCategories { get; set; } = default!;

    public PollingCategory(string identifyer)
	{
		Identifyer = identifyer;
	}
}

