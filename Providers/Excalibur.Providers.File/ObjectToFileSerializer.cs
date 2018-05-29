using Newtonsoft.Json;

namespace Excalibur.Providers.File
{
    /// <summary>
    /// Class that provides custom file serializer settings that should be used when storing a file.
    /// </summary>
    public abstract class ObjectToFileSerializer
    {
        public abstract JsonSerializerSettings JsonSerializerSettings();
    }
}