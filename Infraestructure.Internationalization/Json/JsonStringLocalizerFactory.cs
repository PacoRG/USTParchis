using Infraestructure.API.Services;
using Infraestructure.Internationalization.Json;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System;
using System.Globalization;
using System.Reflection;

namespace Infraestructure.Internationalization
{
    public class JsonStringLocalizerFactory : IStringLocalizerFactory
    {
        string _resourceRelativePath;
        string _sharedResourceName;

        public JsonStringLocalizerFactory(IOptions<JsonLocalizationOptions> options)
        {
            _resourceRelativePath = options.Value?.ResourcePath ?? string.Empty;
            _sharedResourceName = options.Value?.SharedResourceName ?? string.Empty;
        }

        public IStringLocalizer Create(Type resourceSource)
        {
            if (resourceSource == null)
                return new JsonStringLocalizer(
                    _resourceRelativePath,
                    _sharedResourceName,
                    CultureInfo.CurrentUICulture);

            return new JsonStringLocalizer(
                _resourceRelativePath,
                resourceSource.Name,
                CultureInfo.CurrentUICulture);
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            throw new NotImplementedException();
        }
    }
}
