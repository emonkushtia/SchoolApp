namespace SchoolApp.Core.DataTransferObjects
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class StudentItem
    {
        public StudentItem()
        {
            this.CourseItems = new List<CourseItem>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Roll { get; set; }

        public List<CourseItem> CourseItems { get; set; }
    }
}
