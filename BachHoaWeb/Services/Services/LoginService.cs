using Application.Assets.Dtos;
using ModelLib.Repositories;
using Application.Assets.FullDto;

namespace ServiceLib.Services
{
    public class LoginService(LoginRepo repo)
    {
        public async Task<DtoResult<FullLogin>> GetLogin(string StaffID)
        {
            DtoResult<FullLogin> result = new();
            try
            {
                result = await repo.GetLogin(StaffID);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
