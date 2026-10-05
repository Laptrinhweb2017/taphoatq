using Application.Assets.Dto;
using DataLayer.Repository;

namespace Services
{
    public class CategoryService(CategoryRepo _repo)//dependency injection
    {
        public async Task<DtoResult<DtoCategory>> GetAllAsync()
        {
            DtoResult<DtoCategory> result = new();
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
        public async Task<DtoResult<DtoCategory>> AddAsync(DtoCategory dto)
        {
            DtoResult<DtoCategory> result = new();
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
        public async Task<DtoResult<DtoCategory>> UpdateAsync(DtoCategory dto)
        {
            DtoResult<DtoCategory> result = new();
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
        public async Task<DtoResult<DtoCategory>> DeleteAsync(object? ID)
        {
            DtoResult<DtoCategory> result = new();
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
        public async Task<DtoResult<DtoCategory>> DeleteAsync(DtoCategory dto) => await DeleteAsync(dto.Id);
    }
}
