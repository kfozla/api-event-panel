using api_event_panel.Models;

namespace api_event_panel.Dtos;

public class UpdateEventRequest
{
    public string name { get; set; }
    public string description { get; set; }
    public DateTime startTime { get; set; }
    public DateTime endTime { get; set; }
    public string thumbnailUrl { get; set; }
    public string theme { get; set; }
    public string domainName { get; set; }
    public int panelUserId { get; set; }
}