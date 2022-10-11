using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Auditing;

namespace PolpAbp.MultiTenancy.Dtos
{
    public class TenantAddOnOutputDto : TenantAddOnInputDto, IHasCreationTime, IEntityDto<Guid>, IHasModificationTime
    {
        public DateTime CreationTime { get; set; }

        public DateTime? LastModificationTime { get; set; }

        public Guid Id { get; set; }

        public List<TenantAddressOutputDto> Addresses { get; set; }
    }
}

