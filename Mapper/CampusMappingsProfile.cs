using AutoMapper;
using SimpleLAP.DTOs;
using SimpleLAP.Models;

namespace LAP.Application.Mappers;

public class CampusMappingsProfile : Profile
{
    public CampusMappingsProfile()
    {
        CreateMap<ParticipanteDTO, Participante>()
                .ReverseMap();
    }
}
