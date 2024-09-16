using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace CityInfo.API.Entites
{
    public class City
    {
        public City(string name)
        {
            this.Name = name;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        //navigation properties
        public ICollection<PointOfIntrest> PointOfIntrests { get; set; }
     = new List<PointOfIntrest>();
    }
}
