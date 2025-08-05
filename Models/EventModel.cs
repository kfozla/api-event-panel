using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace api_event_panel.Models;

public class EventModel
{
    public EventModel()
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
    [ForeignKey("PanelUserId")]
    public int PanelUserId { get; set; }
    [JsonIgnore]
    public PanelUserModel?  PanelUser{get;set;}
    public long storageSize { get; set; }
}