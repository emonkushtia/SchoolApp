namespace SchoolApp.Core.DataTransferObjects
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class CourseItem
    {
        public CourseItem()
        {
            this.StudentItems = new List<StudentItem>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public List<StudentItem> StudentItems { get; set; }
    }
}
