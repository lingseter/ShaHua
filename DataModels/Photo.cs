using System;

namespace DataModels
{
    public class Photo
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public float Storage { get; set; }
        public string Path { get; set; }
        public int Type { get; set; }
        public int? LikeCount { get; set; }
        public DateTime UpLoadDate { get; set; }
        public DateTime? ModifyDate { get; set; }    
        public int? Status { get; set; }
    }
}
