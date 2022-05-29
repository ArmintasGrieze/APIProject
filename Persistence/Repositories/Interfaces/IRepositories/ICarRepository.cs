using APIdemo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIdemo.Repositories
{
    public interface ICarRepository
    {
        Task<List<Car>> GetAll();

        Task<Car> GetCarById(int id);

        Task<Car> AddCar(Car car);

        Task<Car> UpdateCar(Car car, int id);

        Task<Car> DeleteCar(int id);
    }
}
