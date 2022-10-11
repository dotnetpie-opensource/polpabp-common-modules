using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Auditing;

namespace PolpAbp.MultiTenancy.Dtos
{
    public class TenantContactOutputDto : TenantContactInputDto, IHasCreationTime, IEntityDto<Guid>, IHasModificationTime
    {
        public DateTime CreationTime { get; set; }

        public DateTime? LastModificationTime { get; set; }

        public Guid Id { get; set; }
    }
}

