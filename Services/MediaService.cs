using api_event_panel.Models;
using api_event_panel.Repositories;
using  System.Diagnostics;

namespace api_event_panel.Services;

public class MediaService:IMediaService
{
    private readonly IMediaRepository _repository;
    private readonly IUserRepository _userRepository;
    private readonly IEventRepository _eventRepository;
    private readonly IPanelUserRepository _panelUserRepository;

    public MediaService(IMediaRepository repository,IUserRepository userRepository, IEventRepository eventRepository, IPanelUserRepository panelUserRepository)
    {
        _repository = repository;
        _userRepository = userRepository;
        _eventRepository = eventRepository;
        _panelUserRepository = panelUserRepository;
    }

    public async Task<List<MediaModel>> GetMedias()
    {
        return await _repository.GetMedias();
    }

    public async Task<MediaModel> GetMedia(int id)
    {
        return await _repository.GetMedia(id);
    }

    public async Task SaveMedia(string username,string sessionId,List<IFormFile> mediaList)
    {
        if (!Directory.Exists("uploads"))
            Directory.CreateDirectory("uploads");
        
        
        UserModel user;
        EventModel e;
        PanelUserModel panelUser;
        try
        {
             user = _userRepository.GetByUsernameAndSession(username,sessionId).Result;
             e = _eventRepository.GetEvent(user.EventId).Result;
             panelUser = _panelUserRepository.GetPanelUser(e.PanelUserId).Result;
        }
        catch (Exception)
        {
            throw new Exception("bilgileri getirirken hata oluştu");
        }

        if (panelUser.usingStorage >= panelUser.storageLimit)
        {
            throw new Exception("Depolama limitine ulaşıldı");
        }
        
        foreach (var media in mediaList)
        {
            var newFileName = user.Username + "-" +user.SessionId + media.FileName;
            
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
            e =  _eventRepository.GetEvent(user.EventId).Result;
            e.storageSize += media.Length;
            await _eventRepository.UpdateEvent(e);
            
            panelUser.usingStorage+= media.Length;
            await _panelUserRepository.UpdateUser(panelUser);

            await _repository.SaveMedia(mediaModel);
        }
    }

    public async Task DeleteMedia(string username,string sessionId,int id,string? panelUserRole)
    {
        var media = await _repository.GetMedia(id);
        var user = _userRepository.GetById(media.UserId).Result;
        var Event = _eventRepository.GetEvent(user.EventId).Result;
        var panelUser = _panelUserRepository.GetPanelUser(Event.PanelUserId).Result;
        if (user.Username == username && user.SessionId == sessionId)
        {
            
            Event.storageSize -= media.FileSize;
            await _eventRepository.UpdateEvent(Event);
            panelUser.usingStorage -= media.FileSize;
            await _panelUserRepository.UpdateUser(panelUser);
        
            await _repository.DeleteMedia(id);
        }

        if (panelUserRole != null)
        {
            await _repository.DeleteMedia(id);
        }
        throw new UnauthorizedAccessException();
        
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