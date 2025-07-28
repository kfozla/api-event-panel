using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;

namespace api_event_panel.Models;
using System.Text.Json.Serialization;

public class MediaModel
{
    public int Id { get; set; }
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public DateTime UploadedOn { get; set; }
    public DateTime? DeletedOn { get; set; }
    public  string? PosterPath { get; set; }
    public string FileType { get; set; }
    public long FileSize { get; set; }
    
    [ForeignKey("UserId")]
    public int UserId { get; set; }
 
    [JsonIgnore]
    public UserModel? User { get; set; }

    public MediaModel(string FileName, string FilePath, DateTime UploadedOn, DateTime? DeletedOn, int UserId)
    {
        this.FileName = FileName;
        this.FilePath = FilePath;
        this.UploadedOn = UploadedOn;
        this.DeletedOn = DeletedOn;
        this.UserId = UserId;
    }
    public MediaModel() {}
    
}