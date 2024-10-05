using System.Collections.Immutable;
using AutoMapper;
using Core;
using Database.DbModels;

namespace TopicComponent;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Topic, DbTopic>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title.Value))
            .ForMember(dest => dest.Points, opt => opt.Ignore())
            .ForMember(dest => dest.CallId, opt => opt.MapFrom(src => src.CallId.Value))
            .AfterMap((c, dbC) => dbC.Points = c.Points.Select(p => p.Value).ToHashSet());

        CreateMap<DbTopic, Topic>()
            .ForMember(dest => dest.Points, opt => opt.Ignore())
            .ConstructUsing(dbTopic =>
                new Topic(
                    new TopicId(dbTopic.Id),
                    new Title(dbTopic.Title),
                    dbTopic.Points.Select(p => new Point(p)).ToImmutableHashSet(),
                    new CallId(dbTopic.CallId)
                ));

    }
}