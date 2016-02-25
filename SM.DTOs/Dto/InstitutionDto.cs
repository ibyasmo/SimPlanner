using SM.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace SM.Dto
{
    [MetadataType(typeof(InstitutionMetadata))]
    public class InstitutionDto
	{
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        public string CountryCode { get; set; }
        public CountryDto Country { get; set; }
        public ICollection<DepartmentDto> Departments { get; set; }

    }
}
