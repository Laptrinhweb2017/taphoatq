using Application.Assets.Dtos;
using DataLayer.Repository;


namespace Services
{
    public class CustomerService(CustomerRepo _repo)
    {
        public async Task<DtoResult<DtoCustomer>> GetAllAsync()
        {
            DtoResult<DtoCustomer> result = new();
            try
            {
                result = await _repo.GetAllAsync();
            }
            catch (Exception)
            {

                throw;
            }
            result.Succeed = result.ResultList != null;
            return result;
        }
        public async Task<DtoResult<DtoCustomer>> AddAsync(DtoCustomer dto)
        {
            DtoResult<DtoCustomer> result = new();
            try
            {
                result = await _repo.AddAsync(dto);
            }
            catch (Exception)
            {

                throw;
            }
            result.Succeed = result.Result != null;
            return result;
        }
        public async Task<DtoResult<DtoCustomer>> UpdateAsync(DtoCustomer dto)
        {
            DtoResult<DtoCustomer> result = new();
            try
            {
                result = await _repo.UpdateAsync(dto);
            }
            catch (Exception)
            {

                throw;
            }
            result.Succeed = result.Result != null;
            return result;
        }
        public async Task<DtoResult<DtoCustomer>> DeleteAsync(object? ID)
        {
            DtoResult<DtoCustomer> result = new();
            try
            {
                result = await _repo.DeleteAsync(ID);
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }
        public async Task<DtoResult<DtoCustomer>> DeleteAsync(DtoCustomer dto) => await DeleteAsync(dto.Id);
    }
}
