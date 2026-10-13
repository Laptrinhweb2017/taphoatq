using Application.Asset.Dtos;
using Application.Asset.Interfaces;

namespace ServiceLib.Services
{
	public class StoreInfoService(IRepository<DtoStoreInfo> _repo) : IService<DtoStoreInfo>
	{
		public async Task<DtoResult<DtoStoreInfo>> AddAsync(DtoStoreInfo dto)
		{
			DtoResult<DtoStoreInfo> result = new();
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
		public async Task<DtoResult<DtoStoreInfo>> DeleteAsync(DtoStoreInfo dto)
		{
			DtoResult<DtoStoreInfo> result = new();
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
		public async Task<DtoResult<DtoStoreInfo>> DeleteAsync(object Id)
		{
			DtoResult<DtoStoreInfo> result = new();
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
		public async Task<DtoResult<DtoStoreInfo>> FindAsync(DtoStoreInfo dto)
		{
			DtoResult<DtoStoreInfo> result = new();
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
		public async Task<DtoResult<DtoStoreInfo>> GetAllAsync()
		{
			DtoResult<DtoStoreInfo> result = new();
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
		public async Task<DtoResult<DtoStoreInfo>> GetOneAsync(DtoStoreInfo dto)
		{
			DtoResult<DtoStoreInfo> result = new();
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
		public async Task<DtoResult<DtoStoreInfo>> UpdateAsync(DtoStoreInfo dto)
		{
			DtoResult<DtoStoreInfo> result = new();
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