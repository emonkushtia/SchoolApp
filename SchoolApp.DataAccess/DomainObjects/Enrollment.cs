namespace SchoolApp.DataAccess.DomainObjects
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Enrollments")]
    public class Enrollment
    {
        [Key]
        public int Id { get; set; }

        public DateTime EnrollmentDate { get; set; }

        public int StudentId { get; set; }

        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; }

        public int CourseId { get; set; }

        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }
    }
}
