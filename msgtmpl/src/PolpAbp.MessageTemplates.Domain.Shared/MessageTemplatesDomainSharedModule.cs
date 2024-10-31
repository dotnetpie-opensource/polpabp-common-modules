using System;
using Volo.Abp.Modularity;
using Volo.Abp.Validation;

namespace PolpAbp.MessageTemplates;

[DependsOn(
    typeof(AbpValidationModule)
)]
public class MessageTemplatesDomainSharedModule : AbpModule
{

}
