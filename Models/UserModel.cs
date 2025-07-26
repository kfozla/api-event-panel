namespace api_event_panel.Models;

public class UserModel
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string SessionId { get; set; }
    public List<MediaModel> MediaList { get; set; } = new List<MediaModel>();
}