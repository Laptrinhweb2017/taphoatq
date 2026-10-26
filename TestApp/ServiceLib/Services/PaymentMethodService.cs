using Application.Asset.Dtos;
using Application.Asset.Interfaces;

namespace ServiceLib.Services
{
	public class PaymentMethodService(IRepository<DtoPaymentMethod> _repo) : IService<DtoPaymentMethod>
	{
		public async Task<DtoResult<DtoPaymentMethod>> AddAsync(DtoPaymentMethod dto)
		{
			DtoResult<DtoPaymentMethod> result = new();
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
		public async Task<DtoResult<DtoPaymentMethod>> DeleteAsync(DtoPaymentMethod dto)
		{
			DtoResult<DtoPaymentMethod> result = new();
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
		public async Task<DtoResult<DtoPaymentMethod>> DeleteAsync(object Id)
		{
			DtoResult<DtoPaymentMethod> result = new();
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
		public async Task<DtoResult<DtoPaymentMethod>> FindAsync(DtoPaymentMethod dto)
		{
			DtoResult<DtoPaymentMethod> result = new();
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
		public async Task<DtoResult<DtoPaymentMethod>> GetAllAsync()
		{
			DtoResult<DtoPaymentMethod> result = new();
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
		public async Task<DtoResult<DtoPaymentMethod>> GetOneAsync(DtoPaymentMethod dto)
		{
			DtoResult<DtoPaymentMethod> result = new();
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

        public Task<DtoResult<DtoPaymentMethod>> SaveAsync(DtoPaymentMethod dto)
        {
            throw new NotImplementedException();
        }

        public async Task<DtoResult<DtoPaymentMethod>> UpdateAsync(DtoPaymentMethod dto)
		{
			DtoResult<DtoPaymentMethod> result = new();
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