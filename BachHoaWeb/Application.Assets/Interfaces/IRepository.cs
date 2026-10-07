using Application.Assets.Dtos;

namespace Application.Asset.Interfaces
{
	public interface IRepository<T>
	{
		Task<DtoResult<T>> GetAll();
		Task<DtoResult<T>> GetOne(T entity);
		Task<DtoResult<T>> Find(T entity);
		Task<DtoResult<T>> Add(T entity);
		Task<DtoResult<T>> Update(T entity);
		Task<DtoResult<T>> Delete(T entity);
		Task<DtoResult<T>> Delete(object entityId);
	}
	public interface IRepo<T> : IRepository<T>
	{
		Task<DtoResult<T>> GetByDate(DateTime FromDate, DateTime ToDate);
	}
}