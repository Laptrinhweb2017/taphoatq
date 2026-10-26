using Application.Asset.Dtos;
using Application.Asset.Interfaces;

namespace ServiceLib.Services
{
	public class InputDetailService(IRepository<DtoInputDetail> _repo) : IService<DtoInputDetail>
	{
		public async Task<DtoResult<DtoInputDetail>> AddAsync(DtoInputDetail dto)
		{
			DtoResult<DtoInputDetail> result = new();
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
		public async Task<DtoResult<DtoInputDetail>> DeleteAsync(DtoInputDetail dto)
		{
			DtoResult<DtoInputDetail> result = new();
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
		public async Task<DtoResult<DtoInputDetail>> DeleteAsync(object Id)
		{
			DtoResult<DtoInputDetail> result = new();
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
		public async Task<DtoResult<DtoInputDetail>> FindAsync(DtoInputDetail dto)
		{
			DtoResult<DtoInputDetail> result = new();
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
		public async Task<DtoResult<DtoInputDetail>> GetAllAsync()
		{
			DtoResult<DtoInputDetail> result = new();
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
		public async Task<DtoResult<DtoInputDetail>> GetOneAsync(DtoInputDetail dto)
		{
			DtoResult<DtoInputDetail> result = new();
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

        public Task<DtoResult<DtoInputDetail>> SaveAsync(DtoInputDetail dto)
        {
            throw new NotImplementedException();
        }

        public async Task<DtoResult<DtoInputDetail>> UpdateAsync(DtoInputDetail dto)
		{
			DtoResult<DtoInputDetail> result = new();
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