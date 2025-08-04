using api_event_panel.Models;

namespace api_event_panel.Repositories;

public interface IServicePackageRepository
{
    Task <ServicePackageModel> GetServicePackage(int servicePackageId);
    Task<List<ServicePackageModel>> GetServicePackages();
    Task AddServicePackage(ServicePackageModel servicePackage);
    Task UpdateServicePackage(ServicePackageModel servicePackage);
    Task DeleteServicePackage(int servicePackageId);
}