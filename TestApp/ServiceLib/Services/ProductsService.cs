using Application.Asset.Dtos;
using Application.Asset.Interfaces;

namespace ServiceLib.Services
{
	public class ProductsService(IRepository<DtoProducts> _repo) : IService<DtoProducts>
	{
		public async Task<DtoResult<DtoProducts>> AddAsync(DtoProducts dto)
		{
			DtoResult<DtoProducts> result = new();
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
        public async Task<DtoResult<DtoProducts>> DeleteAsync(DtoProducts SelectedProducts)
        {
            DtoResult<DtoProducts> rs = new();
            if (!SelectedProducts.id.HasValue || SelectedProducts.id.Value <= 0)
            {
                rs.Message = "Vui lòng sản phẩm cần xóa!";
                return rs;
            }
            DtoResult<DtoProducts> result = new();
            try
            {
                result = await _repo.Delete(SelectedProducts);
                result.Succeed = true;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
        public async Task<DtoResult<DtoProducts>> DeleteAsync(object Id)
		{
			DtoResult<DtoProducts> result = new();
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
		public async Task<DtoResult<DtoProducts>> FindAsync(DtoProducts dto)
		{
			DtoResult<DtoProducts> result = new();
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
		public async Task<DtoResult<DtoProducts>> GetAllAsync()
		{
			DtoResult<DtoProducts> result = new();
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
		public async Task<DtoResult<DtoProducts>> GetOneAsync(DtoProducts dto)
		{
			DtoResult<DtoProducts> result = new();
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
		public async Task<DtoResult<DtoProducts>> UpdateAsync(DtoProducts dto)
		{
			DtoResult<DtoProducts> result = new();
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