using api_event_panel.Dtos;
using api_event_panel.Models;

namespace api_event_panel.Services;

public interface IServicePackageService
{ 
    Task <ServicePackageModel> GetServicePackage(int servicePackageId);
    Task<List<ServicePackageModel>> GetServicePackages();
    Task AddServicePackage(ServicePackageRequest servicePackage);
    Task UpdateServicePackage(int id,ServicePackageRequest servicePackage);
    Task DeleteServicePackage(int servicePackageId);
}