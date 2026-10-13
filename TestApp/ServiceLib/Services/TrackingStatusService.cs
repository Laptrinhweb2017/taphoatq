using Application.Asset.Dtos;
using Application.Asset.Interfaces;

namespace ServiceLib.Services
{
	public class TrackingStatusService(IRepository<DtoTrackingStatus> _repo) : IService<DtoTrackingStatus>
	{
		public async Task<DtoResult<DtoTrackingStatus>> AddAsync(DtoTrackingStatus dto)
		{
			DtoResult<DtoTrackingStatus> result = new();
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
		public async Task<DtoResult<DtoTrackingStatus>> DeleteAsync(DtoTrackingStatus dto)
		{
			DtoResult<DtoTrackingStatus> result = new();
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
		public async Task<DtoResult<DtoTrackingStatus>> DeleteAsync(object Id)
		{
			DtoResult<DtoTrackingStatus> result = new();
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
		public async Task<DtoResult<DtoTrackingStatus>> FindAsync(DtoTrackingStatus dto)
		{
			DtoResult<DtoTrackingStatus> result = new();
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
		public async Task<DtoResult<DtoTrackingStatus>> GetAllAsync()
		{
			DtoResult<DtoTrackingStatus> result = new();
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
		public async Task<DtoResult<DtoTrackingStatus>> GetOneAsync(DtoTrackingStatus dto)
		{
			DtoResult<DtoTrackingStatus> result = new();
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
		public async Task<DtoResult<DtoTrackingStatus>> UpdateAsync(DtoTrackingStatus dto)
		{
			DtoResult<DtoTrackingStatus> result = new();
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