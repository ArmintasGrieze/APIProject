using APIdemo.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIdemo.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly AppDbContext context;

        public CarRepository(AppDbContext context)
        {
            this.context = context;
        } 

        public async Task<Car> AddCar(Car car)
        {
            this.context.Cars.Add(car);

            await context.SaveChangesAsync();

            return car;
        }

        public async Task<Car> DeleteCar(int id)
        {
            var result = await this.context.Cars.FirstOrDefaultAsync(x => x.Id == id);

            if (result == null)
            {
                return null;
            }

            this.context.Remove(result);

            await this.context.SaveChangesAsync();

            return result;
        }

        public async Task<List<Car>> GetAll()
        {
            return await this.context.Cars.ToListAsync();
        }

        public async Task<Car> GetCarById(int id)
        {
            var result = await this.context.Cars.FirstOrDefaultAsync(x => x.Id == id);

            if (result == null)
            {
                return null;
            }

            return result;
        }

        public async Task<Car> UpdateCar(Car car, int id)
        {
            var result = await this.context.Cars.FirstOrDefaultAsync(x => x.Id == id);

            if (result == null)
            {
                return null;
            }

            result.FuelType = car.FuelType; 
            result.ReleaseYear = car.ReleaseYear;
            result.Color = car.Color;
            result.Model = car.Model;
            result.Name = car.Name;

            await this.context.SaveChangesAsync();

            return result;
        }
    }
}