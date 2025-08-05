using api_event_panel.Models;

namespace api_event_panel.Dtos;

public class UserMappedevent
{
    public UserMappedevent()
    {
        UserList = new List<UserModel>();
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }
    public DateTime? DeletedOn { get; set; }
    public string thumbnailUrl { get; set; }
    public List<UserModel>? UserList { get; set; }
    public string Theme{get;set;}
    public string DomainName{get;set;}
    public int PanelUserId { get; set; }
    public PanelUserDto?  PanelUser{get;set;}
    public long storageSize { get; set; }
}