using System.ComponentModel;
using System.Runtime.CompilerServices;
using Application.Asset.Common;
using Application.Asset.Dtos;
using Application.Asset.Interfaces;
using ServiceLib.Services;

namespace ViewModel
{
    public class CategoryViewModel : ViewModelBase
    {
        private readonly IService<DtoCategory> _categoryService;
        //private readonly IService<DtoCategory> categoryService;

        public event PropertyChangedEventHandler? PropertyChanged;

        public CategoryViewModel(IService<DtoCategory> categoryService)
        {
            
            CategoryList = new List<DtoCategory>();
            SelectedCategory = new DtoCategory();
            _categoryService = categoryService;
        }

        #region Properties

        private List<DtoCategory> _categoryList = new();

        public List<DtoCategory> CategoryList
        {
            get => _categoryList;
            set
            {
                _categoryList = value;
                OnPropertyChanged();
            }
        }

        private DtoCategory _selectedCategory = new();

        public DtoCategory SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
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

        // Lấy danh sách danh mục
        public async Task InitializeAsync()
        {
            IsLoading = true;
            ErrMess = string.Empty;

            try
            {
                var res = await _categoryService.GetAllAsync();
                if (!IsValidResult(res, DataMode.Multi))
                    return;
                CategoryList = res.ResultList!;
            }
            catch (Exception ex)
            {
                ErrMess = $"Lỗi tải danh sách danh mục: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        // Thêm mới hoặc cập nhật danh mục
        public async Task SaveCategory()
        {
            ErrMess = string.Empty;
            #region Kiểm tra trùng
            DtoCategory? same = _categoryList
                                .FirstOrDefault(x => x.CateName!.Equals(SelectedCategory.CateName!, StringComparison.OrdinalIgnoreCase) &&
                                x.id != SelectedCategory.id);
            if (same != null)
            {
                ErrMess = "Trùng";
                return;
            }

            #endregion
            DtoResult<DtoCategory> res = new();
            try
            {
                res = await _categoryService.SaveAsync(SelectedCategory);      
                if (!IsValidResult(res, DataMode.Single))
                    return;
                //cập nhật giao diện
                if(SelectedCategory.id==null) //thêm mới
                    _categoryList.Add(res.Result!); //thêm cái kết quả trả về mới có id
                else
                {
                    DtoCategory? cate = _categoryList.FirstOrDefault(x => x.id == SelectedCategory.id);
                    cate?.CateName = SelectedCategory.CateName;
                }
                CategoryList = [.._categoryList];

                ErrMess = $"Lưu thành công nhóm {SelectedCategory.CateName}";
            }
            catch (Exception ex)
            {
                ErrMess = $"Lỗi lưu danh mục: {ex.Message}";
            }
        }

        // Xóa danh mục
        public async Task DeleteCategory()
        {
            ErrMess = string.Empty;

            try
            {
                var res = await _categoryService.DeleteAsync(SelectedCategory);

                if (!IsValidResult(res, DataMode.Delete)) //kiểm tra kết quả trả về
                    return;
                DtoCategory? cate = _categoryList.FirstOrDefault(x => x.id == SelectedCategory.id);//tìm trong danh sách
                if(cate != null)
                    _categoryList.Remove(cate);//xóa trong danh sách
                ErrMess = $"Xóa thành công nhóm {SelectedCategory.CateName}";
                CategoryList = [.. _categoryList];
            }
            catch (Exception ex)
            {
                ErrMess = $"Lỗi xóa danh mục: {ex.Message}";
            }
        }

        #endregion

    }
}
