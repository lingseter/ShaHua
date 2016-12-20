using System;

namespace DataModels
{
    public class Comment
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public Guid ObjId { get; set; }
        public string Contents { get; set; }
        public DateTime Date { get; set; }
    }
}
