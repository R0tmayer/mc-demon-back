using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace mc_demon_api.Logic.Profiles
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly CsvConfiguration _csvConfig;

        public ProfileRepository(CsvConfiguration csvConfig)
        {
            _csvConfig = csvConfig;
        }

        public void Update(Profile updatedProfile)
        {
            var profiles = GetAll();
            var searchedProfile = profiles.SingleOrDefault(p => p.Id == updatedProfile.Id);

            if (searchedProfile is null)
            {
                throw new Exception($"Profile was not found with provided id: ${updatedProfile.Id}");
            }

            searchedProfile.Name = updatedProfile.Name;
            searchedProfile.Password = updatedProfile.Password;
            searchedProfile.ConfigName = updatedProfile.ConfigName;
            searchedProfile.TemplateId = updatedProfile.TemplateId;

            WriteProfiles(profiles);
        }

        public void Add(Profile profile)
        {
            var profiles = GetAll();
            var searchedProfile = profiles.SingleOrDefault(p => p.Name.Equals(profile.Name, StringComparison.OrdinalIgnoreCase));

            if (searchedProfile is null)
            {
                profiles.Add(profile);
            }
            else
            {
                searchedProfile.Name = profile.Name;
                searchedProfile.Password = profile.Password;
                searchedProfile.ConfigName = profile.ConfigName;
                searchedProfile.TemplateId = profile.TemplateId;
            }

            WriteProfiles(profiles);
        }

        public List<Profile> GetAll()
        {
            using var reader = new StreamReader(GlobalVariables.PROFILES_CSV_PATH);
            using var csv = new CsvReader(reader, _csvConfig);
            return csv.GetRecords<Profile>().ToList();
        }

        public Profile GetByName(string name)
        {
            var profiles = GetAll();
            return profiles.FirstOrDefault(x => x.Name == name);
        }

        private void WriteProfiles(List<Profile> profiles)
        {
            if (!File.Exists(GlobalVariables.PROFILES_CSV_PATH))
            {
                throw new FileNotFoundException("Profile's csv file didn't found");
            }

            using var writer = new StreamWriter(GlobalVariables.PROFILES_CSV_PATH, false);
            using var csv = new CsvWriter(writer, _csvConfig);
            csv.WriteHeader<Profile>();
            csv.NextRecord();
            csv.WriteRecords(profiles);
        }
    }
}
