namespace api_event_panel.Models;

public class EventModel
{
    public EventModel()
    {
        PersonList = new List<string>();
        MediaList = new List<MediaModel>();
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }
    public DateTime DeletedOn { get; set; }
    public string thumbnailUrl { get; set; }
    public List<string> PersonList { get; set; }
    public List<MediaModel>? MediaList { get; set; }
    public string Theme{get;set;}
    public string DomainName{get;set;}
}