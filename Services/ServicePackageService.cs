using api_event_panel.Dtos;
using api_event_panel.Models;
using api_event_panel.Repositories;

namespace api_event_panel.Services;

public class ServicePackageService: IServicePackageService
{
    private readonly IServicePackageRepository  _servicePackageRepository;
    private readonly IPanelUserService _panelUserService;

    public  ServicePackageService(IServicePackageRepository servicePackageRepository, IPanelUserService panelUserService)
    {
         _servicePackageRepository = servicePackageRepository;
         _panelUserService = panelUserService;
    }

    public async Task AddServicePackage(ServicePackageRequest servicePackage)
    {
        ServicePackageModel servicePackageModel = new ServicePackageModel
        {
            title = servicePackage.title,
            description = servicePackage.description,
            maxEvents = servicePackage.maxEvents,
            activeFor = servicePackage.activeFor,
            activePanelUsers = new List<PanelUserModel>(),
            price = servicePackage.price
        };
        await _servicePackageRepository.AddServicePackage(servicePackageModel);
    }

    public async Task UpdateServicePackage(int id,ServicePackageRequest servicePackage)
    {
        var existingServicePackageModel = _servicePackageRepository.GetServicePackage(id).Result;
        
        existingServicePackageModel.title = servicePackage.title;
        existingServicePackageModel.description = servicePackage.description;
        existingServicePackageModel.maxEvents = servicePackage.maxEvents;
        existingServicePackageModel.activeFor = servicePackage.activeFor;
        existingServicePackageModel.storageLimit = servicePackage.storageLimit;
        existingServicePackageModel.price = servicePackage.price;
        
        await _servicePackageRepository.UpdateServicePackage(existingServicePackageModel);
    }

    public async Task DeleteServicePackage(int servicePackageId)
    {
         await _servicePackageRepository.DeleteServicePackage(servicePackageId);
    }

    public async Task<List<ServicePackageModel>> GetServicePackages()
    {
        var servicePackages = await _servicePackageRepository.GetServicePackages();
        foreach (var servicePackage in servicePackages)
        {
            var userCountForServicePackageModel= _panelUserService.GetAllPanelUsers().Result.Count(u => u.ServicePackageId == servicePackage.Id);
            servicePackage.panelUserCount = userCountForServicePackageModel;
        }
        
        return await _servicePackageRepository.GetServicePackages();
    }

    public async Task<ServicePackageModel> GetServicePackage(int servicePackageId)
    {
        return await _servicePackageRepository.GetServicePackage(servicePackageId);
    }
}