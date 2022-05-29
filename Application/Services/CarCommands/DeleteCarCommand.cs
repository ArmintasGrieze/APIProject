using APIdemo.Models;
using APIdemo.Repositories;
using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectAPI.Services.CarCommands
{
    public class DeleteCarCommand : IRequest<CarDTO>
    {
        public int Id { get; set; }

        public DeleteCarCommand(int id)
        {
            Id = id;
        }
    }

    public class DeleteCarCommandHandler : IRequestHandler<DeleteCarCommand, CarDTO>
    {
        private readonly ICarRepository carRepository;
        private readonly IMapper mapper;

        public DeleteCarCommandHandler(ICarRepository carRepository, IMapper mapper)
        {
            this.carRepository = carRepository;
            this.mapper = mapper;
        }

        public async Task<CarDTO> Handle(DeleteCarCommand request, CancellationToken cancellationToken)
        {
            var result = await carRepository.DeleteCar(request.Id);

            if (result == null)
            {
                throw new Exception($"Car is not found by id {request.Id}.");
            }

            return mapper.Map<CarDTO>(result);
        }
    }
}
