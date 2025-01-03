using CsvHelper.Configuration;
using CsvHelper;
using System.IO;
using mc_demon_api.Logic.Profiles;
using mc_demon_api.Logic.Templates;

namespace mc_demon_api.Logic.Configs
{
    public class ConfigService : IConfigService
    {
        private readonly ITemplateService _templateService;

        public ConfigService(ITemplateService templateService)
        {
            _templateService = templateService;
        }

        public void CreateConfigFile(CreateConfig createConfig)
        {
            var filePath = Path.Combine(GlobalVariables.CONFIGS_FOLDER_PATH, createConfig.ConfigName);
            File.WriteAllText(filePath, createConfig.Content);
        }
    }
}
