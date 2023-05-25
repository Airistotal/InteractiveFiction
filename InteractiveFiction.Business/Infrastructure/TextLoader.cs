using Newtonsoft.Json.Linq;

namespace InteractiveFiction.Business.Infrastructure
{
    public class TextLoader : ITextLoader
    {
        private readonly JObject translations;

        public TextLoader()
        {
            translations = JObject.Parse(File.ReadAllText(@"res/i18n/en_CA.json"));
        }

        public string GetText(string key)
        {
            var token = translations.SelectToken(key);
            if (token == null)
            {
                return key;
            }

            return token.Value<string>() ?? key;
        }
    }
}
