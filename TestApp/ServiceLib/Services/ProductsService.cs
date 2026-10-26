using Application.Asset.Dtos;
using Application.Asset.FullDto;
using Application.Asset.Interfaces;

namespace ServiceLib.Services
{
	public class ProductsService(IRepository<FullProduct> _repo) : IService<FullProduct>
	{
		public async Task<DtoResult<FullProduct>> AddAsync(FullProduct dto)
		{
			DtoResult<FullProduct> result = new();
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
        public async Task<DtoResult<FullProduct>> DeleteAsync(FullProduct SelectedProducts)
        {
            DtoResult<FullProduct> rs = new();
            if (!SelectedProducts.id.HasValue || SelectedProducts.id.Value <= 0)
            {
                rs.Message = "Vui lòng sản phẩm cần xóa!";
                return rs;
            }
            DtoResult<FullProduct> result = new();
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
        public async Task<DtoResult<FullProduct>> DeleteAsync(object Id)
		{
			DtoResult<FullProduct> result = new();
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
		public async Task<DtoResult<FullProduct>> FindAsync(FullProduct dto)
		{
			DtoResult<FullProduct> result = new();
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
		public async Task<DtoResult<FullProduct>> GetAllAsync()
		{
			DtoResult<FullProduct> result = new();
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
		public async Task<DtoResult<FullProduct>> GetOneAsync(FullProduct dto)
		{
			DtoResult<FullProduct> result = new();
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

        public async Task<DtoResult<FullProduct>> SaveAsync(FullProduct dto)
        {
            DtoResult<FullProduct> rs = new();

            // Kiểm tra mã sản phẩm
            if (string.IsNullOrWhiteSpace(dto.ProductCode))
            {
                rs.Message = "Mã sản phẩm không được để trống!";
                return rs;
            }

            // Kiểm tra tên sản phẩm
            if (string.IsNullOrWhiteSpace(dto.ProductName))
            {
                rs.Message = "Tên sản phẩm không được để trống!";
                return rs;
            }

            if (dto.id.HasValue && dto.id.Value > 0)
            {
                // Cập nhật sản phẩm
                rs = await UpdateAsync(dto);
            }
            else
            {
                // Thêm sản phẩm
                rs = await AddAsync(dto);
            }

            return rs;
        }

        public async Task<DtoResult<FullProduct>> UpdateAsync(FullProduct dto)
		{
			DtoResult<FullProduct> result = new();
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