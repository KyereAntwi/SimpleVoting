using System;
namespace SVoting.Shared.Models;

public class VoteDto
{
    public Guid Id { get; set; }
    public string VotingCode { get; set; } = string.Empty;
    public Guid NomineeId { get; set; }
    public Guid PollCategoryId { get; set; }
    public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
}

