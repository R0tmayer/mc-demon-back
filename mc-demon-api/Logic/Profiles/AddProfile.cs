namespace mc_demon_api.Logic.Profiles
{
    public class AddProfile
    {
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfigName { get; set; } = string.Empty;
        public int TemplateId { get; set; }
    }
}
