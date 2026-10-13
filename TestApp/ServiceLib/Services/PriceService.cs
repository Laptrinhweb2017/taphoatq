using Application.Asset.Dtos;
using Application.Asset.Interfaces;

namespace ServiceLib.Services
{
	public class PriceService(IRepository<DtoPrice> _repo) : IService<DtoPrice>
	{
		public async Task<DtoResult<DtoPrice>> AddAsync(DtoPrice dto)
		{
			DtoResult<DtoPrice> result = new();
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
		public async Task<DtoResult<DtoPrice>> DeleteAsync(DtoPrice dto)
		{
			DtoResult<DtoPrice> result = new();
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
		public async Task<DtoResult<DtoPrice>> DeleteAsync(object Id)
		{
			DtoResult<DtoPrice> result = new();
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
		public async Task<DtoResult<DtoPrice>> FindAsync(DtoPrice dto)
		{
			DtoResult<DtoPrice> result = new();
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
		public async Task<DtoResult<DtoPrice>> GetAllAsync()
		{
			DtoResult<DtoPrice> result = new();
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
		public async Task<DtoResult<DtoPrice>> GetOneAsync(DtoPrice dto)
		{
			DtoResult<DtoPrice> result = new();
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
		public async Task<DtoResult<DtoPrice>> UpdateAsync(DtoPrice dto)
		{
			DtoResult<DtoPrice> result = new();
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