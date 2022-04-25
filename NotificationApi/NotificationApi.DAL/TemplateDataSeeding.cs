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

            var saveChanges = false;
            
            var templates = _context.Templates;
            var sourceTemplates = _templateDataForEnvironments.Get(environment);

            foreach(var template in sourceTemplates)
            {
                if (!templates.Any(t => t.NotifyTemplateId == template.NotifyTemplateId))
                {
                    _context.Templates.Add(template);
                    saveChanges = true;
                }
            }
            if (saveChanges)
            {
                _context.SaveChanges();
            }
        }
    }

}
