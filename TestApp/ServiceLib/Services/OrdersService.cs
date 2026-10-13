using Application.Asset.Dtos;
using Application.Asset.Interfaces;

namespace ServiceLib.Services
{
	public class OrdersService(IRepository<DtoOrders> _repo) : IService<DtoOrders>
	{
		public async Task<DtoResult<DtoOrders>> AddAsync(DtoOrders dto)
		{
			DtoResult<DtoOrders> result = new();
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
		public async Task<DtoResult<DtoOrders>> DeleteAsync(DtoOrders dto)
		{
			DtoResult<DtoOrders> result = new();
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
		public async Task<DtoResult<DtoOrders>> DeleteAsync(object Id)
		{
			DtoResult<DtoOrders> result = new();
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
		public async Task<DtoResult<DtoOrders>> FindAsync(DtoOrders dto)
		{
			DtoResult<DtoOrders> result = new();
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
		public async Task<DtoResult<DtoOrders>> GetAllAsync()
		{
			DtoResult<DtoOrders> result = new();
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
		public async Task<DtoResult<DtoOrders>> GetOneAsync(DtoOrders dto)
		{
			DtoResult<DtoOrders> result = new();
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
		public async Task<DtoResult<DtoOrders>> UpdateAsync(DtoOrders dto)
		{
			DtoResult<DtoOrders> result = new();
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