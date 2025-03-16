namespace mc_demon_api.Logic.Templates
{
    public class TemplateService : ITemplateService
    {
        private readonly ITemplateRepository _templateRepository;

        public TemplateService( ITemplateRepository templateRepository)
        {
            _templateRepository = templateRepository;
        }

        public List<Template> GetAll()
        {
            return _templateRepository.GetAll();
        }

        public string GetRawContentById(int id)
        {
            return _templateRepository.GetRawContentById(id);
        }

        public List<string> GetTemplateMarks(int id)
        {
            return _templateRepository.GetTemplateMarks(id);
        }
    }
}
