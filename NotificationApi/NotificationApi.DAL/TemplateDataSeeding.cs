using NotificationApi.DAL;
using System.Linq;

namespace NotificationApi
{
    public class TemplateDataSeeding
    {
        private readonly NotificationsApiDbContext _context;
        private readonly TemplateDataForEnvironments _templateDataForEnvironments = new TemplateDataForEnvironments();

        public TemplateDataSeeding(NotificationsApiDbContext context)
        {
            _context = context;
        }

        public void Run(string environment)
        {
            _context.Database.EnsureCreated();

            var templates = _context.Templates;
            var sourceTemplates = _templateDataForEnvironments.Get(environment);

            foreach (var template in sourceTemplates)
            {
                var existingTemplate = templates.FirstOrDefault(x => x.NotificationType == template.NotificationType);
                if (existingTemplate == null)
                {
                    _context.Templates.Add(template);
                }
                else if(existingTemplate.NotifyTemplateId != template.NotifyTemplateId)
                {
                    _context.Remove(existingTemplate);
                    _context.Templates.Add(template);
                }
            }
            _context.SaveChanges();
        }
    }

}
