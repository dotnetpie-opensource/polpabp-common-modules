using System;
using Volo.Abp.Application.Dtos;

namespace PolpAbp.Contact.Dtos
{
	public class SearchAddressDto : PagedAndSortedResultRequestDto
    {
		public string Keyword { get; set; }
		public int RoleId { get; set; }
	}
}
