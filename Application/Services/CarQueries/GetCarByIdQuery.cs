using APIdemo.Models;
using APIdemo.Repositories;
using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectAPI.Services.CarQueries
{
    public class GetCarByIdQuery : IRequest<CarDTO>
    {
        public int Id { get; set; }

        public GetCarByIdQuery(int id)
        {
            Id = id;
        }
    }

    public class GetCarByIdQueryHandler : IRequestHandler<GetCarByIdQuery, CarDTO>
    {
        private readonly ICarRepository carRepository;
        private readonly IMapper mapper;

        public GetCarByIdQueryHandler(IMapper mapper, ICarRepository carRepository)
        {
            this.mapper = mapper;
            this.carRepository = carRepository;
        }

        public async Task<CarDTO> Handle(GetCarByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await carRepository.GetCarById(request.Id);

            if (result == null)
            {
                throw new Exception($"Car is not found by id {request.Id}.");
            }

            return mapper.Map<CarDTO>(result);
        }
    }
}
