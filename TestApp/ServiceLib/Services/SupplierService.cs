using Application.Asset.Dtos;
using Application.Asset.Interfaces;

namespace ServiceLib.Services
{
	public class SupplierService(IRepository<DtoSupplier> _repo) : IService<DtoSupplier>
	{
		public async Task<DtoResult<DtoSupplier>> AddAsync(DtoSupplier dto)
		{
			DtoResult<DtoSupplier> result = new();
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
		public async Task<DtoResult<DtoSupplier>> DeleteAsync(DtoSupplier dto)
		{
			DtoResult<DtoSupplier> result = new();
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
		public async Task<DtoResult<DtoSupplier>> DeleteAsync(object Id)
		{
			DtoResult<DtoSupplier> result = new();
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
		public async Task<DtoResult<DtoSupplier>> FindAsync(DtoSupplier dto)
		{
			DtoResult<DtoSupplier> result = new();
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
		public async Task<DtoResult<DtoSupplier>> GetAllAsync()
		{
			DtoResult<DtoSupplier> result = new();
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
		public async Task<DtoResult<DtoSupplier>> GetOneAsync(DtoSupplier dto)
		{
			DtoResult<DtoSupplier> result = new();
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

        public Task<DtoResult<DtoSupplier>> SaveAsync(DtoSupplier dto)
        {
            throw new NotImplementedException();
        }

        public async Task<DtoResult<DtoSupplier>> UpdateAsync(DtoSupplier dto)
		{
			DtoResult<DtoSupplier> result = new();
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