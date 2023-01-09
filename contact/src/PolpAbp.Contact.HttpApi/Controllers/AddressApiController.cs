using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PolpAbp.Contact.Dtos;
using PolpAbp.Contact.Services;

namespace PolpAbp.Contact.Controllers
{
    [Authorize]
    [Route("api/contact/address")]
    public class AddressApiController : ContactController
    {
        private readonly IAddressAppService _addressAppService;

        public AddressApiController(IAddressAppService addressAppService)
        {
            _addressAppService = addressAppService;
        }

        [HttpGet("{id:Guid}")]
        public async Task<AddressOutputDto> LoadAddressByIdAsync([FromRoute] Guid id)
        {
            var ret = await _addressAppService.FindByIdAsync(id);
            return ret;
        }

        // todo: Permission
        [HttpPost]
        public async Task<Guid> CreateAsync([FromBody] AddressInputDto input)
        {
            // todo: Check the redundancy.
            var id = await _addressAppService.CreateAsync(input);
            return id;
        }

        // todo: Permission
        [HttpPatch("{id:Guid}")]
        public async Task UpdateAsync([FromRoute] Guid id,
            [FromBody] JsonPatchDocument<AddressInputDto> data)
        {
            // todo: Redundancy

            var a = await _addressAppService.FindByIdAsync(id);
            data.ApplyTo(a);
            // todo: Check the redundancy.
            await _addressAppService.UpdateAsyc(id, a);
        }

    }
}

