using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using DepsWebApp.Services;
using Microsoft.AspNetCore.Authorization;

namespace DepsWebApp.Controllers
{
    /// <summary>
    /// Rates controller 
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Authorize]

    public class RatesController : ControllerBase
    {
        private readonly ILogger<RatesController> _logger;
        private readonly IRatesService _rates;

        /// <summary>
        /// Rates Controller
        /// </summary>
        /// <param name="rates"></param>
        /// <param name="logger"></param>
        public RatesController(
            IRatesService rates,
            ILogger<RatesController> logger)
        {
            _rates = rates;
            _logger = logger;
        }

        /// <summary>
        /// Get method
        /// </summary>
        /// <param name="srcCurrency"></param>
        /// <param name="dstCurrency"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        [HttpGet("{srcCurrency}/{dstCurrency}")]
        public async Task<ActionResult<decimal>> Get(string srcCurrency, string dstCurrency, decimal? amount)
        {
            var exchange =  await _rates.ExchangeAsync(srcCurrency, dstCurrency, amount ?? decimal.One);
            if (!exchange.HasValue)
            {
                _logger.LogDebug($"Can't exchange from '{srcCurrency}' to '{dstCurrency}'");
                return BadRequest("Invalid currency code");
            }
            return exchange.Value.DestinationAmount;
        }
    }
}
