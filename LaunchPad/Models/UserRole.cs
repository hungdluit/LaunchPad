namespace LaunchPad.Models
{
    public class UserRole
    {
        public string Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
