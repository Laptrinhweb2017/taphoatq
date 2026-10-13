using Application.Asset.Dtos;
using Application.Assets.FullDto;
using ModelLib.Repositories;

namespace ServiceLib.Services
{
    public class LoginService(LoginRepo repo)
    {
        public async Task<DtoResult<FullLogin>> GetAllLogin()
        {
            return await repo.GetAllLogin();
        }
    }
}
