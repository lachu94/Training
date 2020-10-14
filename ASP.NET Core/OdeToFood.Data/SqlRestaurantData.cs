using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace OdeToFood.Data
{
    public class SqlRestaurantData : IRestaurantData
    {
        private readonly OdeToFoodDbContext db;
        public SqlRestaurantData(OdeToFoodDbContext db)
        {
            this.db = db;
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            db.Add(newRestaurant);

            return newRestaurant;
        }

        public int Commit()
        {
            db.SaveChanges();
            return 0;
        }

        public Restaurant GetById(int id)
        {
            return db.Restaurants.Find(id);
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name) => from r in db.Restaurants
                                                                            where string.IsNullOrEmpty(name) || r.Name.StartsWith(name)
                                                                            orderby r.Name
                                                                            select r;
      

        public Restaurant Delete(int id)
        {
            Restaurant restaurant = GetById(id);
            if (restaurant != null)
            {
                db.Restaurants.Remove(restaurant);
            }

            return restaurant;
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var enity = db.Restaurants.Attach(updatedRestaurant);
            enity.State = EntityState.Modified;
            return updatedRestaurant;
        }

        public int GetCountOfRestaurants()
        {
            return db.Restaurants.Count();
        }
    }
}
