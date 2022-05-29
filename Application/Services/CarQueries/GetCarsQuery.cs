using APIdemo.Models;
using APIdemo.Repositories;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectAPI.Services.CarQueries
{
    public class GetCarsQuery : IRequest<List<CarDTO>>
    {

    }

    public class GetCarsQueryHandler : IRequestHandler<GetCarsQuery, List<CarDTO>>
    {
        private readonly ICarRepository carRepository;
        private readonly IMapper mapper;

        public GetCarsQueryHandler(IMapper mapper, ICarRepository carRepository)
        {
            this.mapper = mapper;
            this.carRepository = carRepository;
        }

        public async Task<List<CarDTO>> Handle(GetCarsQuery request, CancellationToken cancellationToken)
        {
            var result = await carRepository.GetAll();

            return mapper.Map<List<CarDTO>>(result);
        }
    }
}
