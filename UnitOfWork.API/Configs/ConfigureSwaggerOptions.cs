using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace UnitOfWork.API.Configs
{
    public class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                var openApiInfo = new OpenApiInfo
                {
                    Title = $"API v{description.ApiVersion}",
                    Version = description.ApiVersion.ToString(),
                };

                options.SwaggerDoc(description.GroupName, openApiInfo);
            }
        }

        public void Configure(string? name, SwaggerGenOptions options)
        {
            Configure(options);
        }
    }
}
