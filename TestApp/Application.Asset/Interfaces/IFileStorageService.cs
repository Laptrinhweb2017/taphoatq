using Microsoft.AspNetCore.Components.Forms;

namespace Application.Asset.Interfaces
{
    public interface IFileStorageService
    {
        Task<string> SaveFileAsync(IBrowserFile file, string customFileName, string folder);
        void DeleteFolderContent(string folder);
        string DeleteFile(string folder, string filename);
    }
}
