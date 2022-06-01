using APIdemo.Models;
using APIdemo.Repositories;
using AutoMapper;
using MediatR;
using ProjectAPI.Validators;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectAPI.Services.CarCommands
{
    public class UpdateCarCommand : IRequest<CarDTO>
    {
        public CarDTO Car { get; set; }

        public int Id { get; set; }

        public UpdateCarCommand(CarDTO car, int id)
        {
            Car = car;
            Id = id;
        }
    }

    public class UpdateCarCommandHandler : IRequestHandler<UpdateCarCommand, CarDTO>
    {
        private readonly ICarRepository carRepository;
        private readonly IMapper mapper;

        public UpdateCarCommandHandler(ICarRepository carRepository, IMapper mapper)
        {
            this.carRepository = carRepository;
            this.mapper = mapper;
        }

        public async Task<CarDTO> Handle(UpdateCarCommand request, CancellationToken cancellationToken)
        {
            var validator = new AddAndUpdateCarValidator();

            var validationResult = validator.Validate(mapper.Map<Car>(request.Car));

            if (!validationResult.IsValid)
            {
                throw new Exception();
            }

            var result = await carRepository.UpdateCar(mapper.Map<Car>(request.Car), request.Id);

            return mapper.Map<CarDTO>(result);
        }
    }
}
