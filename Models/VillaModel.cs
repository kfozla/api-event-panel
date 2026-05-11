namespace api_event_panel.Models;

public class VillaModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; }
    public DateTime LastModifiedOn { get; set; }
}