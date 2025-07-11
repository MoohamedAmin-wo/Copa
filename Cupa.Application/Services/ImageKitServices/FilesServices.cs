using System.Net;

namespace Cupa.Application.Services.ImageKitServices;
internal sealed class FilesServices : IFilesServices
{
    private readonly ImageKitAccount _account;
    private readonly ImagekitClient _client;
    public FilesServices(IOptions<ImageKitAccount> account)
    {
        _account = account.Value;
        _client = new ImagekitClient(_account.PublicKey, _account.PrivateKey, _account.UrlEndpoint);
    }

    public async Task<UploadResponse> UploadFileAsync(IFormFile file, string folder)
    {
        var fileTypeResult = file.GetFileType();
        if (fileTypeResult.Equals(FileType.invalid))
            return new UploadResponse { Message = "Invalid file Type !" };

        if (fileTypeResult.Equals(FileType.picture))
        {
            var pictureValidation = file.IsValidImage();
            if (!string.IsNullOrEmpty(pictureValidation))
                return new UploadResponse { Message = pictureValidation };
        }

        if (fileTypeResult.Equals(FileType.video))
        {
            var VideoValidation = await file.IsValidVideo();
            if (!string.IsNullOrEmpty(VideoValidation))
                return new UploadResponse { Message = VideoValidation };
        }

        if (fileTypeResult.Equals(FileType.pdf))
        {
            var pdfValidation = file.IsValidPdf();
            if (!string.IsNullOrEmpty(pdfValidation))
                return new UploadResponse { Message = pdfValidation };
        }

        var fileBase64String = await ConvertToBase64StringAsync(file);
        var extension = Path.GetExtension(file.FileName);

        var uploadRequest = new FileCreateRequest
        {
            file = fileBase64String,
            fileName = $"{Guid.NewGuid()}.{extension}",
            folder = folder,
            useUniqueFileName = true,
            isPrivateFile = false,
        };


        Result result = await _client.UploadAsync(uploadRequest);

        if (result is null || string.IsNullOrEmpty(result.url))
            return new UploadResponse { Message = "Upload faild !\n no response returned" };

        return new UploadResponse
        {
            IsSuccess = true,
            Url = result.url,
            Uid = uploadRequest.fileName,
            Message = "Upload Successfully ",
            ThumbnailUrl = result.thumbnailUrl,
        };
    }

    public async Task<DeleteResponse> DeleteFileAsync(string Uid)
    {
        var result = await _client.DeleteFileAsync(Uid);
        if (result.HttpStatusCode != ((int)HttpStatusCode.OK))
            return new DeleteResponse { Message = "unable to delete current file !" };

        return new DeleteResponse { IsSuccess = true, Message = "deleted Successfully " };
    }

    public async Task<string> UploadImage(IFormFile image)
    {

        using var memoryStream = new MemoryStream();
        await image.CopyToAsync(memoryStream);
        var fileBytes = memoryStream.ToArray();
        var file = Convert.ToBase64String(fileBytes);

        var extension = Path.GetExtension(image.FileName);

        var uploadRequest = new FileCreateRequest
        {
            file = file,
            fileName = $"{Guid.NewGuid().ToString()}.{extension}",
            folder = "/TestFolder",
            useUniqueFileName = true,
            isPrivateFile = false
        };

        var result = await _client.UploadAsync(uploadRequest);
        return result.fileId;
    }

    private async Task<string> ConvertToBase64StringAsync(IFormFile file)
    {
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        var fileBytes = memoryStream.ToArray();
        var result = Convert.ToBase64String(fileBytes);


        return result;
    }

}