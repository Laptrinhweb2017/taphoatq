using Application.Asset.Dtos;
using Application.Asset.Interfaces;

namespace ServiceLib.Services
{
	public class PriceService(IRepository<DtoPrice> _repo) : IService<DtoPrice>
	{
		public async Task<DtoResult<DtoPrice>> AddAsync(DtoPrice dto)
		{
			DtoResult<DtoPrice> result = new();
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
		public async Task<DtoResult<DtoPrice>> DeleteAsync(DtoPrice dto)
		{
			DtoResult<DtoPrice> result = new();
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
		public async Task<DtoResult<DtoPrice>> DeleteAsync(object Id)
		{
			DtoResult<DtoPrice> result = new();
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
		public async Task<DtoResult<DtoPrice>> FindAsync(DtoPrice dto)
		{
			DtoResult<DtoPrice> result = new();
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
		public async Task<DtoResult<DtoPrice>> GetAllAsync()
		{
			DtoResult<DtoPrice> result = new();
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
		public async Task<DtoResult<DtoPrice>> GetOneAsync(DtoPrice dto)
		{
			DtoResult<DtoPrice> result = new();
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

        public async Task<DtoResult<DtoPrice>> SaveAsync(DtoPrice dto)
        {
            DtoResult<DtoPrice> rs = new();

            if (!dto.Price.HasValue || dto.Price.Value < 0)
            {
                rs.Message = "Giá sản phẩm không hợp lệ!";
                return rs;
            }

            dto.AppliedDate ??= DateTime.Now;

            if (dto.id.HasValue && dto.id.Value > 0)
            {
                rs = await UpdateAsync(dto);
            }
            else
            {
                rs = await AddAsync(dto);
            }

            return rs;
        }

        public async Task<DtoResult<DtoPrice>> UpdateAsync(DtoPrice dto)
		{
			DtoResult<DtoPrice> result = new();
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