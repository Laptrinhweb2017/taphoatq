using Application.Asset.Dtos;
using Application.Asset.Interfaces;

namespace ServiceLib.Services
{
	public class PaymentDetailService(IRepository<DtoPaymentDetail> _repo) : IService<DtoPaymentDetail>
	{
		public async Task<DtoResult<DtoPaymentDetail>> AddAsync(DtoPaymentDetail dto)
		{
			DtoResult<DtoPaymentDetail> result = new();
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
		public async Task<DtoResult<DtoPaymentDetail>> DeleteAsync(DtoPaymentDetail dto)
		{
			DtoResult<DtoPaymentDetail> result = new();
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
		public async Task<DtoResult<DtoPaymentDetail>> DeleteAsync(object Id)
		{
			DtoResult<DtoPaymentDetail> result = new();
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
		public async Task<DtoResult<DtoPaymentDetail>> FindAsync(DtoPaymentDetail dto)
		{
			DtoResult<DtoPaymentDetail> result = new();
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
		public async Task<DtoResult<DtoPaymentDetail>> GetAllAsync()
		{
			DtoResult<DtoPaymentDetail> result = new();
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
		public async Task<DtoResult<DtoPaymentDetail>> GetOneAsync(DtoPaymentDetail dto)
		{
			DtoResult<DtoPaymentDetail> result = new();
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

        public Task<DtoResult<DtoPaymentDetail>> SaveAsync(DtoPaymentDetail dto)
        {
            throw new NotImplementedException();
        }

        public async Task<DtoResult<DtoPaymentDetail>> UpdateAsync(DtoPaymentDetail dto)
		{
			DtoResult<DtoPaymentDetail> result = new();
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