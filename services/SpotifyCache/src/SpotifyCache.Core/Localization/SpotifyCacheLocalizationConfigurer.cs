using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace SpotifyCache.Localization
{
    public static class SpotifyCacheLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(SpotifyCacheConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(SpotifyCacheLocalizationConfigurer).GetAssembly(),
                        "SpotifyCache.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
