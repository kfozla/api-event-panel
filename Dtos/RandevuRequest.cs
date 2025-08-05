namespace api_event_panel.Dtos;

public class RandevuRequest
{
    public string name { get; set; }
    public DateTime start { get; set; }
    public DateTime end { get; set; }
    public string description { get; set; }
    public DateTime createdOn { get; set; }
}