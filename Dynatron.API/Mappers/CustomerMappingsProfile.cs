using AutoMapper;
using Dynatron.API.DTO.Request;
using Dynatron.API.DTO.Response;
using Dynatron.API.Model;

namespace Dynatron.API.Mappers
{
    public class CustomerMappingsProfile : Profile
    {
        public CustomerMappingsProfile()
        {
            CreateMap<Customer, CustomerResponseDTO>()
                .ReverseMap();

            CreateMap<Customer, CustomerRequestDTO>()
                .ReverseMap();
                
        }
    }
}
