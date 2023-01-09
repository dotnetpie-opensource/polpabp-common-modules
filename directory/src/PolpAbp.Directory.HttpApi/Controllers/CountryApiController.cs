using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolpAbp.Directory.Dtos;
using PolpAbp.Directory.Services;

namespace PolpAbp.Directory.Controllers
{
    // [Authorize]
    [Route("api/directory")]
    public class CountryApiController : DirectoryController
	{
		private readonly ICountryAppService _countryAppService;

        public CountryApiController(ICountryAppService countryAppService)
		{
			_countryAppService = countryAppService;
		}

        // List all countries.
		[HttpGet("list-countries")]
		public Task<IEnumerable<CountryOutputDto>> ListCountriesAsync(CancellationToken cancellationToken = default)
		{
			return _countryAppService.ListAsyc(cancellationToken);
		}

        [HttpGet("list-states-by-country/{id:Guid}")]
        public Task<IEnumerable<StateProvinceOutputDto>> ListStatesByCountryAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            return _countryAppService.ListStateProvincesByCountryAsync(id, cancellationToken);
        }
    }
}

