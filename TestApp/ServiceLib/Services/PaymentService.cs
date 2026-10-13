using Application.Asset.Dtos;
using Application.Asset.Interfaces;

namespace ServiceLib.Services
{
	public class PaymentService(IRepository<DtoPayment> _repo) : IService<DtoPayment>
	{
		public async Task<DtoResult<DtoPayment>> AddAsync(DtoPayment dto)
		{
			DtoResult<DtoPayment> result = new();
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
		public async Task<DtoResult<DtoPayment>> DeleteAsync(DtoPayment dto)
		{
			DtoResult<DtoPayment> result = new();
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
		public async Task<DtoResult<DtoPayment>> DeleteAsync(object Id)
		{
			DtoResult<DtoPayment> result = new();
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
		public async Task<DtoResult<DtoPayment>> FindAsync(DtoPayment dto)
		{
			DtoResult<DtoPayment> result = new();
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
		public async Task<DtoResult<DtoPayment>> GetAllAsync()
		{
			DtoResult<DtoPayment> result = new();
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
		public async Task<DtoResult<DtoPayment>> GetOneAsync(DtoPayment dto)
		{
			DtoResult<DtoPayment> result = new();
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
		public async Task<DtoResult<DtoPayment>> UpdateAsync(DtoPayment dto)
		{
			DtoResult<DtoPayment> result = new();
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