using Application.Asset.Interfaces;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;
using static System.Net.Mime.MediaTypeNames;

namespace ServiceLib.Services
{
	public class FileStorageService(IWebHostEnvironment env) : IFileStorageService
	{
		private readonly IWebHostEnvironment _env = env;
		public async Task<List<string>> GetFolderContent(string path)
		{
            var fromPath = Path.Combine(_env.WebRootPath, path);
			if (!Directory.Exists(fromPath))
			{
				return [];
			} 
			return Directory.GetFiles(fromPath).ToList();
        }
        public async Task<bool> SaveTextFile(string folder, string fileName, string[] contents)
        {
            try
            {
                var uploadsPath = Path.Combine(_env.WebRootPath ?? AppContext.BaseDirectory, folder);
                Directory.CreateDirectory(uploadsPath); // Đảm bảo thư mục tồn tại

                var fullPath = Path.Combine(uploadsPath, fileName);

                // Ghi đè toàn bộ nội dung mới vào file (tự động tạo mới nếu chưa có, ghi đè nếu đã tồn tại)
                await File.WriteAllLinesAsync(fullPath, contents);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> AppendTextLogAsync(string folder, string fileName, string[] contents)
        {
            try
            {
                var uploadsPath = Path.Combine(_env.WebRootPath ?? AppContext.BaseDirectory, folder);
                Directory.CreateDirectory(uploadsPath); // Đảm bảo thư mục tồn tại

                var fullPath = Path.Combine(uploadsPath, fileName);

                // Chuẩn bị sẵn dữ liệu log (thêm Environment.NewLine để các dòng không bị dính vào nhau)
                var linesToAppend = contents.Select(s => $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {s}{Environment.NewLine}");

                // Ghi nối tiếp vào file (tự động tạo file nếu chưa có, hỗ trợ đa luồng an toàn hơn)
                await File.AppendAllLinesAsync(fullPath, linesToAppend);

                return true;
            }
            catch (Exception)
            {
                // Tùy chọn: Có thể ghi log ra console hoặc throw tùy ý
                throw;
            }
        }

        public async Task<string> SaveFileAsync(IBrowserFile file, string customFileName, string folder)
		{
			if (file == null || string.IsNullOrWhiteSpace(customFileName))
				throw new ArgumentException("Invalid file or name");

			var uploadsPath = Path.Combine(_env.WebRootPath, folder);
			Directory.CreateDirectory(uploadsPath); // make sure the folder exists

			var ext = Path.GetExtension(file.Name);
			var fullPath = Path.Combine(uploadsPath, customFileName + ext);
			if (File.Exists(fullPath))
			{
				File.Delete(fullPath);
			}	

			await using var stream = new FileStream(fullPath, FileMode.Create);
			await file.OpenReadStream(10 * 1024 * 1024).CopyToAsync(stream); // limit to 10MB

			return Path.Combine(folder, customFileName + ext); // relative path
		}
		public void DeleteFolderContent(string folder)
		{
			var uploadsPath = Path.Combine(_env.WebRootPath, folder);
			var files =  Directory.GetFiles(uploadsPath);
			foreach (var item in files)
			{
				File.Delete(item);
			}
		}

		public string DeleteFile(string folder, string filename)
		{
			var uploadsPath = Path.Combine(_env.WebRootPath, folder, filename);
			if(File.Exists(uploadsPath))
			{
				File.Delete(uploadsPath);
				return "OK";
			}
			else
				return "Không tìm thấy";
		}
	}
}
