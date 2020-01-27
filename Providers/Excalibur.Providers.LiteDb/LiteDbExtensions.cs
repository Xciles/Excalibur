using System;
using System.Collections.Generic;
using System.Text;
using Excalibur.Base.Providers;
using MvvmCross.IoC;

namespace Excalibur.Providers.LiteDb
{
    public static class LiteDbExtensions
    {
        /// <summary>
        /// Registers a <see cref="LiteDbProvider{TKey, TDomain}"/> as implementation for <see cref="IDatabaseProvider{TId,T}"/>
        /// </summary>
        /// <typeparam name="TKey">The type of the unique identifier of the domain entity</typeparam>
        /// <typeparam name="TDomain">The domain entity to be saved</typeparam>
        public static void RegisterDefaultDatabaseProvider<TKey, TDomain>(this IMvxIoCProvider ioCProvider)
            where TDomain : ProviderDomain<TKey>
        {
            ioCProvider.RegisterType<IDatabaseProvider<TKey, TDomain>, LiteDbProvider<TKey, TDomain>>();
        }
    }
}
