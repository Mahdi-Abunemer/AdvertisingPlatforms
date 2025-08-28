using System;

namespace AdvertisingPlatforms.Domain.Exceptions
{
    /// <summary>
    /// Domain-level validation exception for invariants inside domain objects.
    /// </summary>
    public class DomainValidationException : Exception
    {
        public DomainValidationException()
        {
        }

        public DomainValidationException(string message)
            : base(message)
        {
        }

        public DomainValidationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
