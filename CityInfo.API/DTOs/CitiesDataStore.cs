namespace CityInfo.API.DTOs
{
    public class CitiesDataStore
    {
        // singleton design pattern
        public List<CitiesDTo> cities { get; set; }

        //  public static CitiesDataStore current { get; } = new CitiesDataStore();

        public CitiesDataStore()
        {
            cities = new List<CitiesDTo>()
            {
            new CitiesDTo { Id = 1, name = "karaj", description = "is my town",
                PointOfIntrests =new List<PointOfIntrestDTo>()
                {
                   new PointOfIntrestDTo(){Id=1,Name = "baragan",Description ="nice rocky place"},
                   new PointOfIntrestDTo(){Id=2,Name = "sad karaj",Description ="nice lake"},
                }
            },
            new CitiesDTo { Id = 2, name = "tehran", description = "is neer my town" ,
            PointOfIntrests = new List<PointOfIntrestDTo>()
            {
                new PointOfIntrestDTo(){Id=3,Name = "baragan",Description ="nice rocky place"},
                new PointOfIntrestDTo(){Id=4,Name = "sad karaj",Description ="nice lake"},
            }
            },
            new CitiesDTo { Id = 3, name = "tabriz", description = "is the cool town" ,
             PointOfIntrests = new List<PointOfIntrestDTo>()
            {
                new PointOfIntrestDTo(){Id=5,Name = "baragan",Description ="nice rocky place"},
                new PointOfIntrestDTo(){Id=6,Name = "sad karaj",Description ="nice lake"},
            }
            }
            };
        }
    }
}
