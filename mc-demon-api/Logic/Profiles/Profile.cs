namespace mc_demon_api.Logic.Profiles
{
    public class Profile
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfigName { get; set; } = string.Empty;
        public int TemplateId { get; set; }
    }
}
