using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace OdeToFood.Data
{
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

        public Restaurant Delete(int id)
        {
            Restaurant restaurant = restaurants.FirstOrDefault(x => x.Id == id);
            if(restaurant != null)
            {
                restaurants.Remove(restaurant);
            }

            return restaurant;
        }

        public int GetCountOfRestaurants()
        {
            return restaurants.Count;
        }
    }
}
