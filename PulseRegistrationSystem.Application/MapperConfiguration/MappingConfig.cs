using AutoMapper;
using PulseRegistrationSystem.Application.DTOs.Request;
using PulseRegistrationSystem.Application.DTOs.Response;
using PulseRegistrationSystem.Domain.Entities;

namespace PulseRegistrationSystem.Application.MapperConfiguration;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<UsuarioRequestDto, Usuario>();
        CreateMap<Usuario, UsuarioResponseDto>();
        CreateMap<Login, LoginResponseDto>()
            .ForMember(dest => dest.NomeUsuario, opt => opt.MapFrom(src => src.Usuario.Nome))
            .ForMember(dest => dest.UsuarioId, opt => opt.MapFrom(src => src.Usuario.Id))
            .ForMember(dest => dest.TentativasLogin, opt => opt.MapFrom(src => src.TentativasLogin));
 
 
        CreateMap<LoginRequestDto, Login>();
    }
}