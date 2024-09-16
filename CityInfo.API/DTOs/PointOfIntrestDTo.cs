namespace CityInfo.API.DTOs
{
    public class PointOfIntrestDTo
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        //navigation properties

        public CitiesDTo CitiesDTo { get; set; }
    }
}
