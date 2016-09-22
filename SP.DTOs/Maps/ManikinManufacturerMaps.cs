using SP.DataAccess;

{
    internal class ManikinManufacturerMaps: DomainDtoMap<ManikinManufacturer, ManikinManufacturerDto>
    {
        public ManikinManufacturerMaps() : base(m => new ManikinManufacturer
            {
                Id = m.Id,
                Name = m.Name
            },
            m => new ManikinManufacturerDto
            {
                Id = m.Id,
                Name = m.Name
            })
        { }
    }
}