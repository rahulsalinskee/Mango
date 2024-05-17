using Mango.Web.Models.CommonDTOs;

namespace Mango.Web.Services.IService
{
    public interface IBaseService
    {
        Task<ResponseDto> SendAsync(RequestDto requestDto, bool isBearerTokenIncluded = true);
    }
}
