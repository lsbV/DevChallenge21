using System;
using System.Collections.Immutable;
using AutoMapper;
using Core;
using Database;
using Database.DbModels;

namespace CallComponent
{
    class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<DbCall, Call>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => new CallId(src.Id)))
                .ForMember(dest => dest.Topics, opt => opt.Ignore())
                .ForMember(dest => dest.Locations, opt => opt.Ignore())
                .ForMember(dest => dest.People, opt => opt.Ignore())
                .ConstructUsing((src, context) =>
                    new Call(
                        new CallId(src.Id),
                        src.Tone,
                        new Transcription(src.Transcription),
                        src.People.Select(p => new Person(p)).ToImmutableHashSet(),
                        src.Locations.Select(l => new Location(l)).ToImmutableHashSet(),
                        src.Topics.Select(t => new Topic(
                            new TopicId(t.Id),
                            new Title(t.Title),
                            t.Points.Select(p => new Point(p)).ToImmutableHashSet(),
                            new CallId(src.Id))
                        ).ToImmutableList(),
                        src.Status

                    ));

            CreateMap<Call, DbCall>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
                .ForMember(dest => dest.Tone, opt => opt.MapFrom(src => src.Tone))
                .ForMember(dest => dest.Transcription, opt => opt.MapFrom(src => src.Transcription.Text))
                .ForMember(dest => dest.People, opt => opt.MapFrom(src => src.People.Select(p => p.Name).ToHashSet()))
                .ForMember(dest => dest.Locations, opt => opt.MapFrom(src => src.Locations.Select(l => l.Address).ToHashSet()))
                .ForMember(dest => dest.Topics, opt => opt.MapFrom(src => src.Topics.Select(t => new DbTopic
                {
                    Id = t.Id.Value,
                    Title = t.Title.Value,
                    Points = t.Points.Select(p => p.Value).ToHashSet(),
                    CallId = src.Id.Value
                }).ToList()));

        }

    }
}
