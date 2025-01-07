using CsvHelper;
using CsvHelper.Configuration;
using mc_demon_api.Logic.Configs;
using mc_demon_api.Logic.Profiles;
using mc_demon_api.Logic.Templates;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace mc_demon_api.Controllers
{
    public class HomeController : ControllerBase
    {
        private readonly IProfileService _profileService;
        private readonly ITemplateService _templateService;
        private readonly IConfigService _configService;

        public HomeController(IProfileService profileService, ITemplateService templateService, IConfigService configService)
        {
            _profileService = profileService;
            _templateService = templateService;
            _configService = configService;
        }

        [HttpGet("info")]
        public IActionResult GetInfo()
        {
            var profiles = _profileService.GetAll();
            var templates = _templateService.GetAll();

            var result = new
            {
                Profiles = profiles,
                Templates = templates
            };

            return Ok(result);
        }

        [HttpPost("profiles")]
        public IActionResult AddProfile([FromBody] AddProfile newProfile)
        {
            _profileService.AddProfile(newProfile);
            return Ok();
        }

        [HttpPut("profiles")]
        public IActionResult UpdateProfile([FromBody] Profile updatedProfile)
        {
            _profileService.UpdateProfile(updatedProfile);
            return Ok();
        }

        [HttpGet("configs/{profileName}")]
        public IActionResult GenerateConfig(string profileName)
        {
            var profile = _profileService.GetByName(profileName);
            var content = _profileService.GenerateConfig(profile);
            return Ok(content);
        }

        [HttpPost("configs")]
        public IActionResult CreateConfigFile([FromBody] CreateConfig createConfig)
        {
            _configService.CreateConfigFile(createConfig);
            return Ok();
        }

        [HttpPut("configs")]
        public IActionResult UpdateAllConfigs()
        {
            _configService.UpdateAllConfigs();
            return Ok();
        }
    }
}
