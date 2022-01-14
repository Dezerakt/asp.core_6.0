namespace asp_project.Models.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public int RoleId { get; set; }
        public Role? Role { get; set; }
        public DateTime Birthday { get; set; } = DateTime.Now;
    }
}
