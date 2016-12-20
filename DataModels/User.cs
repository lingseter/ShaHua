using System;

namespace DataModels
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public int Role { get; set; }
        public string PasswordHash { get; set; }
        public string Phone { get; set; }
        public int? Age { get; set; }
        public int? Gender { get; set; }
        public string Portrait { get; set; }
        public string Address { get; set; }
        public string Career { get; set; }
        public string Profile { get; set; }
        public string Setting { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? Status { get; set; }
    }
}
