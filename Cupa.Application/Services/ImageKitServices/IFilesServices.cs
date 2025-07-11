namespace Cupa.Application.Services.ImageKitServices;
public interface IFilesServices
{
    Task<string> UploadImage(IFormFile image);

    Task<UploadResponse> UploadFileAsync(IFormFile file, string folder);
    Task<DeleteResponse> DeleteFileAsync(string Uid);
}