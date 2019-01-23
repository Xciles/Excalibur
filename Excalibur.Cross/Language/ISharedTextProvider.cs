using MvvmCross.Localization;

namespace Excalibur.Cross.Language
{
    public interface ISharedTextProvider : IMvxLanguageBinder
    {
        /// <summary>
        /// Get a Resource provider for the specified resource
        /// </summary>
        /// <param name="resourceName">Name of the localized resource e.g. 'MainViewModel'</param>
        /// <returns><see cref="IMvxLanguageBinder"/> for the specified resource</returns>
        IMvxLanguageBinder GetTextResource(string resourceName);

        /// <summary>
        /// Gets a string from a specified resource by its entry key
        /// </summary>
        /// <param name="resourceName">Name of the localized resource e.g. 'MainViewModel'</param>
        /// <param name="entryKey">Name of the localized resource entry e.g. 'MainTitle'</param>
        /// <returns><see cref="string"/>For the specified Resource and Entry</returns>
        string GetTextFromResource(string resourceName, string entryKey);

        /// <summary>
        /// Gets a string from a specified resource by its entry key
        /// </summary>
        /// <param name="resourceName">Name of the localized resource e.g. 'MainViewModel'</param>
        /// <param name="entryKey">Name of the localized resource entry e.g. 'MainTitle'</param>
        /// <param name="args">Arguments</param>
        /// <returns><see cref="string"/>For the specified Resource, Entry and Arguments</returns>
        string GetTextFromResource(string resourceName, string entryKey, params object[] args);
    }
}

