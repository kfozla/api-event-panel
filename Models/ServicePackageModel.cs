using System.Text.Json.Serialization;

namespace api_event_panel.Models;

public class ServicePackageModel
{ 
    public int Id { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public int activeFor { get; set; }
    public int maxEvents { get; set; }
    [JsonIgnore]
    public List<PanelUserModel>? activePanelUsers { get; set; }
    public int? panelUserCount { get; set; }
    public int price { get; set; }
    public int storageLimit { get; set; }

    
}