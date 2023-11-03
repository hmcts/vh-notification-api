using System;
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
                var existingTemplates = templates.Where(x => x.NotificationType == template.NotificationType).ToList();

                // if no templates exist for a given type, just add
                if (!existingTemplates.Any())
                {
                    _context.Templates.Add(template);
                    _context.SaveChanges();
                }

                // if multiple templates exist for a given type, remove all and add again from the source
                var duplicateTemplates = existingTemplates.Count > 1;
                var nonMatchingTemplate = existingTemplates.Count == 1 &&
                                          existingTemplates[0].NotifyTemplateId != template.NotifyTemplateId
                                          && existingTemplates[0].Parameters != template.Parameters;
                var paramsDoNotMatch = existingTemplates.Count == 1 &&
                                       existingTemplates[0].Parameters != template.Parameters;
                if (duplicateTemplates || nonMatchingTemplate || paramsDoNotMatch)
                {
                    _context.Templates.RemoveRange(existingTemplates);
                    _context.Templates.Add(template);
                    _context.SaveChanges();
                }
            }
            _context.SaveChanges();
        }
    }

}
