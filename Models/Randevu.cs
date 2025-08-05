using System.ComponentModel.DataAnnotations.Schema;

namespace api_event_panel.Models;

public class Randevu
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime start { get; set; }
    public DateTime end { get; set; }
    public DateTime createdOn { get; set; }
    public DateTime updatedOn { get; set; }
    public DateTime? deletedOn { get; set; }
    public string description { get; set; }
    [ForeignKey("panelUserId")]
    public int panelUserId { get; set; }
}