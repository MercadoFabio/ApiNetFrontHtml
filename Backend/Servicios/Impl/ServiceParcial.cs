using AutoMapper;
using Parcial.Dtos;
using Parcial.Models;
using Parcial.Repositories.Interfaces;
using Parcial.Servicios.Interfaces;
using Parcial.Validators;

namespace Parcial.Servicios.Impl
{
    public class ServiceParcial : IParcialService
    {
        private readonly IAlbanilRepository _albanilRepository;
        private readonly IObraRepository _obraRepository;
        private readonly IMapper _mapper;
        private readonly AlbanileXObraValidator _albanilXObraValidador;
        private readonly AlbanilValidator _albanilValidador;

        public ServiceParcial(AlbanilValidator albanilValidador
            ,IObraRepository obraRepository
            , IAlbanilRepository albanilRepository
            ,IMapper mapper
            ,AlbanileXObraValidator albanilXObraValidador)
        {

            _albanilValidador = albanilValidador;
            _obraRepository = obraRepository;
            _mapper = mapper;
            _albanilXObraValidador = albanilXObraValidador;
            _albanilRepository = albanilRepository;
        }
        public async Task<BaseResponse<List<AlbanilDto>>> GetAlbanilesNotObraAsync(Guid obraId)
        {
            var response = new BaseResponse<List<AlbanilDto>>();
            try
            {
                var albaniles = await _obraRepository.GetAbanilesNotInObra(obraId);
                var albanilesDto = _mapper.Map<List<AlbanilDto>>(albaniles);
                response.Data = albanilesDto;
                response.Success = true;
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "Error al obtener los albañiles.";
                throw;
            }
            return response;
        }

        public async Task<BaseResponse<List<ObraDto>>> GetObrasAsync()
        {
            var response = new BaseResponse<List<ObraDto>>();
            try
            {
                var obras = await _obraRepository.GetActiveObras();
                var obrasDto = _mapper.Map<List<ObraDto>>(obras);
                response.Data = obrasDto;
                response.Success = true;
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "Error al obtener las obras.";
                throw;
            }
            return response;
        }

        public async Task<BaseResponse<AlbanilDto>> PostAlbanilAsync(AlbanilDto albanilDto)
        {
            var response = new BaseResponse<AlbanilDto>();
            var validacion = await _albanilValidador.ValidateAsync(albanilDto);
            if (!validacion.IsValid)
            {
                var errorMessage = string.Join(", ", 
                    validacion.Errors.Select(x => x.ErrorMessage));
                response.Success = false;
                response.Message = errorMessage;
                return response;
            }
            try
            {
                var existAlbanile = await _albanilRepository.AlbanilExists(albanilDto.Dni);
               
                if (existAlbanile)
                {
                    response.Success = false;
                    response.Message = "El albañil ya está registrado.";
                    return response;
                }

                var albanil = _mapper.Map<Albanile>(albanilDto);
                albanil.Id = Guid.NewGuid();
                albanil.Activo = true;
                albanil.FechaAlta = DateTime.Now;

                await _albanilRepository.AddAlbanil(albanil);
                response.Success = true;
                response.Message = "El albañil se agrego con éxito.";

            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "Error al añadir el albañil.";
                throw;
            }
            return response;
        }

        public async Task<BaseResponse<AlbanilXObraDto>> PostAlbanilXObraAsync(AlbanilXObraDto albanilXObraDto)
        {
            var response = new BaseResponse<AlbanilXObraDto>();
            var validacion = await _albanilXObraValidador.ValidateAsync(albanilXObraDto);

            if (!validacion.IsValid)
            {
                var errorMessage = string.Join(", ",
                    validacion.Errors.Select(x => x.ErrorMessage));
                response.Success = false;
                response.Message = errorMessage;
                return response;
            }
            try
            {
                var albanil = await _albanilRepository.GetAlbanilById(albanilXObraDto.IdAlbanil);
                if (albanil == null || !albanil.Activo)
                {
                    response.Success = false;
                    response.Message = "El albañil no se encontró o esta inactivo.";
                    return response;
                }

                var albanilInObra = await _obraRepository
                    .GetAlbanilInObra(albanilXObraDto.IdObra, albanilXObraDto.IdAlbanil);
                if (albanilInObra)
                {
                    response.Success = false;
                    response.Message = "El albañil ya forma parte de la obra.";
                    return response;
                }
                var albanilXObra = _mapper.Map<AlbanilesXObra>(albanilXObraDto);
                albanilXObra.Id = Guid.NewGuid();
                albanilXObra.FechaAlta = DateTime.Now;

                await _obraRepository.AddAlbanilToObra(albanilXObra);
                response.Success = true;
                response.Message = "El albañil fue agregado con éxito a la obra.";


            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "Error al añadir el albañil a la obra.";
                throw;
            }
            return response;
        }
    }
}
