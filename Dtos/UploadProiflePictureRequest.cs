using System.ComponentModel.DataAnnotations;

namespace api_event_panel.Dtos;

public class UploadProfilePictureRequest
{
    [Required]
    [DataType(DataType.Upload)]
    public IFormFile File { get; set; }

}