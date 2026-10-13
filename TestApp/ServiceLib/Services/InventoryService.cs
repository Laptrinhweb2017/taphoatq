using Application.Asset.Dtos;
using Application.Asset.Interfaces;

namespace ServiceLib.Services
{
	public class InventoryService(IRepository<DtoInventory> _repo) : IService<DtoInventory>
	{
		public async Task<DtoResult<DtoInventory>> AddAsync(DtoInventory dto)
		{
			DtoResult<DtoInventory> result = new();
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
		public async Task<DtoResult<DtoInventory>> DeleteAsync(DtoInventory dto)
		{
			DtoResult<DtoInventory> result = new();
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
		public async Task<DtoResult<DtoInventory>> DeleteAsync(object Id)
		{
			DtoResult<DtoInventory> result = new();
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
		public async Task<DtoResult<DtoInventory>> FindAsync(DtoInventory dto)
		{
			DtoResult<DtoInventory> result = new();
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
		public async Task<DtoResult<DtoInventory>> GetAllAsync()
		{
			DtoResult<DtoInventory> result = new();
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
		public async Task<DtoResult<DtoInventory>> GetOneAsync(DtoInventory dto)
		{
			DtoResult<DtoInventory> result = new();
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
		public async Task<DtoResult<DtoInventory>> UpdateAsync(DtoInventory dto)
		{
			DtoResult<DtoInventory> result = new();
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