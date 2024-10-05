using System.Collections.Immutable;
using AutoMapper;
using Core;
using MainServer.Models;
using Microsoft.IdentityModel.Tokens;

namespace MainServer.Infrastructure.AutoMapperProfiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CreateTopicRequestDto, Topic>()
            .ForMember(dest => dest.Points, opt => opt.Ignore())
            .ConstructUsing(request => new Topic(
                new TopicId(0),
                new Title(request.Title),
                request.Points.Select(p => new Point(p)).ToImmutableHashSet(),
                CallId.Default
            ));

        CreateMap<Topic, TopicResponseDto>()
            .ForMember(dest => dest.Points, opt => opt.MapFrom(src => src.Points.Select(p => p.Value).ToArray()))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title.Value));


        CreateMap<Call, CallResponseDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
            .ForMember(dest => dest.EmotionTone, opt => opt.MapFrom(src => src.Tone.ToFriendlyString()))
            .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Transcription.Text))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => string.Join(", ", src.People.Select(p => p.Name))))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => string.Join(", ", src.Locations.Select(l => l.Address))))
            .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Topics.Select(t => t.Title).ToArray()));
    }
}
