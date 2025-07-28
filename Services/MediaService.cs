using api_event_panel.Models;
using api_event_panel.Repositories;
using  System.Diagnostics;

namespace api_event_panel.Services;

public class MediaService:IMediaService
{
    public readonly IMediaRepository _repository;
    public readonly IUserRepository _userRepository;

    public MediaService(IMediaRepository repository,IUserRepository userRepository)
    {
        _repository = repository;
        _userRepository = userRepository;
    }

    public async Task<List<MediaModel>> GetMedias()
    {
        return await _repository.GetMedias();
    }

    public async Task<MediaModel> GetMedia(int id)
    {
        return await _repository.GetMedia(id);
    }

    public async Task SaveMedia(int userId,List<IFormFile> mediaList)
    {
        if (!Directory.Exists("uploads"))
            Directory.CreateDirectory("uploads");
        
        UserModel user = _userRepository.GetById(userId).Result;
        foreach (var media in mediaList)
        {
            var newFileName = user.Username + "-" + media.FileName;
            
            var isImage = media.ContentType.StartsWith("image/");
            var isVideo = media.ContentType.StartsWith("video/");

            if (!isImage && !isVideo)
                continue;
            
            var folder = isImage? "uploads/images" : "uploads/videos";
            
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            
            var filePath = Path.Combine(folder, newFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await media.CopyToAsync(stream);
            }


            
            var mediaModel = new MediaModel
            {
                FileName = newFileName,
                FilePath = filePath,
                User = user,
                UploadedOn = DateTime.UtcNow,
                FileType =isImage ?"image":"video",
                FileSize = media.Length,
            };
            if (isVideo)
            {
                var posterFolder = "uploads/posters";
                if (!Directory.Exists(posterFolder))
                    Directory.CreateDirectory(posterFolder);

                string safeFileName = Path.GetFileNameWithoutExtension(newFileName)
                    .Replace(" ", "_")
                    .Replace("'", "")
                    .Replace("\"", "");

                var posterFileName = safeFileName + ".jpg";
                var posterPath = Path.Combine(posterFolder, posterFileName);

                var ffmpegCommand = $"ffmpeg -i \\\"{filePath}\\\" -ss 00:00:01.000 -vframes 1 \\\"{posterPath}\\\"";
                
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "bash",
                        Arguments = $"-c \"{ffmpegCommand}\"",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                    }
                };
                process.Start();
                string output = await process.StandardOutput.ReadToEndAsync();
                string error = await process.StandardError.ReadToEndAsync();
                Console.WriteLine(output);
                Console.WriteLine(error);
                await process.WaitForExitAsync();

                // Optional: DB'ye poster path eklemek istersen
                mediaModel.PosterPath = posterPath;
            }

            await _repository.SaveMedia(mediaModel);
        }
    }

    public async Task DeleteMedia(int id)
    {
        await _repository.DeleteMedia(id);
    }

    public async Task<List<MediaModel>> GetMediaByUserId(int userId)
    {
        return await _repository.GetMediaByUserId(userId);
    }
    public async Task<List<MediaModel>> GetMediasByEventId(int eventId)
    {
        return await _repository.GetMediasByEventId(eventId);
    }
}