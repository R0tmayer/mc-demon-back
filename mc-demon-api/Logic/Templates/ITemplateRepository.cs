namespace mc_demon_api.Logic.Templates
{
    public interface ITemplateRepository
    {
        List<Template> GetAll();
        string GetRawContentById(int id);
        List<string> GetTemplateMarks(int id);
    }
}
