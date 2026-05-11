namespace api_event_panel.Dtos;

public class UpdateVillaRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; }
    public DateTime LastModifiedOn { get; set; }
    
}