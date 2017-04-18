namespace SchoolApp.DataAccess.DomainObjects
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Students")]
    public class Student
    {
        public Student()
        {
            this.Enrollments = new HashSet<Enrollment>();
        }

        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        [Required]
        public string Name { get; set; }

        [StringLength(20)]
        [Required]
        public string Roll { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
