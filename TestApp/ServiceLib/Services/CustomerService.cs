using Application.Asset.Dtos;
using Application.Asset.Interfaces;

namespace ServiceLib.Services
{
	public class CustomerService(IRepository<DtoCustomer> _repo) : IService<DtoCustomer>
	{
		public async Task<DtoResult<DtoCustomer>> AddAsync(DtoCustomer dto)
		{
			DtoResult<DtoCustomer> result = new();
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
		public async Task<DtoResult<DtoCustomer>> DeleteAsync(DtoCustomer dto)
		{
			DtoResult<DtoCustomer> result = new();
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
		public async Task<DtoResult<DtoCustomer>> DeleteAsync(object Id)
		{
			DtoResult<DtoCustomer> result = new();
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
		public async Task<DtoResult<DtoCustomer>> FindAsync(DtoCustomer dto)
		{
			DtoResult<DtoCustomer> result = new();
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
		public async Task<DtoResult<DtoCustomer>> GetAllAsync()
		{
			DtoResult<DtoCustomer> result = new();
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
		public async Task<DtoResult<DtoCustomer>> GetOneAsync(DtoCustomer dto)
		{
			DtoResult<DtoCustomer> result = new();
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

        public async Task<DtoResult<DtoCategory>> SaveAsync(DtoCustomer dto)
        {
            throw new NotImplementedException();
        }

        public async Task<DtoResult<DtoCustomer>> UpdateAsync(DtoCustomer dto)
		{
			DtoResult<DtoCustomer> result = new();
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