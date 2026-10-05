using Application.Assets.Dto;
using Application.Assets.FullDto;
using DataLayer.Repository;


namespace Services
{
    public class LoginInfoService(LoginInfoRepo _repo)
    {
        public async Task<DtoResult<FullLogin>> GetLogin()
        {
            DtoResult<FullLogin> rs = new();
            try
            {
                rs = await _repo.GetLogin();
            }
            catch (Exception)
            {

                throw;
            }
            return rs;
        }
        public async Task<DtoResult<DtoLoginInfo>> AddAsync(DtoLoginInfo dto)
        {
            DtoResult<DtoLoginInfo> rs = new();
            try
            {
                rs = await _repo.AddAsync(dto);
            }
            catch (Exception)
            {

                throw;
            }
            return rs;
        }
    }
}
