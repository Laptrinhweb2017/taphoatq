using Application.Asset.Dtos;
using Application.Asset.Interfaces;

namespace ServiceLib.Services
{
	public class StockOutputService(IRepository<DtoStockOutput> _repo) : IService<DtoStockOutput>
	{
		public async Task<DtoResult<DtoStockOutput>> AddAsync(DtoStockOutput dto)
		{
			DtoResult<DtoStockOutput> result = new();
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
		public async Task<DtoResult<DtoStockOutput>> DeleteAsync(DtoStockOutput dto)
		{
			DtoResult<DtoStockOutput> result = new();
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
		public async Task<DtoResult<DtoStockOutput>> DeleteAsync(object Id)
		{
			DtoResult<DtoStockOutput> result = new();
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
		public async Task<DtoResult<DtoStockOutput>> FindAsync(DtoStockOutput dto)
		{
			DtoResult<DtoStockOutput> result = new();
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
		public async Task<DtoResult<DtoStockOutput>> GetAllAsync()
		{
			DtoResult<DtoStockOutput> result = new();
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
		public async Task<DtoResult<DtoStockOutput>> GetOneAsync(DtoStockOutput dto)
		{
			DtoResult<DtoStockOutput> result = new();
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

        public Task<DtoResult<DtoStockOutput>> SaveAsync(DtoStockOutput dto)
        {
            throw new NotImplementedException();
        }

        public async Task<DtoResult<DtoStockOutput>> UpdateAsync(DtoStockOutput dto)
		{
			DtoResult<DtoStockOutput> result = new();
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