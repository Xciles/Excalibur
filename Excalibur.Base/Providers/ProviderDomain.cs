namespace Excalibur.Base.Providers
{
    /// <summary>
    /// This class can be used for database objects used by projects. The class
    /// will provide a default property for the identifier TId and other supporting methods.
    /// </summary>
    /// <typeparam name="TId">  The type of Identifier to use for the database object. Ints, guids,
    ///                         etc. </typeparam>
    public abstract class ProviderDomain<TId>
    {
        /// <summary>
        /// Property for the identity used by database objects.
        /// </summary>
        /// <value>
        /// The value should never be set, outside of the classes which implement the
        /// <see cref="ProviderDomain{TId}"/>.
        /// </value>
        public TId Id { get; set; }

        // Todo add == operator
        // Todo add Equals operator

        /// <summary>
        /// This method will provide a check to see if the database object is a new object or an existing
        /// object.
        /// </summary>
        /// <returns>
        /// true if transient, false if not.
        /// </returns>
        public virtual bool IsTransient()
        {
            return Id == null || Id.Equals(default(TId));
        }
    }
}