using Application.Asset.Dtos;
using Application.Asset.Interfaces;

namespace ServiceLib.Services
{
	public class LoginInfoService(IRepository<DtoLoginInfo> _repo) : IService<DtoLoginInfo>
	{
		public async Task<DtoResult<DtoLoginInfo>> AddAsync(DtoLoginInfo dto)
		{
			DtoResult<DtoLoginInfo> result = new();
			try
			{
				result = await _repo.Add(dto);
			}
			catch (Exception ex)
			{
				result.Message = ex.Message;
			}
			return result;
		}
		public async Task<DtoResult<DtoLoginInfo>> DeleteAsync(DtoLoginInfo dto)
		{
			DtoResult<DtoLoginInfo> result = new();
			try
			{
				result = await _repo.Delete(dto);
			}
			catch (Exception ex)
			{
				result.Message = ex.Message;
			}
			return result;
		}
		public async Task<DtoResult<DtoLoginInfo>> DeleteAsync(object Id)
		{
			DtoResult<DtoLoginInfo> result = new();
			try
			{
				result = await _repo.Delete(Id);
			}
			catch (Exception ex)
			{
				result.Message = ex.Message;
			}
			return result;
		}
		public async Task<DtoResult<DtoLoginInfo>> FindAsync(DtoLoginInfo dto)
		{
			DtoResult<DtoLoginInfo> result = new();
			try
			{
				result = await _repo.Find(dto);
			}
			catch (Exception ex)
			{
				result.Message = ex.Message;
			}
			return result;
		}
		public async Task<DtoResult<DtoLoginInfo>> GetAllAsync()
		{
			DtoResult<DtoLoginInfo> result = new();
			try
			{
				result = await _repo.GetAll();
			}
			catch (Exception ex)
			{
				result.Message = ex.Message;
			}
			return result;
		}
		public async Task<DtoResult<DtoLoginInfo>> GetOneAsync(DtoLoginInfo dto)
		{
			DtoResult<DtoLoginInfo> result = new();
			try
			{
				result = await _repo.GetOne(dto);
			}
			catch (Exception ex)
			{
				result.Message = ex.Message;
			}
			return result;
		}

        public Task<DtoResult<DtoLoginInfo>> SaveAsync(DtoLoginInfo dto)
        {
            throw new NotImplementedException();
        }

        public async Task<DtoResult<DtoLoginInfo>> UpdateAsync(DtoLoginInfo dto)
		{
			DtoResult<DtoLoginInfo> result = new();
			try
			{
				result = await _repo.Update(dto);
			}
			catch (Exception ex)
			{
				result.Message = ex.Message;
			}
			return result;
		}
	}
}