using Application.Asset.Dtos;
using Application.Asset.Interfaces;

namespace ServiceLib.Services
{
	public class WarehousesService(IRepository<DtoWarehouses> _repo) : IService<DtoWarehouses>
	{
		public async Task<DtoResult<DtoWarehouses>> AddAsync(DtoWarehouses dto)
		{
			DtoResult<DtoWarehouses> result = new();
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
		public async Task<DtoResult<DtoWarehouses>> DeleteAsync(DtoWarehouses dto)
		{
			DtoResult<DtoWarehouses> result = new();
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
		public async Task<DtoResult<DtoWarehouses>> DeleteAsync(object Id)
		{
			DtoResult<DtoWarehouses> result = new();
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
		public async Task<DtoResult<DtoWarehouses>> FindAsync(DtoWarehouses dto)
		{
			DtoResult<DtoWarehouses> result = new();
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
		public async Task<DtoResult<DtoWarehouses>> GetAllAsync()
		{
			DtoResult<DtoWarehouses> result = new();
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
		public async Task<DtoResult<DtoWarehouses>> GetOneAsync(DtoWarehouses dto)
		{
			DtoResult<DtoWarehouses> result = new();
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

        public Task<DtoResult<DtoWarehouses>> SaveAsync(DtoWarehouses dto)
        {
            throw new NotImplementedException();
        }

        public async Task<DtoResult<DtoWarehouses>> UpdateAsync(DtoWarehouses dto)
		{
			DtoResult<DtoWarehouses> result = new();
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