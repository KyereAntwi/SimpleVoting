using System;
namespace SVoting.Shared.Models;

public class NomineeDto
{
    public Guid Id { get; set; }
    public string Fullname { get; set; } = string.Empty;
}

