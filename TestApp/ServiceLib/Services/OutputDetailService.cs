using Application.Asset.Dtos;
using Application.Asset.Interfaces;

namespace ServiceLib.Services
{
	public class OutputDetailService(IRepository<DtoOutputDetail> _repo) : IService<DtoOutputDetail>
	{
		public async Task<DtoResult<DtoOutputDetail>> AddAsync(DtoOutputDetail dto)
		{
			DtoResult<DtoOutputDetail> result = new();
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
		public async Task<DtoResult<DtoOutputDetail>> DeleteAsync(DtoOutputDetail dto)
		{
			DtoResult<DtoOutputDetail> result = new();
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
		public async Task<DtoResult<DtoOutputDetail>> DeleteAsync(object Id)
		{
			DtoResult<DtoOutputDetail> result = new();
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
		public async Task<DtoResult<DtoOutputDetail>> FindAsync(DtoOutputDetail dto)
		{
			DtoResult<DtoOutputDetail> result = new();
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
		public async Task<DtoResult<DtoOutputDetail>> GetAllAsync()
		{
			DtoResult<DtoOutputDetail> result = new();
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
		public async Task<DtoResult<DtoOutputDetail>> GetOneAsync(DtoOutputDetail dto)
		{
			DtoResult<DtoOutputDetail> result = new();
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

        public Task<DtoResult<DtoOutputDetail>> SaveAsync(DtoOutputDetail dto)
        {
            throw new NotImplementedException();
        }

        public async Task<DtoResult<DtoOutputDetail>> UpdateAsync(DtoOutputDetail dto)
		{
			DtoResult<DtoOutputDetail> result = new();
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