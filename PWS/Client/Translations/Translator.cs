using System.Collections.Generic;
using Websites.Razor.ClassLibrary.Components;

// ReSharper disable InconsistentNaming

namespace Client.Translations
{
    static class Translator
    {
        public static string Get(
            string id,
            string language,
            Dictionary<string, string[]> translations)
        {
            int languageIndex;

            switch (language)
            {
                case LanguageSelectorBase.LanguageEn: languageIndex = 0; break;
                case LanguageSelectorBase.LanguageDe: languageIndex = 1; break;
                case LanguageSelectorBase.LanguageIt: languageIndex = 2; break;
                default: languageIndex = 0; break;

            }

            if (translations.TryGetValue(id, out var values)) return values[languageIndex];
            return @"translation error";
        }
    }
}
