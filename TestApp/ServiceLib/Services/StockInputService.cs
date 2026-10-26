using Application.Asset.Dtos;
using Application.Asset.Interfaces;

namespace ServiceLib.Services
{
	public class StockInputService(IRepository<DtoStockInput> _repo) : IService<DtoStockInput>
	{
		public async Task<DtoResult<DtoStockInput>> AddAsync(DtoStockInput dto)
		{
			DtoResult<DtoStockInput> result = new();
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
		public async Task<DtoResult<DtoStockInput>> DeleteAsync(DtoStockInput dto)
		{
			DtoResult<DtoStockInput> result = new();
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
		public async Task<DtoResult<DtoStockInput>> DeleteAsync(object Id)
		{
			DtoResult<DtoStockInput> result = new();
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
		public async Task<DtoResult<DtoStockInput>> FindAsync(DtoStockInput dto)
		{
			DtoResult<DtoStockInput> result = new();
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
		public async Task<DtoResult<DtoStockInput>> GetAllAsync()
		{
			DtoResult<DtoStockInput> result = new();
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
		public async Task<DtoResult<DtoStockInput>> GetOneAsync(DtoStockInput dto)
		{
			DtoResult<DtoStockInput> result = new();
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

        public Task<DtoResult<DtoStockInput>> SaveAsync(DtoStockInput dto)
        {
            throw new NotImplementedException();
        }

        public async Task<DtoResult<DtoStockInput>> UpdateAsync(DtoStockInput dto)
		{
			DtoResult<DtoStockInput> result = new();
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