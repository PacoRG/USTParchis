
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.API.Services
{
    public class JsonStringLocalizer : IStringLocalizer
    {
        string _resourdeRelativePath;
        CultureInfo _cultureInfo;
        JObject _resourceCache;

        public JsonStringLocalizer(
            string resourdeRelativePath, 
            CultureInfo cultureInfo)
        {
            _resourdeRelativePath = resourdeRelativePath;
            _cultureInfo = cultureInfo;
        }

        JObject GetResource()
        {
            if (_resourceCache == null)
            {
                string tag = _cultureInfo.Name;

                string filePath = $"{_resourdeRelativePath}/{tag}.json";

                string json = File.Exists(filePath) ?
                    File.ReadAllText(filePath, Encoding.Unicode) :
                    "{}";

                _resourceCache = JObject.Parse(json);
            }

            return _resourceCache;
        }

        public LocalizedString this[string name]
        {
            get
            {
               return this[name, null];
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                var resources = GetResource();
                string value = resources.Value<string>(name);

                bool resourceExists = !string.IsNullOrWhiteSpace(value);

                if (resourceExists)
                {
                    if (arguments == null)
                    {
                        value = string.Format(value, arguments);
                    }

                }
                else
                {
                    value = name;
                }

                return new LocalizedString(name, value, !resourceExists);
            }
        }
        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            var resources = GetResource();

            foreach(var pair in resources)
            {
                yield return new LocalizedString(pair.Key, pair.Value.Value<string>());
            }
        }

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            return new JsonStringLocalizer(_resourdeRelativePath, culture);
        }
    }
}
