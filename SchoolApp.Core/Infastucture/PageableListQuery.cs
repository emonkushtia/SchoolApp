namespace SchoolApp.Core.Infastucture
{
    using System;

    [Serializable]
    public class PageableListQuery
    {
        public PageableListQuery()
        {
            this.Offset = 0;
            this.Limit = 10;
        }

        public string Sort { get; set; }

        public int? Offset { get; set; }

        public int Limit { get; set; }
    }
}