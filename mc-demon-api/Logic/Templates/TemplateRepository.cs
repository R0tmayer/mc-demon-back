

using CsvHelper;
using CsvHelper.Configuration;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Options;

namespace mc_demon_api.Logic.Templates
{
    public class TemplateRepository : ITemplateRepository
    {
        private readonly CsvConfiguration _csvConfig;
        private readonly SourceOptions _sourceOptions;

        public TemplateRepository(CsvConfiguration csvConfig, IOptions<SourceOptions> sourceOptions)
        {
            _csvConfig = csvConfig;
            _sourceOptions = sourceOptions.Value;
        }

        public List<Template> GetAll()
        {
            using var reader = new StreamReader(_sourceOptions.TEMPLATES_CSV_PATH);
            using var csv = new CsvReader(reader, _csvConfig);
            return csv.GetRecords<Template>().ToList();
        }

        public string GetRawContentById(int id)
        {
            var template = GetAll().FirstOrDefault(t => t.Id == id);
            var filePath = Path.Combine(_sourceOptions.TEMPLATES_FOLDER_PATH, template.FileName);
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
