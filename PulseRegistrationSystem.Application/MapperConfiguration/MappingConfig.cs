using AutoMapper;
using PulseRegistrationSystem.Application.DTOs.Request;
using PulseRegistrationSystem.Application.DTOs.Response;
using PulseRegistrationSystem.Domain.Entities;

namespace PulseRegistrationSystem.Application.MapperConfiguration;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<EnderecoRequestDto, Endereco>();

        CreateMap<UsuarioRequestDto, Usuario>()
            .ConstructUsing((dto, context) => new Usuario(
                dto.Nome,
                dto.Cpf,
                dto.DataNascimento,
                context.Mapper.Map<Endereco>(dto.FilialMottu), // aqui usa o contexto do AutoMapper
                dto.Email,
                dto.Funcao,
                null
            ));


        CreateMap<EnderecoRequestDto, Endereco>();


        
        CreateMap<EnderecoRequestDto, Endereco>()
            .ConstructUsing(dto => new Endereco(
                dto.Rua, 
                dto.Complemento, 
                dto.Bairro, 
                dto.Cep, 
                dto.Cidade, 
                dto.Estado
            ));


        CreateMap<Usuario, UsuarioResponseDto>();
        CreateMap<Login, LoginResponseDto>()
            .ForMember(dest => dest.NomeUsuario, opt => opt.MapFrom(src => src.UsuarioId))
            .ForMember(dest => dest.TentativasLogin, opt => opt.MapFrom(src => src.TentativasLogin));
 
 
        CreateMap<LoginRequestDto, Login>();
    }
}