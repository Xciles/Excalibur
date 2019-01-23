using MvvmCross.Localization;

namespace Excalibur.Cross.Language
{
    /// <inheritdoc />
    /// <summary>
    /// Text provider that provides access to shared localized text resources
    /// Based on <see cref="MvvmCross.Plugin.JsonLocalization"/>
    /// </summary>
    public class SharedTextProvider : ISharedTextProvider
    {
        private readonly string _namespaceName;
        private readonly IMvxLanguageBinder _languageBinder;

        /// <summary>
        /// Constructor that should be called when registering an instance of this class with the DI service.
        /// Default shared localized text resources filename is 'Shared'
        /// </summary>
        /// <param name="namespaceName">Namespace containing the shared text resource(s).</param>
        public SharedTextProvider(string namespaceName)
        {
            _namespaceName = namespaceName;
            _languageBinder = new MvxLanguageBinder(_namespaceName, "Shared");
        }

        /// <summary>
        /// Constructor that should be called when registering an instance of this class with the DI service.
        /// </summary>
        /// <param name="namespaceName">Namespace containing the shared text resource(s).</param>
        /// <param name="sharedFilename">Name of the file containing the shared text resource(s) used to override the default filename ('Shared').</param>
        public SharedTextProvider(string namespaceName, string sharedFilename)
        {
            _namespaceName = namespaceName;
            _languageBinder = new MvxLanguageBinder(_namespaceName, sharedFilename);
        }

        public string GetText(string entryKey) => _languageBinder.GetText(entryKey);

        public string GetText(string entryKey, params object[] args) => _languageBinder.GetText(entryKey, args);

        /// <inheritdoc />
        public IMvxLanguageBinder GetTextResource(string resourceName)
        {
            return new MvxLanguageBinder(_namespaceName, resourceName);
        }

        /// <inheritdoc />
        public string GetTextFromResource(string resourceName, string entryKey)
        {
            return GetTextResource(resourceName).GetText(entryKey);
        }

        /// <inheritdoc />
        public string GetTextFromResource(string resourceName, string entryKey, params object[] args)
        {
            return GetTextResource(resourceName).GetText(entryKey, args);
        }
    }
}

