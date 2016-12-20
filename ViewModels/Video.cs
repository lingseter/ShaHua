using System;

namespace ViewModels
{
    public class Video
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Brief { get; set; }
        public int? LikeCount { get; set; }
        public DateTime UpLoadDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? Status { get; set; }
    }
}
