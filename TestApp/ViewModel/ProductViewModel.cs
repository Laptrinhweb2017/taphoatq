using Application.Asset.Common;
using Application.Asset.Dtos;
using Application.Asset.FullDto;
using Application.Asset.Interfaces;
using Application.Asset.ViewDtos;
using Microsoft.AspNetCore.Components.Forms;

namespace ViewModel
{
    public class ProductsViewModel(IService<FullProduct> _productsService, IFileStorageService fileService, IService<DtoPrice> _priceService) : ViewModelBase
    {
        #region Methods
        // Nạp toàn bộ dữ liệu 
        public async Task InitializeAsync()
        {
            IsLoading = true;
            ErrMess = string.Empty;

            try
            {
                // 1. Lấy danh sách sản phẩm
                DtoResult<FullProduct> resProducts = await _productsService.GetAllAsync();
                if (!IsValidResult(resProducts, DataMode.Multi))
                {
                    return;
                }
                // 2. Lấy danh sách giá 
                DtoResult<DtoPrice> resPrices = await _priceService.GetAllAsync();
                if (!IsValidResult(resPrices, DataMode.Multi))
                    return;

                PriceList = resPrices.ResultList!;

                CategoryList = [..resProducts.ResultList!.Where(x=>x.CateID!=null).GroupBy(cate => new {cate.CateID, cate.CateName})
                                    .Select(x=>new DtoCategory
                                    {
                                        CateName = x.Key.CateName,
                                        id = x.Key.CateID
                                    }).OrderBy(x=>x.CateName)];
                BrandList = [..resProducts.ResultList!.Where(x=>x.BrandID!=null).GroupBy(brand => new {brand.BrandID, brand.BrandName})
                                    .Select(x=> new DtoBrand{
                                        BrandName = x.Key.BrandName,
                                        id = x.Key.BrandID
                                    }).OrderBy(x=>x.BrandName)];
                RootProdList = [..resProducts.ResultList!.Where(x=>x.id!=null).Select(x=>new FullProduct {
                    id = x.id,
                    BrandName = x.BrandName,
                    ProductName = x.ProductName,
                    BrandID = x.BrandID,
                    CateID = x.CateID,
                    CateName= x.CateName,
                    CreatedAt = x.CreatedAt,
                    Descript = x.Descript,
                    Image = x.Image,
                    ProductCode = x.ProductCode,

                }).OrderBy(x=>x.ProductName)];
                List<string> imglist = await fileService.GetFolderContent("assets/img/product");
                RootProdList.ForEach(x =>
                {
                    string? s = imglist.FirstOrDefault(z => z.Contains(x.ProductCode));
                    x.Image = s != null ? s.Split("\\").LastOrDefault() : "default.jpg";
                });
            }
            catch (Exception ex)
            {
                ErrMess = $"Lỗi tải dữ liệu sản phẩm: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        #region Properties
        // Danh sách sản phẩm
        private List<FullProduct> _rootProdList = new();
        private List<FullProduct> RootProdList
        {
            get => _rootProdList;
            set
            {
                _rootProdList = value;
                if (ViewBy == 0) //xử lý tạo cây sản phẩm dựa vào cate
                {
                    ProductList = [.._rootProdList.Where(x=>x.id!=null).GroupBy(cate => new {cate.CateID, cate.CateName})
                                .Select(x=>new ViewDto<FullProduct>
                                {
                                    ID = $"cate{x.Key.CateID}",
                                    DisplayText = x.Key.CateName,
                                    Children = [],
                                    Data = [..x.OrderBy(z=>z.ProductName)]
                                })];
                }
                else //tạo cây sản phẩm dựa vào brand
                {
                    ProductList = [.._rootProdList.Where(x=>x.id!=null).GroupBy(brand => new {brand.BrandID, brand.BrandName})
                                .Select(x=>new ViewDto<FullProduct>
                                {
                                    ID = $"brand{x.Key.BrandID}",
                                    DisplayText = x.Key.BrandName,
                                    Children = [],
                                    Data = [..x.OrderBy(z=>z.ProductName)]
                                })];
                }
                OnPropertyChanged();
            }
        }
        int viewBy = 0;
        public int ViewBy
        {
            get => viewBy;
            set
            {
                viewBy = value;
                RootProdList = [.. _rootProdList];
            }
        } //0: View by cate; 1 viewby brand

        public List<ViewDto<FullProduct>> ProductList { get; set; } = [];

        // Đối tượng sản phẩm đang được chọn để Thêm/Sửa
        private FullProduct _selectedProduct = new();
        public FullProduct SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                OnPropertyChanged();
            }
        }

        //giá sản phẩm
        private List<DtoPrice> _priceList = new();
        public List<DtoPrice> PriceList = [];

        public List<DtoPrice> SelectedPrices { get; set; } = [];

        private DtoPrice _selectedPrice = new();
        public DtoPrice SelectedPrice
        {
            get => _selectedPrice;
            set
            {
                _selectedPrice = value;
                OnPropertyChanged();
            }
        }

        // Danh sách Danh mục
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

        // Danh sách Thương hiệu
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



        // Lấy giá mới nhất theo ProdID
        public decimal? GetPriceForProduct(int? prodId)
        {
            if (!prodId.HasValue) return null;
            return _priceList
                .Where(p => p.ProdID == prodId)
                .OrderByDescending(p => p.AppliedDate)
                .ThenByDescending(p => p.id)
                .FirstOrDefault()?.Price;
        }

        // Thêm mới hoặc cập nhật sản phẩm
        public async Task SaveProduct(IBrowserFile? img)
        {
            ErrMess = string.Empty;

            #region Kiểm tra trùng mã sản phẩm
            DtoProducts? sameCode = _rootProdList.FirstOrDefault(x =>
                x.ProductCode!.Equals(SelectedProduct.ProductCode!, StringComparison.OrdinalIgnoreCase) &&
                x.id != SelectedProduct.id);

            if (sameCode != null)
            {
                ErrMess = "Mã sản phẩm đã tồn tại!";
                return;
            }
            #endregion

            DtoResult<FullProduct> res = new();
            try
            {
                // Gọi hàm SaveAsync đã được viết sẵn trong ProductsService
                res = await _productsService.SaveAsync(SelectedProduct);

                if (!IsValidResult(res, DataMode.Single))
                    return;
                //xử lý file ảnh
                string imgFileName = "";
                if (img != null)
                {
                    imgFileName = await fileService.SaveFileAsync(img, $"{SelectedProduct.ProductCode}", "assets/img/product");
                }
                res.Result.Image = imgFileName.Split("\\").LastOrDefault();
                // Cập nhật lại giao diện bộ nhớ C#
                if (SelectedProduct.id == null) // Thêm mới
                {
                    _rootProdList.Add(res.Result!);
                }
                else // Cập nhật
                {
                    DtoProducts? prod = _rootProdList.FirstOrDefault(x => x.id == SelectedProduct.id);
                    if (prod != null)
                    {
                        prod.ProductCode = SelectedProduct.ProductCode;
                        prod.ProductName = SelectedProduct.ProductName;
                        prod.Image = SelectedProduct.Image;
                        prod.Descript = SelectedProduct.Descript;
                        prod.BrandID = SelectedProduct.BrandID;
                        prod.CateID = SelectedProduct.CateID;
                        prod.Image = imgFileName;
                    }
                }

                RootProdList = [.. _rootProdList];
                ErrMess = $"Lưu thành công sản phẩm {SelectedProduct.ProductName}";
            }
            catch (Exception ex)
            {
                ErrMess = $"Lỗi lưu sản phẩm: {ex.Message}";
            }
        }

        // Xóa sản phẩm
        public async Task DeleteProduct()
        {
            ErrMess = string.Empty;

            try
            {
                var res = await _productsService.DeleteAsync(SelectedProduct);

                if (!IsValidResult(res, DataMode.Delete))
                    return;

                FullProduct? prod = _rootProdList.FirstOrDefault(x => x.id == SelectedProduct.id);
                if (prod != null)
                {
                    _rootProdList.Remove(prod);
                }

                RootProdList = [.. _rootProdList];
                ErrMess = $"Xóa thành công sản phẩm {SelectedProduct.ProductName}";
            }
            catch (Exception ex)
            {
                ErrMess = $"Lỗi xóa sản phẩm: {ex.Message}";
            }
        }

        public async Task SavePriceAsync(DtoPrice dto)
        {
            SelectedPrice = new()
            {
                AppliedDate = dto.AppliedDate,
                id = dto.id,
                Price = dto.Price,
                ProdID = dto.ProdID
            };
            ErrMess = string.Empty;

            DtoResult<DtoPrice> res = new();
            try
            {
                //gán ProdID của sản phẩm đang chọn để áp dụng ngày mặc định    
                SelectedPrice.ProdID = SelectedProduct.id;
                SelectedPrice.AppliedDate ??= DateTime.Now;

                //gọi service để lưu thông tin giá
                res = await _priceService.SaveAsync(SelectedPrice);

                if (!IsValidResult(res, DataMode.Single))
                    return;

                //cập nhật lại giao diện 
                if (SelectedPrice.id == null)
                {
                    _priceList.Add(res.Result!);
                }
                else
                {
                    DtoPrice price = _priceList.FirstOrDefault(x => x.id == SelectedPrice.id);
                    if (price != null)
                    {
                        price.Price = res.Result!.Price;
                        price.AppliedDate = res.Result!.AppliedDate;
                    }
                }
                //cập nhật lại danh sách tổng và danh sách hiển thị trên modal
                PriceList = [.. _priceList];
                SelectedPrices = [.. _priceList.Where(x => x.ProdID == SelectedProduct.id)
                                       .OrderByDescending(x => x.AppliedDate)];
                //reset ô nhập mới cho lần sau 
                SelectedPrice = new DtoPrice { ProdID = SelectedProduct.id, AppliedDate = DateTime.Now };
                ErrMess = $"Lưu thành công mức giá {res.Result!.Price:NO} VNĐ";
            }
            catch (Exception ex)
            {
                ErrMess = $"Lỗi lưu bảng giá: {ex.Message}";
            }
        }
        #endregion
            }

    }