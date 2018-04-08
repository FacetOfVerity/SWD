using System;
using Microsoft.Extensions.DependencyInjection;

namespace SWD.Utils.Docx.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddBuilderWithPredefinedTemplates(this IServiceCollection services,
            Action<PredefinedTemplatesOptions> setUp)
        {
            var options = new PredefinedTemplatesOptions();
            setUp.Invoke(options);

            services.AddSingleton(options);
            services.AddScoped<IDocumentBuilderProvider, PredefinedTemplatesProvider>();

            return services;
        }
    }
}
