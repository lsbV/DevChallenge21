using System.Collections.Immutable;
using AutoMapper;
using Core;
using MainServer.Controllers;
using Microsoft.IdentityModel.Tokens;

namespace MainServer.Infrastructure.AutoMapperProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateCategoryRequestDTO, Category>()
                .ForMember(dest => dest.Points, opt => opt.Ignore())
                .ConstructUsing(request => new Category(
                    new CategoryId(0),
                    new Title(request.Title),
                    request.Points.Select(p => new Point(p)).ToImmutableHashSet()
                ));

            CreateMap<Category, CategoryResponseDTO>()
                .ForMember(dest => dest.Points, opt => opt.MapFrom(src => src.Points.Select(p => p.Value).ToArray()))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title.Value));

        }
    }
}
