using System.Linq;

namespace NotificationApi.DAL;

public class TemplateDataSeeding(NotificationsApiDbContext context)
{
    private readonly TemplateDataForEnvironments _templateDataForEnvironments = new ();
    
    public void Run(string environment)
    {
        context.Database.EnsureCreated();
        
        var templates = context.Templates;
        var sourceTemplates = _templateDataForEnvironments.Get(environment);
        
        foreach (var template in sourceTemplates)
        {
            var existingTemplates = templates.Where(x => x.NotificationType == template.NotificationType).ToList();
            
            // if no templates exist for a given type, just add
            if (existingTemplates.Count == 0)
            {
                context.Templates.Add(template);
                context.SaveChanges();
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
                context.Templates.RemoveRange(existingTemplates);
                context.Templates.Add(template);
                context.SaveChanges();
            }
        }
        context.SaveChanges();
    }
}
