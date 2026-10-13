using Application.Asset.Dtos;
using Application.Asset.Interfaces;

namespace ServiceLib.Services
{
	public class POTrackingService(IRepository<DtoPOTracking> _repo) : IService<DtoPOTracking>
	{
		public async Task<DtoResult<DtoPOTracking>> AddAsync(DtoPOTracking dto)
		{
			DtoResult<DtoPOTracking> result = new();
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
		public async Task<DtoResult<DtoPOTracking>> DeleteAsync(DtoPOTracking dto)
		{
			DtoResult<DtoPOTracking> result = new();
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
		public async Task<DtoResult<DtoPOTracking>> DeleteAsync(object Id)
		{
			DtoResult<DtoPOTracking> result = new();
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
		public async Task<DtoResult<DtoPOTracking>> FindAsync(DtoPOTracking dto)
		{
			DtoResult<DtoPOTracking> result = new();
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
		public async Task<DtoResult<DtoPOTracking>> GetAllAsync()
		{
			DtoResult<DtoPOTracking> result = new();
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
		public async Task<DtoResult<DtoPOTracking>> GetOneAsync(DtoPOTracking dto)
		{
			DtoResult<DtoPOTracking> result = new();
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
		public async Task<DtoResult<DtoPOTracking>> UpdateAsync(DtoPOTracking dto)
		{
			DtoResult<DtoPOTracking> result = new();
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