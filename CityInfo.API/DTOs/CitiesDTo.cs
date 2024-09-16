namespace CityInfo.API.DTOs
{
    public class CitiesDTo
    {
        public int Id { get; set; }
        public string name { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;

        public int CountOfIntrestPoints
        {
            get
            {
                return PointOfIntrests.Count();
            }
        }

        //navigation properties
        public ICollection<PointOfIntrestDTo> PointOfIntrests { get; set; }
        = new List<PointOfIntrestDTo>();
    }
}
