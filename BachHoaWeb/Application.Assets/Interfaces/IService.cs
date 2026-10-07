using Application.Asset.Dtos;
using Application.Assets.Dtos;

namespace Application.Asset.Interfaces
{
	public interface IService<T>
	{
		Task<DtoResult<T>> GetAllAsync();
		Task<DtoResult<T>> GetOneAsync(T dto);
		Task<DtoResult<T>> FindAsync(T dto);
		Task<DtoResult<T>> AddAsync(T dto);
		Task<DtoResult<T>> UpdateAsync(T dto);
		Task<DtoResult<T>> DeleteAsync(T dto);
		Task<DtoResult<T>> DeleteAsync(object Id);
	}	
	public interface IServ<T> : IService<T>
	{
		Task<DtoResult<T>> GetByDateAsync(DateTime FromDate, DateTime ToDate);
	}

}