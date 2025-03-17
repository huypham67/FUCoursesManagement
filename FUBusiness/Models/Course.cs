using System;
using System.Collections.Generic;

namespace FUBusiness.Models;

public partial class Course
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Category { get; set; }

    public string CourseCode { get; set; } = null!;

    public int? Capacity { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<EnrollmentRecord> EnrollmentRecords { get; set; } = new List<EnrollmentRecord>();
}
