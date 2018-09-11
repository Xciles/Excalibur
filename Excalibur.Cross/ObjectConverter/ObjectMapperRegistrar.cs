using System;

namespace Excalibur.Cross.ObjectConverter
{
    public static class ObjectMapperRegistrar
    {
        public static void RegisterBinding<TInterface>(Func<TInterface> serviceConstructor) where TInterface : class
        {
            
        }

        public static void RegisterBinding<TInterface>(TInterface service) where TInterface : class
        {
            
        }
    }
}
