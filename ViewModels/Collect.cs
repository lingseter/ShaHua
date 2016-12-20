using System;

namespace ViewModels
{
    public class Collect
    {
        public Guid UserId { get; set; }
        public int Type { get; set; }
        public Guid ObjId { get; set; }
        public DateTime Date { get; set; }
    }
}
