using Application.Asset.Dtos;
using Application.Asset.Interfaces;

namespace ServiceLib.Services
{
	public class OrderStatusService(IRepository<DtoOrderStatus> _repo) : IService<DtoOrderStatus>
	{
		public async Task<DtoResult<DtoOrderStatus>> AddAsync(DtoOrderStatus dto)
		{
			DtoResult<DtoOrderStatus> result = new();
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
		public async Task<DtoResult<DtoOrderStatus>> DeleteAsync(DtoOrderStatus dto)
		{
			DtoResult<DtoOrderStatus> result = new();
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
		public async Task<DtoResult<DtoOrderStatus>> DeleteAsync(object Id)
		{
			DtoResult<DtoOrderStatus> result = new();
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
		public async Task<DtoResult<DtoOrderStatus>> FindAsync(DtoOrderStatus dto)
		{
			DtoResult<DtoOrderStatus> result = new();
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
		public async Task<DtoResult<DtoOrderStatus>> GetAllAsync()
		{
			DtoResult<DtoOrderStatus> result = new();
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
		public async Task<DtoResult<DtoOrderStatus>> GetOneAsync(DtoOrderStatus dto)
		{
			DtoResult<DtoOrderStatus> result = new();
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

        public Task<DtoResult<DtoOrderStatus>> SaveAsync(DtoOrderStatus dto)
        {
            throw new NotImplementedException();
        }

        public async Task<DtoResult<DtoOrderStatus>> UpdateAsync(DtoOrderStatus dto)
		{
			DtoResult<DtoOrderStatus> result = new();
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