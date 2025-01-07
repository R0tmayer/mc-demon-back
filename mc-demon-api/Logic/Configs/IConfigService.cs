using mc_demon_api.Logic.Profiles;

namespace mc_demon_api.Logic.Configs
{
    public interface IConfigService
    {
        void CreateConfigFile(CreateConfig createConfig);
        void UpdateAllConfigs();
    }
}
