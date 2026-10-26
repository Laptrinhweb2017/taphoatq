using Application.Asset.Dtos;
using Application.Asset.Interfaces;

namespace ServiceLib.Services
{
	public class OrderDetailService(IRepository<DtoOrderDetail> _repo) : IService<DtoOrderDetail>
	{
		public async Task<DtoResult<DtoOrderDetail>> AddAsync(DtoOrderDetail dto)
		{
			DtoResult<DtoOrderDetail> result = new();
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
		public async Task<DtoResult<DtoOrderDetail>> DeleteAsync(DtoOrderDetail dto)
		{
			DtoResult<DtoOrderDetail> result = new();
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
		public async Task<DtoResult<DtoOrderDetail>> DeleteAsync(object Id)
		{
			DtoResult<DtoOrderDetail> result = new();
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
		public async Task<DtoResult<DtoOrderDetail>> FindAsync(DtoOrderDetail dto)
		{
			DtoResult<DtoOrderDetail> result = new();
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
		public async Task<DtoResult<DtoOrderDetail>> GetAllAsync()
		{
			DtoResult<DtoOrderDetail> result = new();
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
		public async Task<DtoResult<DtoOrderDetail>> GetOneAsync(DtoOrderDetail dto)
		{
			DtoResult<DtoOrderDetail> result = new();
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

        public Task<DtoResult<DtoOrderDetail>> SaveAsync(DtoOrderDetail dto)
        {
            throw new NotImplementedException();
        }

        public async Task<DtoResult<DtoOrderDetail>> UpdateAsync(DtoOrderDetail dto)
		{
			DtoResult<DtoOrderDetail> result = new();
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