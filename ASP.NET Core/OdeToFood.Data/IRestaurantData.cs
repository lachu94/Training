using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace OdeToFood.Data
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetAll();
       
    }
    public class InMemoryRestaruantData : IRestaurantData
    {
        readonly List<Restaurant> restaurants;
        public InMemoryRestaruantData()
        {
            restaurants = new List<Restaurant>()
            {
                new Restaurant {Id =1 , Name = "Stott's PIzza", Location = "Maryland", Cuisine = CuisineType.Italian },
                new Restaurant {Id =2 , Name = "Cinamon Club", Location = "Maryland", Cuisine = CuisineType.None },
                new Restaurant {Id =3 , Name = "La Costa", Location = "Maryland", Cuisine = CuisineType.Mexican }
            };
        }
        public IEnumerable<Restaurant> GetAll() => from r in restaurants
                                                   orderby r.Name
                                                   select r;
    }
}
