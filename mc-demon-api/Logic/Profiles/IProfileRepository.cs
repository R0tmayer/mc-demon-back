namespace mc_demon_api.Logic.Profiles
{
    public interface IProfileRepository
    {
        void Add(Profile profile);
        void Update(Profile profile);
        List<Profile> GetAll();
        Profile GetByName(string name);
    }
}
