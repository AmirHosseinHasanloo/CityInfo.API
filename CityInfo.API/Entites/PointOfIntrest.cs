using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CityInfo.API.Entites
{
    public class PointOfIntrest
    {
        public PointOfIntrest(string name)
        {
            this.Name = name;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }

        //navigation property
        [ForeignKey("CityId")]
        public City? City { get; set; }
        public int CityId { get; set; }
    }
}
