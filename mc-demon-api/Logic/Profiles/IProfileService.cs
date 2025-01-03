namespace mc_demon_api.Logic.Profiles
{
    public interface IProfileService
    {
        List<Profile> GetAll();
        Profile GetByName(string name);
        string GenerateConfig(Profile profile);
        void AddProfile(AddProfile newProfile);
        void UpdateProfile(Profile updatedProfile);
    }
}
