namespace asp_project.Models.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public string Pages { get; set; } = String.Empty;
        public List<User>? Users { get; set; }
    }
}
