using System;

namespace DepsWebApp.Options
{
    /// <summary>
    /// Options of Nbu Client
    /// </summary>
    public class NbuClientOptions
    {
        /// <summary>
        /// Base address with URL
        /// </summary>
        public string BaseAddress { get; set; }
        /// <summary>
        /// Valid check
        /// </summary>
        public bool IsValid => !string.IsNullOrWhiteSpace(BaseAddress) &&
                               Uri.TryCreate(BaseAddress, UriKind.Absolute, out _);
    }
}
