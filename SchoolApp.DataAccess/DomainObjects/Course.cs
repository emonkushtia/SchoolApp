namespace SchoolApp.DataAccess.DomainObjects
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Courses")]
    public class Course
    {
        public Course()
        {
            this.Enrollments = new HashSet<Enrollment>();
        }

        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(20)]
        public string Code { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
