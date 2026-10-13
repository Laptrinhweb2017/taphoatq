using Application.Asset.Dtos;
using Application.Asset.Interfaces;

namespace ServiceLib.Services
{
	public class CategoryService(IRepository<DtoCategory> _repo) : IService<DtoCategory>
	{
		public async Task<DtoResult<DtoCategory>> AddAsync(DtoCategory dto)
		{
			DtoResult<DtoCategory> result = new();
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
		public async Task<DtoResult<DtoCategory>> DeleteAsync(DtoCategory SelectedCategory)
		{
			DtoResult<DtoCategory> rs = new();
            if (!SelectedCategory.id.HasValue	|| SelectedCategory.id.Value <= 0)
            {
                rs.Message = "Vui lòng chọn danh mục cần xóa!";
                return rs;
            }
            DtoResult<DtoCategory> result = new();
			try
			{
				result = await _repo.Delete(SelectedCategory);
				result.Succeed = true;
			}
			catch (Exception ex)
			{
				result.Message = ex.Message;
			}
			return result;
		}
		public async Task<DtoResult<DtoCategory>> DeleteAsync(object Id)
		{
			DtoResult<DtoCategory> result = new();
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
		public async Task<DtoResult<DtoCategory>> FindAsync(DtoCategory dto)
		{
			DtoResult<DtoCategory> result = new();
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
		public async Task<DtoResult<DtoCategory>> GetAllAsync()
		{
			DtoResult<DtoCategory> result = new();
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
		public async Task<DtoResult<DtoCategory>> GetOneAsync(DtoCategory dto)
		{
			DtoResult<DtoCategory> result = new();
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

        public async Task<DtoResult<DtoCategory>> SaveAsync(DtoCategory SelectedCategory)
        {
			DtoResult<DtoCategory> rs = new();
            if (string.IsNullOrWhiteSpace(SelectedCategory.CateName))
            {
				rs.Message = "Tên danh mục không được để trống!";
				return rs;
            }
            if (SelectedCategory.id.HasValue
                    && SelectedCategory.id.Value > 0)
            {
                // Cập nhật danh mục
                rs = await UpdateAsync(SelectedCategory);
            }
            else
            {
                // Thêm danh mục
                rs = await AddAsync(SelectedCategory);
            }
			rs.Succeed = true;
			return rs;
        }

        public async Task<DtoResult<DtoCategory>> UpdateAsync(DtoCategory dto)
		{
			DtoResult<DtoCategory> result = new();
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