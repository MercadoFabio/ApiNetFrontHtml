using Parcial.Dtos;

namespace Parcial.Servicios.Interfaces
{
    public interface IParcialService
    {
        Task<BaseResponse<List<ObraDto>>> GetObrasAsync();
        Task<BaseResponse<AlbanilXObraDto>> PostAlbanilXObraAsync(AlbanilXObraDto albanilXObraDto);
        Task<BaseResponse<AlbanilDto>> PostAlbanilAsync(AlbanilDto albanilDto);
        Task<BaseResponse<List<AlbanilDto>>> GetAlbanilesNotObraAsync(Guid obraId);
    }
}
