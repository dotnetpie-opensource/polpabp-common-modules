﻿using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Auditing;

namespace PolpAbp.Contact.Dtos
{
    public class AddressOutputDto : AddressInputDto, IHasCreationTime, IEntityDto<Guid>
    {
        public Guid Id { get; set; }
        public DateTime CreationTime { get; set; }
    }
}

