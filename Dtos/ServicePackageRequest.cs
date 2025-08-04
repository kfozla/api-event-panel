namespace api_event_panel.Dtos;

public class ServicePackageRequest
{
    public string title { get; set; }
    public string description { get; set; }
    public int activeFor { get; set; }
    public int maxEvents { get; set; }
    public int price { get; set; }
}