namespace SchoolApp.Core.DataTransferObjects
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class StudentCreateItem
    {
        public StudentCreateItem()
        {
            this.CoursesList = new List<int>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Roll { get; set; }

        public List<int> CoursesList { get; set; }
    }
}
