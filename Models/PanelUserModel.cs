using System.ComponentModel.DataAnnotations.Schema;

namespace api_event_panel.Models;

public class PanelUserModel
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string? PhoneNumber { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public string Role  { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }
    public DateTime? DeletedOn { get; set; }
    public List<EventModel> EventList { get; set; }
    public ServicePackageModel? ServicePackage { get; set; }
    [ForeignKey("servicePackageId")]
    public int? ServicePackageId { get; set; } 
    public DateTime? ServicePackageExpiration { get; set; }
    public DateTime? ServicePackageAddedOn { get; set; }
    public int? maxEvents { get; set; }
    public long usingStorage { get; set; }
    public long storageLimit {get; set;}
}