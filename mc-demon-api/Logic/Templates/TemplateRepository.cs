

using CsvHelper;
using CsvHelper.Configuration;
using System.Text.RegularExpressions;

namespace mc_demon_api.Logic.Templates
{
    public class TemplateRepository : ITemplateRepository
    {
        private readonly CsvConfiguration _csvConfig;

        public TemplateRepository(CsvConfiguration csvConfig)
        {
            _csvConfig = csvConfig;
        }

        public List<Template> GetAll()
        {
            using var reader = new StreamReader(GlobalVariables.TEMPLATES_CSV_PATH);
            using var csv = new CsvReader(reader, _csvConfig);
            return csv.GetRecords<Template>().ToList();
        }

        public string GetRawContentById(int id)
        {
            var template = GetAll().FirstOrDefault(t => t.Id == id);
            var filePath = Path.Combine(GlobalVariables.TEMPLATES_FOLDER_PATH, template.FileName);
            return File.ReadAllText(filePath);
        }

        public List<string> GetTemplateMarks(int id)
        {
            var content = GetRawContentById(id);

            var fields = new List<string>();
            var pattern = @"<(.*?)>";
            var matches = Regex.Matches(content, pattern);

            foreach (Match match in matches)
            {
                fields.Add(match.Groups[1].Value);
            }

            return fields;
        }
    }
}
