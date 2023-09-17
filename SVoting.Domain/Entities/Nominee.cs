namespace SVoting.Domain.Entities;

public class Nominee
{
	public Guid Id { get; set; }
	public string Fullname { get; set; } = string.Empty;
	public byte[]? Photograph { get; set; }
	public string? Photomime { get; set; }

	public string UserName { get; set; } = string.Empty;

	public ICollection<NomineeCategory> NomineeCategories { get; set; } = default!;
	public ICollection<Vote> Votes { get; set; } = default!;
}

