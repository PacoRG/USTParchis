using Microsoft.Extensions.Localization;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Infraestructure.API.Services
{
    public class JsonStringLocalizer : IStringLocalizer
    {
        string _resourdeRelativePath;
        string _typeName;
        

        CultureInfo _cultureInfo;
        JObject _resourceCache;

        public JsonStringLocalizer(
            string resourdeRelativePath, 
            string typeName,
            CultureInfo cultureInfo)
        {
            _typeName = typeName;
            _resourdeRelativePath = resourdeRelativePath;
            _cultureInfo = cultureInfo;
        }

        JObject GetResource(CultureInfo culture)
        {

            if (_resourceCache == null)
            {
                string filePath = this.GetFilePath(culture);

                if (File.Exists(filePath))
                {
                    return JObject.Parse(File.ReadAllText(filePath, Encoding.Unicode));
                }
                else if (culture.Parent != null && culture.Parent.TwoLetterISOLanguageName != "iv")
                {
                    return this.GetResource(_cultureInfo.Parent);
                }
                else
                {
                    return JObject.Parse("{}");
                }
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
                var resources = GetResource(_cultureInfo);
                string value = resources.Value<string>(name);

                bool resourceExists = !string.IsNullOrWhiteSpace(value);

                if (resourceExists)
                {
                    if (arguments != null)
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
            var resources = GetResource(_cultureInfo);

            foreach(var pair in resources)
            {
                yield return new LocalizedString(pair.Key, pair.Value.Value<string>());
            }
        }

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            return new JsonStringLocalizer(_resourdeRelativePath, _typeName, culture);
        }

        private string GetFilePath(CultureInfo culture)
        {
            string tag = culture.Name;
          
            string[] splits = _typeName.Split('.');

            string filePath = string.Empty;

            if (splits.Length > 1)
            {
                var namspace = _typeName.Substring(0, _typeName.Substring(0, _typeName.Length - 1).LastIndexOf('.'));
                filePath = $"./{_resourdeRelativePath}/{namspace}/{splits[splits.Length - 1]}-{tag}.json";
            }
            else
            {
                filePath = $"./{_resourdeRelativePath}/{_typeName}-{tag}.json";
            }
            return filePath;
        }
    }
}
