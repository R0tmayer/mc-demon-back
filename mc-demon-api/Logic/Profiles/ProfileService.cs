using CsvHelper;
using CsvHelper.Configuration;
using mc_demon_api.Logic.Templates;

namespace mc_demon_api.Logic.Profiles
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;
        private readonly ITemplateRepository _templateRepository;

        public ProfileService(IProfileRepository profileRepository, ITemplateRepository templateRepository)
        {
            _profileRepository = profileRepository;
            _templateRepository = templateRepository;
        }

        public void AddProfile(AddProfile newProfile)
        {
            var newId = GetAll().Count + 1;

            var profile = new Profile
            {
                Id = newId,
                Name = newProfile.Name,
                Password = newProfile.Password,
                ConfigName = newProfile.ConfigName,
                TemplateId = newProfile.TemplateId
            };

            _profileRepository.Add(profile);
        }

        public void UpdateProfile(Profile updatedProfile)
        {
            _profileRepository.Update(updatedProfile);
        }

        public string GenerateConfig(Profile profile)
        {
            var rawContent = _templateRepository.GetRawContentById(profile.TemplateId);
            var content = rawContent.Replace("<password>", profile.Password);
            return content;
        }

        public List<Profile> GetAll()
        {
            return _profileRepository.GetAll();
        }

        public Profile GetByName(string name)
        {
            return _profileRepository.GetByName(name);
        }

    }
}
