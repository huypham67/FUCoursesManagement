using System;
using System.Collections.Generic;

namespace FUBusiness.Models;

public partial class User
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Role { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<EnrollmentRecord> EnrollmentRecords { get; set; } = new List<EnrollmentRecord>();

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
}
