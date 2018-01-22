using Infraestructure.API.Services;
using Infraestructure.Internationalization.Json;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace Infraestructure.Internationalization
{
    public class JsonStringLocalizerFactory : IStringLocalizerFactory
    {
        Dictionary<string, IStringLocalizer> _localizersCache;

        string _resourceRelativePath;
        string _sharedResourceName;

        public JsonStringLocalizerFactory(IOptions<JsonLocalizationOptions> options)
        {
            _resourceRelativePath = options.Value?.ResourcePath ?? string.Empty;
            _sharedResourceName = options.Value?.SharedResourceName ?? string.Empty;

            _localizersCache = new Dictionary<string, IStringLocalizer>();

        }

        public IStringLocalizer Create(Type resourceSource)
        {
            IStringLocalizer localizer;
            string requestType = _sharedResourceName;

            if (resourceSource != null)
            {
                requestType = resourceSource.FullName;
            }

            if (!_localizersCache.TryGetValue(requestType + "-" + CultureInfo.CurrentUICulture, out localizer))
            {
                _localizersCache.Add(
                    requestType + "-" + CultureInfo.CurrentUICulture,
                    new JsonStringLocalizer(
                        _resourceRelativePath,
                        requestType,
                        CultureInfo.CurrentUICulture));
            }

            return _localizersCache.GetValueOrDefault(requestType + "-" + CultureInfo.CurrentUICulture);
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            throw new NotImplementedException();
        }
    }
}
