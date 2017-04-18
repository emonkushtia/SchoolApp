namespace SchoolApp.Core.DataTransferObjects
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class CourseCreateItem
    {
        public CourseCreateItem()
        {
            this.StudentList = new List<int>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public List<int> StudentList { get; set; }
    }
}
