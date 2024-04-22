using System.Data.Entity;
using InnoClinic.Shared.Domain.Abstractions;
using InnoClinic.Shared.Exceptions.Models;
using ServicesAPI.Domain;
using Shared.Misc;

namespace ServicesAPI.Application.Helpers;

internal static class AuthorizationHelper {

    public static async Task<IEnumerable<Service>> GetAuthorizedPage(
        this IRepository<Service> repository,
        IPageDesc pageDesc,
        IUserDescriptor user,
        CancellationToken cancellationToken = default) {
            
        var query = repository.GetAll();

        if (!user.IsAdmin()) {
            query = query.Where(product => product.IsActive);
        }

        return await query.Paginate(pageDesc).ToListAsync(cancellationToken);
    }

    public static bool IsVisibleToUser(this Service service, IUserDescriptor user) {
        if (user.IsAdmin()) return true;

        return service.IsActive;
    }

    /// <summary>
    /// That function validates if user can see service otherwise throws exception
    /// </summary>
    /// <param name="service">Service</param>
    /// <param name="user">User that tries to see product</param>
    /// <exception cref="NotFoundException">Product is not visible to user</exception>
    public static void ValidateVisibility(this Service service, IUserDescriptor user) {
        if (service.IsVisibleToUser(user)) {
            return;
        }

        throw NotFoundException.NotFoundInDatabase($"Service with id {service.Id}");
    }
}
