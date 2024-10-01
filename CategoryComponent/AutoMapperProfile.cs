using System.Collections.Immutable;
using AutoMapper;
using Core;
using Database.DbModels;

namespace CategoryComponent;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Category, DbCategory>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title.Value))
            .ForMember(dest => dest.Points, opt => opt.Ignore())
            .AfterMap((c, dbC) => dbC.Points = c.Points.Select(p => p.Value).ToHashSet());

        CreateMap<DbCategory, Category>()
            .ForMember(dest => dest.Points, opt => opt.Ignore())
            .ConstructUsing(dbCategory =>
                new Category(
                    new CategoryId(dbCategory.Id),
                    new Title(dbCategory.Title),
                    dbCategory.Points.Select(p => new Point(p)).ToImmutableHashSet()
                ));

    }
}