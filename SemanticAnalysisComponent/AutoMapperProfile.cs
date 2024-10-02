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
        CreateMap<AnalysisReportDto, AnalysisReport>()
            .ForMember(dest => dest.Transcription, opt => opt.MapFrom(src => src.Transcription))
            .ForMember(dest => dest.Tone, opt => opt.MapFrom(src => src.Tone))
            .ForMember(dest => dest.Names, opt => opt.Ignore())
            .ForMember(dest => dest.Locations, opt => opt.Ignore())
            .ForMember(dest => dest.Categories, opt => opt.Ignore())
            .ConstructUsing(src =>
                new AnalysisReport(
                    new Transcription(src.Transcription),
                    EmotionalToneExtensions.FromFriendlyString(src.Tone),
                    src.People.Select(n => new Name(n)).ToImmutableHashSet(),
                    src.Locations.Select(l => new Location(l)).ToImmutableHashSet(),
                    src.Categories.Select(c => new Core.Category(
                        CategoryId.Default,
                        new Title(c.Title),
                        c.Points.Select(p => new Point(p)).ToImmutableHashSet())
                    ).ToImmutableHashSet()
                ));
    }
}