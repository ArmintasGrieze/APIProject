using APIdemo.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectAPI.Services.CarCommands;
using ProjectAPI.Services.CarQueries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIdemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly IMediator mediator;

        public CarController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<CarDTO>>> GetCars()
        {
            var cars = await this.mediator.Send(new GetCarsQuery());

            return Ok(cars);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CarDTO>> GetCarById(int id)
        {
            var result = await this.mediator.Send(new GetCarByIdQuery(id));

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<CarDTO>> AddCar([FromBody] CarDTO car)
        {
            var result = await this.mediator.Send(new AddCarCommand(car));

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CarDTO>> UpdateCar([FromBody] CarDTO car, int id)
        {
            var result = await this.mediator.Send(new UpdateCarCommand(car, id));

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CarDTO>> DeleteCar(int id)
        {
            var result = await this.mediator.Send(new DeleteCarCommand(id));

            return Ok(result);
        }
    }
}
