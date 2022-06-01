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
    public class AddCarCommand : IRequest<CarDTO>
    {
        public CarDTO Car { get; set; }

        public AddCarCommand(CarDTO car)
        {
            Car = car;
        }
    }

    public class AddCarCommandHandler : IRequestHandler<AddCarCommand, CarDTO>
    {
        private readonly ICarRepository carRepository;
        private readonly IMapper mapper;

        public AddCarCommandHandler(ICarRepository carRepository, IMapper mapper)
        {
            this.carRepository = carRepository;
            this.mapper = mapper;
        }

        public async Task<CarDTO> Handle(AddCarCommand request, CancellationToken cancellationToken)
        {
            var validator = new AddAndUpdateCarValidator();

            var validationResult = validator.Validate(mapper.Map<Car>(request.Car));

            if (!validationResult.IsValid)
            {
                throw new Exception();
            }

            var result = await carRepository.AddCar(mapper.Map<Car>(request.Car));


            return mapper.Map<CarDTO>(result);
        }
    }
}
