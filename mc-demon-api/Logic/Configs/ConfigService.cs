using mc_demon_api.Logic.Profiles;
using Microsoft.Extensions.Options;

namespace mc_demon_api.Logic.Configs
{
    public class ConfigService : IConfigService
    {
        private readonly IProfileService _profileService;
        private readonly SourceOptions _sourceOptions;


        public ConfigService(IProfileService profileService, IOptions<SourceOptions> sourceOptions)
        {
            _profileService = profileService;
            _sourceOptions = sourceOptions.Value;
        }

        public void CreateConfigFile(CreateConfig createConfig)
        {
            var filePath = Path.Combine(_sourceOptions.CONFIGS_FOLDER_PATH, createConfig.ConfigName);
            File.WriteAllText(filePath, createConfig.Content);
        }

        public void UpdateAllConfigs()
        {
            var profiles = _profileService.GetAll();

            if (profiles.Count <= 0)
            {
                throw new Exception("No profiles found");
            }
            
            var profileDict = new Dictionary<string, string>();

            foreach (var profile in profiles)
            {
                var content = _profileService.GenerateConfig(profile);
                profileDict.Add(profile.ConfigName, content);
            }

            foreach (var pair in profileDict)
            {
                var configName = pair.Key;
                var content = pair.Value;

                string filePath =
                    Path.Combine(_sourceOptions.CONFIGS_FOLDER_PATH, configName);

                File.WriteAllText(filePath, content);
            }
        }
    }
}