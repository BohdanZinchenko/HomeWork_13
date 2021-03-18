namespace DepsWebApp.Options
{
    /// <summary>
    /// Structure of options of rates
    /// </summary>
    public class RatesOptions
    {
        /// <summary>
        /// Base currency
        /// </summary>
        public string BaseCurrency { get; set; }
        /// <summary>
        /// Valid check
        /// </summary>
        public bool IsValid => !string.IsNullOrWhiteSpace(BaseCurrency);
    }
}
