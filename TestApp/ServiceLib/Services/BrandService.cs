using Application.Asset.Dtos;
using Application.Asset.Interfaces;

namespace ServiceLib.Services
{
	public class BrandService(IRepository<DtoBrand> _repo) : IService<DtoBrand>
	{
		public async Task<DtoResult<DtoBrand>> AddAsync(DtoBrand dto)
		{
			DtoResult<DtoBrand> result = new();
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
		public async Task<DtoResult<DtoBrand>> DeleteAsync(DtoBrand SelectedBrand)
		{
			DtoResult<DtoBrand> rs = new();
			if(!SelectedBrand.id.HasValue	|| SelectedBrand.id.Value <=0)
            {
                rs.Message = "Vui lòng thương hiệu cần xóa!";
                return rs;
            }
            DtoResult<DtoBrand> result = new();
            try
			{
				result = await _repo.Delete(SelectedBrand);
                result.Succeed = true;
            }
			catch (Exception ex)
			{
				result.Message = ex.Message;
			}
			return result;
		}
		public async Task<DtoResult<DtoBrand>> DeleteAsync(object Id)
		{
			DtoResult<DtoBrand> result = new();
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
		public async Task<DtoResult<DtoBrand>> FindAsync(DtoBrand dto)
		{
			DtoResult<DtoBrand> result = new();
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
		public async Task<DtoResult<DtoBrand>> GetAllAsync()
		{
			DtoResult<DtoBrand> result = new();
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
		public async Task<DtoResult<DtoBrand>> GetOneAsync(DtoBrand dto)
		{
			DtoResult<DtoBrand> result = new();
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

        public async Task<DtoResult<DtoBrand>> SaveBra(DtoBrand SelectedBrand)
        {
            DtoResult<DtoBrand> rs = new();
            if (string.IsNullOrWhiteSpace(SelectedBrand.BrandName))
            {
                rs.Message = "Tên thương hiệu không được để trống!";
                return rs;
            }
            if (SelectedBrand.id.HasValue
                    && SelectedBrand.id.Value > 0)
            {
                // Cập nhật thương hiệu
                rs = await UpdateAsync(SelectedBrand);
            }
            else
            {
                // Thêm thương hiệu
                rs = await AddAsync(SelectedBrand);
            }
            rs.Succeed = true;
            return rs;
        }

        public async Task<DtoResult<DtoBrand>> SaveAsync(DtoBrand dto)
        {
            DtoResult<DtoBrand> rs = new();
            if (string.IsNullOrWhiteSpace(dto.BrandName))
            {
                rs.Message = "Tên danh mục không được để trống!";
                return rs;
            }
            if (dto.id.HasValue
                    && dto.id.Value > 0)
            {
                // Cập nhật danh mục
                rs = await UpdateAsync(dto);
            }
            else
            {
                // Thêm danh mục
                rs = await AddAsync(dto);
            }
            rs.Succeed = true;
            return rs;
        }

        public async Task<DtoResult<DtoBrand>> UpdateAsync(DtoBrand dto)
		{
			DtoResult<DtoBrand> result = new();
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