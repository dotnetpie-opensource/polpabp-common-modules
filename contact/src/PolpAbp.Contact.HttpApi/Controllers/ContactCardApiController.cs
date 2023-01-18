using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PolpAbp.Contact.Dtos;
using PolpAbp.Contact.Services;
using Volo.Abp.Application.Dtos;

namespace PolpAbp.Contact.Controllers
{
    [Authorize]
    [Route("api/contact/contactcards")]
    public class ContactCardApiController : ContactController
    {
        private readonly IContactCardAppService _appService;

        public ContactCardApiController(IContactCardAppService appService)
        {
            _appService = appService;
        }

        [HttpGet("{id:Guid}")]
        public async Task<ContactCardOutputDto> LoadByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            var ret = await _appService.FindByIdAsync(id, cancellationToken: cancellationToken);
            // todo: Throw an exception.
            return ret;
        }

        // todo: Permission
        [HttpPost]
        public async Task<Guid> CreateAsync([FromBody] ContactCardInputDto input, CancellationToken cancellationToken = default)
        {
            // todo: Check the redundancy.
            var id = await _appService.CreateAsync(input, cancellationToken: cancellationToken);
            return id;
        }

        // todo: Permission
        [HttpPatch("{id:Guid}")]
        public async Task UpdateAsync([FromRoute] Guid id,
            [FromBody] JsonPatchDocument<ContactCardInputDto> data, CancellationToken cancellationToken = default)
        {
            // todo: Redundancy
            var a = await _appService.FindByIdAsync(id, true, cancellationToken);
            data.ApplyTo(a);
            // todo: Check the redundancy.
            await _appService.UpdateAsyc(id, a, cancellationToken:cancellationToken);
        }


        [HttpGet("search")]
        public async Task<PagedResultDto<ContactCardOutputDto>> SearchAsync([FromQuery] SearchContactCardDto input, CancellationToken cancellationToken = default)
        {
            var ret = await _appService.SearchAsync(input, cancellationToken);
            return ret;
        }
    }
}

