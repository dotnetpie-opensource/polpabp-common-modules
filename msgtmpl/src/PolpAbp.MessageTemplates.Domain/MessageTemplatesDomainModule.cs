using System;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace PolpAbp.MessageTemplates;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(MessageTemplatesDomainSharedModule)
)]
public class MessageTemplatesDomainModule : AbpModule
{

}
