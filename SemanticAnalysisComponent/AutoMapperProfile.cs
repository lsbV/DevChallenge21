using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core;

namespace SemanticAnalysisComponent;

internal class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<AnalysisReportDto, Call>()
            .ForMember(dest => dest.Transcription, opt => opt.MapFrom(src => src.Transcription))
            .ForMember(dest => dest.Tone, opt => opt.MapFrom(src => src.Tone))
            .ForMember(dest => dest.People, opt => opt.Ignore())
            .ForMember(dest => dest.Locations, opt => opt.Ignore())
            .ForMember(dest => dest.Topics, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.Ignore())
            .ConstructUsing((src, context) =>
                new Call(
                    context.Items[nameof(CallId)] as CallId ?? throw new ArgumentException(nameof(CallId)),
                    EmotionalToneExtensions.FromFriendlyString(src.Tone),
                    new Transcription(src.Transcription),
                    src.People.Select(n => new Person(n)).ToImmutableHashSet(),
                    src.Locations.Select(l => new Location(l)).ToImmutableHashSet(),
                    src.Topics.Select(c => new Core.Topic(
                        TopicId.Default,
                        new Title(c.Title),
                        c.Points.Select(p => new Point(p)).ToImmutableHashSet(),
                        context.Items[nameof(CallId)] as CallId ?? throw new ArgumentException(nameof(CallId)))
                    ).ToImmutableList(),
                    Status.Processing
                ));
    }
}