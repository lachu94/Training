using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace OdeToFood.Data
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetRestaurantsByName(string name);
        Restaurant GetById(int id);
        Restaurant Update(Restaurant updatedRestaurant);
        Restaurant Add(Restaurant newRestaurant);
        int Commit();

    }
    public class InMemoryRestaruantData : IRestaurantData
    {
        readonly List<Restaurant> restaurants;
        public InMemoryRestaruantData()
        {
            restaurants = new List<Restaurant>()
            {
                new Restaurant {Id =1 , Name = "Scott's Pizza", Location = "Maryland", Cuisine = CuisineType.Italian },
                new Restaurant {Id =2 , Name = "Cinamon Club", Location = "London", Cuisine = CuisineType.None },
                new Restaurant {Id =3 , Name = "La Costa", Location = "California", Cuisine = CuisineType.Mexican }
            };
        }

        public Restaurant GetById(int id)
        {
            return restaurants.SingleOrDefault(r => r.Id == id);
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name) => from r in restaurants
                                                                            where string.IsNullOrEmpty(name) || r.Name.StartsWith(name)
                                                                            orderby r.Name
                                                                            select r;

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            Restaurant restaurant = restaurants.SingleOrDefault(x => x.Id == updatedRestaurant.Id);
            if (restaurant != null)
            {
                restaurant.Name = updatedRestaurant.Name;
                restaurant.Location = updatedRestaurant.Location;
                restaurant.Cuisine = updatedRestaurant.Cuisine;
            }

            return restaurant;
        }

        public int Commit()
        {
            return 0;
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            restaurants.Add(newRestaurant);
            newRestaurant.Id = restaurants.Max(restaurants => restaurants.Id) + 1;
            return newRestaurant;
        }
    }
}
