using System;
using PolpAbp.Contact.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;

namespace PolpAbp.Contact
{
	public abstract class ContactController : AbpControllerBase
    {
        protected ContactController()
        {
            LocalizationResource = typeof(ContactResource);
        }
    }
}

