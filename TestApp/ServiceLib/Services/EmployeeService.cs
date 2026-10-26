using Application.Asset.Dtos;
using Application.Asset.Interfaces;

namespace ServiceLib.Services
{
	public class EmployeeService(IRepository<DtoEmployee> _repo) : IService<DtoEmployee>
	{
		public async Task<DtoResult<DtoEmployee>> AddAsync(DtoEmployee dto)
		{
			DtoResult<DtoEmployee> result = new();
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
		public async Task<DtoResult<DtoEmployee>> DeleteAsync(DtoEmployee dto)
		{
			DtoResult<DtoEmployee> result = new();
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
		public async Task<DtoResult<DtoEmployee>> DeleteAsync(object Id)
		{
			DtoResult<DtoEmployee> result = new();
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
		public async Task<DtoResult<DtoEmployee>> FindAsync(DtoEmployee dto)
		{
			DtoResult<DtoEmployee> result = new();
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
		public async Task<DtoResult<DtoEmployee>> GetAllAsync()
		{
			DtoResult<DtoEmployee> result = new();
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
		public async Task<DtoResult<DtoEmployee>> GetOneAsync(DtoEmployee dto)
		{
			DtoResult<DtoEmployee> result = new();
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

        public Task<DtoResult<DtoEmployee>> SaveAsync(DtoEmployee dto)
        {
            throw new NotImplementedException();
        }

        public async Task<DtoResult<DtoEmployee>> UpdateAsync(DtoEmployee dto)
		{
			DtoResult<DtoEmployee> result = new();
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