using System.ComponentModel;
using System.Runtime.CompilerServices;
using Application.Asset.Common;
using Application.Asset.Dtos;
using ServiceLib.Services;

namespace ViewModel
{
    public class BrandViewModel : ViewModelBase
    {
        private readonly BrandService _brandService;

        public event PropertyChangedEventHandler? PropertyChanged;

        public BrandViewModel(BrandService brandService)
        {
            _brandService = brandService;
            BrandList = new List<DtoBrand>();
            SelectedBrand = new DtoBrand();
        }

        #region Properties

        private List<DtoBrand> _brandList = new();

        public List<DtoBrand> BrandList
        {
            get => _brandList;
            set
            {
                _brandList = value;
                OnPropertyChanged();
            }
        }

        private DtoBrand _selectedBrand = new();

        public DtoBrand SelectedBrand
        {
            get => _selectedBrand;
            set
            {
                _selectedBrand = value;
                OnPropertyChanged();
            }
        }

        private bool _isLoading;

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Methods

        // Lấy danh sách thương hiệu
        public async Task InitializeAsync()
        {
            IsLoading = true;
            ErrMess = string.Empty;

            try
            {
                var res = await _brandService.GetAllAsync();
                if (!IsValidResult(res, DataMode.Multi))
                    return;
                BrandList = res.ResultList!;
            }
            catch (Exception ex)
            {
                ErrMess = $"Lỗi tải danh sách thương hiệu: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        // Thêm mới hoặc cập nhật thương hiệu
        public async Task SaveBrand()
        {
            ErrMess = string.Empty;

            #region Kiểm tra trùng
            DtoBrand? same = _brandList
                                .FirstOrDefault(x => x.BrandName!.Equals(SelectedBrand.BrandName!, StringComparison.OrdinalIgnoreCase) &&
                                x.id != SelectedBrand.id);
            if (same != null)
            {
                ErrMess = "Trùng";
                return;
            }
            #endregion

            DtoResult<DtoBrand> res = new();
            try
            {
                res = await _brandService.SaveBra(SelectedBrand);
                if (!IsValidResult(res, DataMode.Single))
                    return;

                // Cập nhật giao diện
                if (SelectedBrand.id == null) // Thêm mới
                    _brandList.Add(res.Result!); // Thêm kết quả trả về mới có id
                else
                {
                    DtoBrand? brand = _brandList.FirstOrDefault(x => x.id == SelectedBrand.id);
                    if (brand != null)
                        brand.BrandName = SelectedBrand.BrandName;
                }
                BrandList = [.. _brandList];

                ErrMess = $"Lưu thành công thương hiệu {SelectedBrand.BrandName}";
            }
            catch (Exception ex)
            {
                ErrMess = $"Lỗi lưu thương hiệu: {ex.Message}";
            }
        }

        // Xóa thương hiệu
        public async Task DeleteBrand()
        {
            ErrMess = string.Empty;

            try
            {
                var res = await _brandService.DeleteAsync(SelectedBrand);

                if (!IsValidResult(res, DataMode.Delete)) // Kiểm tra kết quả trả về
                    return;

                DtoBrand? brand = _brandList.FirstOrDefault(x => x.id == SelectedBrand.id); // Tìm trong danh sách
                if (brand != null)
                    _brandList.Remove(brand); // Xóa trong danh sách

                ErrMess = $"Xóa thành công thương hiệu {SelectedBrand.BrandName}";
                BrandList = [.. _brandList];
            }
            catch (Exception ex)
            {
                ErrMess = $"Lỗi xóa thương hiệu: {ex.Message}";
            }
        }

        #endregion
    }
}