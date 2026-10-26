using Microsoft.AspNetCore.Components.Forms;

namespace Application.Asset.Interfaces
{
    public interface IFileStorageService
    {
        Task<string> SaveFileAsync(IBrowserFile file, string customFileName, string folder);
        Task<List<string>> GetFolderContent(string path);
        void DeleteFolderContent(string folder);
        string DeleteFile(string folder, string filename);
        Task<bool> SaveTextFile(string folder, string fileName, string[] contents);
        Task<bool> AppendTextLogAsync(string folder, string fileName, string[] contents);
    }
}
